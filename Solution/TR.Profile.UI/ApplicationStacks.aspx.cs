///
///                           $Id: ApplicationStacks.aspx.cs 14023 2010-11-26 15:17:41Z neil.middleton $
///              $LastChangedDate: 2010-11-26 15:17:41 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14023 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/PlatformStacks.aspx.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;
using TR.Profile.Common.Data;
using TR.Profile.Data;
using TR.Profile.Common;
using System.Drawing;

namespace TR.Profile.UI
{
	public partial class ApplicationStacksPage : Page
	{
		private const string PAGE_STATE_KEY = "PageState";

		private const string COARSE_FILTER_PAGE_STATE = "CoarseFilter";
		private const string FINE_FILTER_PAGE_STATE = "FineFilter";
		private const string STACK_PAGE_STATE = "Stack";

		private const string MESSAGE_03 = "Specify Platform Stack";

		private Exception _exception = null;
		private StackHierarchy _hierarchy = null;

		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}
		private string PageState
		{
			get { return this.ViewState[PAGE_STATE_KEY] as string; }
			set { this.ViewState[PAGE_STATE_KEY] = value; }
		}
		private IEnumerable<CheckBox> StackCheckBoxes
		{
			get
			{
				foreach (RepeaterItem repeaterItem in StacksRepeater.Items)
				{
					foreach (CheckBox checkBox in repeaterItem.Controls.OfType<CheckBox>())
					{
						yield return checkBox;
					}
				}
			}
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			_hierarchy = new StackHierarchy();
			try
			{
				using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
				{
					StackGatewayBase gateway = new ApplicationStackGateway(dt.Transaction);
					gateway.Load(_hierarchy);
				}
			}
			catch (Exception ex)
			{
				_exception = ex;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.CurrentProfile == null)
			{
				Navigator.RedirectToDefaultPage(this.Response);
			}
			if (_exception != null)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, _exception.Message);
				return;
			}

			if (!IsPostBack)
			{
				// Init Controls
				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				FilenameLabel.Text = this.CurrentProfile.Context.Name;

				// Bindings
				BindCoarseFilter();
				StacksRepeater.DataSource = _hierarchy.Stacks;
				DataBind();

				// Check CheckBoxes
				foreach (CheckBox checkBox in this.StackCheckBoxes)
				{
					checkBox.Checked = this.CurrentProfile.ActiveProvisioningPlan.ApplicationStacks.Any((s) => s.Name == checkBox.Text);
				}

				// Set initial State
				this.PageState = COARSE_FILTER_PAGE_STATE;
				SetPageState(COARSE_FILTER_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_03);
			}
			else
			{
				// Update Profile
				Act(
					(checkBox, stack) =>
					{
						if (checkBox.Checked)
						{	// add
							if (stack == null)
							{
								stack = _hierarchy.Stacks.First((s) => s.Name == checkBox.Text);
								this.CurrentProfile.ActiveProvisioningPlan.ApplicationStacks.Add(stack);
							}
						}
						else // if (!checkBox.Checked)
						{	// delete
							if (stack != null)
							{
								this.CurrentProfile.ActiveProvisioningPlan.ApplicationStacks.Remove(stack);
							}
						}
					});

				// Rebuid CheckBoxes
				FilterLookup majorFilter = _hierarchy.FindMajorFilterByCode(CoarseFilterDropDownList.SelectedItem.Value);
				FilterLookup minorFilter = _hierarchy.FindMinorFilterByCode(FineFilterDropDownList.SelectedItem.Value);
				StacksRepeater.DataSource = RunThrough(_hierarchy.Stacks, majorFilter, minorFilter).ToList<Stack>();
				DataBind();
			}
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			// Update state of CheckBoxes
			Act(
				(checkBox, stack) =>
				{
					if (stack != null)
					{
						checkBox.Checked = true;
					}
				});
		}

		protected void CoarseFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentProfile.ActiveProvisioningPlan.PlatformStack.MajorFilter = _hierarchy.FindMajorFilterByCode(CoarseFilterDropDownList.SelectedItem.Value);
			if (CurrentProfile.ActiveProvisioningPlan.PlatformStack.MajorFilter == null || CurrentProfile.ActiveProvisioningPlan.PlatformStack.MajorFilter.IsDefault)
			{
				SetPageState(COARSE_FILTER_PAGE_STATE);
			}
			else
			{
				SetPageState(FINE_FILTER_PAGE_STATE);
			}
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_03);
		}
		protected void FineFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentProfile.ActiveProvisioningPlan.PlatformStack.MinorFilter = _hierarchy.FindMinorFilterByCode(FineFilterDropDownList.SelectedItem.Value);
			if (CurrentProfile.ActiveProvisioningPlan.PlatformStack.MinorFilter == null || CurrentProfile.ActiveProvisioningPlan.PlatformStack.MinorFilter.IsDefault)
			{
				SetPageState(FINE_FILTER_PAGE_STATE);
			}
			else
			{
				SetPageState(STACK_PAGE_STATE);
			}
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_03);
		}

		protected void AcceptButton_Click(object sender, EventArgs e)
		{
			Navigator.RedirectToProvisioningPlanExtrasPage(this.CurrentProfile, this.Response);
		}

		private void SetPageState(string pPageState)
		{
			if (pPageState == COARSE_FILTER_PAGE_STATE)
			{
				FineFilterDropDownList.DataSource = new List<FilterLookup> { new FilterLookup() };
				FineFilterDropDownList.DataValueField = "Code";
				FineFilterDropDownList.DataTextField = "Code";
				FineFilterDropDownList.Enabled = false;
				this.DataBind();
			}
			else if (pPageState == FINE_FILTER_PAGE_STATE)
			{
				List<FilterLookup> minorFilters =
					(from item in _hierarchy.Stacks
					 where item.MajorFilter.Code == CurrentProfile.ActiveProvisioningPlan.PlatformStack.MajorFilter.Code
					 group item by item.MinorFilter.Code into c
					 select c.First().MinorFilter)
					 .ToList<FilterLookup>();
				minorFilters.Insert(0, new FilterLookup());

				FineFilterDropDownList.DataSource = minorFilters;
				FineFilterDropDownList.DataValueField = "Code";
				FineFilterDropDownList.DataTextField = "Code";
				FineFilterDropDownList.Enabled = true;
				this.DataBind();
			}
		}

		private void BindCoarseFilter()
		{
			List<FilterLookup> majorFilters =
				(from item in _hierarchy.Stacks
				 group item by item.MajorFilter.Code into i
				 select i.First().MajorFilter)
				 .ToList<FilterLookup>();
			majorFilters.Insert(0, new FilterLookup());

			CoarseFilterDropDownList.DataSource = majorFilters;
			CoarseFilterDropDownList.DataValueField = "Code";
			CoarseFilterDropDownList.DataTextField = "Code";
		}

		private IEnumerable<Stack> RunThrough(ICollection<Stack> pItems, FilterLookup pMajorFilter, FilterLookup pMinorFilter)
		{
			if (pItems != null)
			{
				foreach (Stack stack in pItems)
				{
					if (pMajorFilter == null || pMajorFilter.IsDefault)
					{
						yield return stack;
					}
					else if (stack.MajorFilter.Code == pMajorFilter.Code)
					{
						if (pMinorFilter == null || pMinorFilter.IsDefault)
						{
							yield return stack;
						}
						else if (stack.MinorFilter.Code == pMinorFilter.Code)
						{
							yield return stack;
						}
					}
				}
			}
		}

		private void Act(Action<CheckBox, Stack> pAction)
		{
			if (pAction == null)
				return;

			foreach (CheckBox checkBox in this.StackCheckBoxes)
			{
				Stack stack = this.CurrentProfile.ActiveProvisioningPlan.ApplicationStacks.FirstOrDefault((s) => s.Name == checkBox.Text);
				pAction(checkBox, stack);
			}
		}

	}

}
