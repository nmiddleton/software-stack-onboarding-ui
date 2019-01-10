///
///                           $Id: ITIC.aspx.cs 14754 2011-01-21 10:45:55Z neil.middleton $
///              $LastChangedDate: 2011-01-21 10:45:55 +0000 (Fri, 21 Jan 2011) $
///          $LastChangedRevision: 14754 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/ITIC.aspx.cs $
///

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Common;
using TR.Profile.Common.Data;
using TR.Profile.Data;
using TR.Profile.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace TR.Profile.UI
{
    public partial class ITICPage : Page
    {
        private const string PAGE_STATE_KEY = "PageState";
		private const string PLATFORM_STACK_NAME_KEY = "PlatformStackName";

        private const string INFRASTUCTURE_PAGE_STATE = "Infrastructure";
        private const string CAPABILITY_PAGE_STATE = "Capability";
        private const string LSG_PAGE_STATE = "LogicalSystemGroup";

		private const string STACK_PAGE_STATE = "Stack";
        private const string FINAL_PAGE_STATE = "Finita La Comedia";

        private const string MESSAGE_01 = "Specify Infrastructure";
        private const string MESSAGE_02 = "Specify Capability";
        private const string MESSAGE_03 = "Specify Logical System Group";
		private const string MESSAGE_04 = "Specify Standard OS Build";

        private Exception _exception = null;
        private ITICHierarchy _ITICHierarchy = null;
		private PlatformStackHierarchy _platformStackHierarchy = null;

		private string PlatformStackName
		{
			get { return this.ViewState[PLATFORM_STACK_NAME_KEY] as string; }
			set { this.ViewState[PLATFORM_STACK_NAME_KEY] = value; }
		}
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

		private FilterLookup MajorFilter
		{
			get
			{
				return (CoarseFilterDropDownList.SelectedValue == FilterLookup.DEFAULT_CODE) ?
					new FilterLookup() :
					_platformStackHierarchy.FindMajorFilterByCode(CoarseFilterDropDownList.SelectedValue);
			}
		}
		private FilterLookup MinorFilter
		{
			get
			{
				return (FineFilterDropDownList.SelectedValue == FilterLookup.DEFAULT_CODE) ?
					new FilterLookup() :
					_platformStackHierarchy.FindMinorFilterByCode(FineFilterDropDownList.SelectedValue);
			}
		}
		private FilterLookup ArchitectureFilter
		{
			get
			{
				return (ArchitectureDropDownList.SelectedValue == FilterLookup.DEFAULT_CODE) ?
					new FilterLookup() :
					_platformStackHierarchy.FindArchitectureByCode(ArchitectureDropDownList.SelectedValue);
			}
		}
		private FilterLookup BuildFilter
		{
			get
			{
				return (BuildDropDownList.SelectedValue == FilterLookup.DEFAULT_CODE) ?
					new FilterLookup() :
					_platformStackHierarchy.FindBuildByCode(BuildDropDownList.SelectedValue);
			}
		}

        protected void Page_Init(object sender, EventArgs e)
        {
			using (EventWaitHandle synchronizer = new AutoResetEvent(false))
			{
				Task t1 = Task.Factory.StartNew(
					() =>
					{
						try
						{
							_ITICHierarchy = new ITICHierarchy();
							using (DataTransactionBase dt = new OracleDataTransaction(Configuration.ITICConnectionString))
							{
								ITICGateway gateway = new ITICGateway(dt.Transaction);
								gateway.Load(_ITICHierarchy);
							}
						}
						catch (Exception ex)
						{
							_exception = ex;
						}
					});
				Task t2 = Task.Factory.StartNew(
					() =>
					{
						try
						{
							_platformStackHierarchy = new PlatformStackHierarchy();
							using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
							{
								StackGatewayBase gateway = new PlatformStackGateway(dt.Transaction);
								gateway.Load(_platformStackHierarchy);
							}
						}
						catch (Exception ex)
						{
							_exception = ex;
						}
					});
				Task.Factory.ContinueWhenAll(new Task[] { t1, t2 }, (ts) => { synchronizer.Set(); });
				synchronizer.WaitOne();
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

			if (!this.IsPostBack)
			{
				BindInfrastructure();

				SetPageState(INFRASTUCTURE_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_01);

				this.PlatformStackName = null;
				InitFilters();
			}
			else	// if (this.IsPostBack)
			{
				if (StacksRadioButtonList.SelectedIndex >= 0)
				{
					this.PlatformStackName = StacksRadioButtonList.SelectedValue;
				}
			}
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (this.PlatformStackName != null)
			{
				for (int i = 0; i < StacksRadioButtonList.Items.Count; i++)
				{
					ListItem item = StacksRadioButtonList.Items[i];
					if (item.Value == this.PlatformStackName)
					{
						StacksRadioButtonList.SelectedIndex = i;
						SetPageState(FINAL_PAGE_STATE);
						return;
					}
				}
			}
		}

        protected void InfrastructureDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentProfile.ITICCode.Infrastructure = _ITICHierarchy.FindInfrastructureByCode(InfrastructureDropDownList.SelectedItem.Value);
            CurrentProfile.ITICCode.Capability = null;
            CurrentProfile.ITICCode.LogicalSystemGroup = null;
			if (CurrentProfile.ITICCode.Infrastructure == null || CurrentProfile.ITICCode.Infrastructure.IsDefault)
            {
                SetPageState(INFRASTUCTURE_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_01);
            }
            else
            {
                SetPageState(CAPABILITY_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_02);
			}
		}
        protected void CapabilityDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentProfile.ITICCode.Capability = _ITICHierarchy.FindCapabilityByCode(CapabilityDropDownList.SelectedItem.Value);
            CurrentProfile.ITICCode.LogicalSystemGroup = null;
			if (CurrentProfile.ITICCode.Capability == null || CurrentProfile.ITICCode.Capability.IsDefault)
            {
                SetPageState(CAPABILITY_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_02);
            }
            else
            {
                SetPageState(LSG_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_03);
			}
		}
        protected void LogicalSystemGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentProfile.ITICCode.LogicalSystemGroup = _ITICHierarchy.FindLogicalSystemGroupByCode(LogicalSystemGroupDropDownList.SelectedItem.Value);
			if (CurrentProfile.ITICCode.LogicalSystemGroup == null || CurrentProfile.ITICCode.LogicalSystemGroup.IsDefault)
            {
                SetPageState(LSG_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_03);
            }
            else
            {
				SetPageState(STACK_PAGE_STATE);
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, MESSAGE_04);
			}
		}
		protected void PopulateStacks_Handler(object sender, EventArgs e)
		{
			PopulateStacks();
		}
		protected void DropFilterImageButton_Click(object sender, ImageClickEventArgs e)
		{
			InitFilters();
			PopulateStacks();
		}

		protected void StacksRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
		}

		protected void AcceptButton_Click(object sender, EventArgs e)
        {
			string name = Domain.Profile.BuildFullName(this.CurrentProfile.ITICCode.Code, this.PlatformStackName);
			this.CurrentProfile.Context.Name = name;
			this.CurrentProfile.ProvisioningPlans[0].Name = name;
			PlatformStack ps = _platformStackHierarchy.Stacks.First((s) => s.Name == this.PlatformStackName) as PlatformStack;
			this.CurrentProfile.Context.PlatformName = ps.MajorFilter.Code;
			this.CurrentProfile.Context.PlatformVersion = ps.MinorFilter.Code;
			this.CurrentProfile.Context.Architecture = ps.Architecture.Code;
			this.CurrentProfile.Context.TRBuild = ps.Build.Code;
			Navigator.RedirectToPlatformStacksPage(this.CurrentProfile, this.Response);
		}

        private void SetPageState(string pPageState)
        {
            if (pPageState == INFRASTUCTURE_PAGE_STATE)
            {
				EnableDisableCapability(false);
				EnableDisableLogicalSystemGroup(false);
                this.DataBind();

				CoarseFilterDropDownList.Enabled = false;
				FineFilterDropDownList.Enabled = false;
				ArchitectureDropDownList.Enabled = false;
				BuildDropDownList.Enabled = false;
				StacksRadioButtonList.DataSource = null;
				StacksRadioButtonList.DataBind();

                AcceptButton.Enabled = false;
            }
            else if (pPageState == CAPABILITY_PAGE_STATE)
            {
				EnableDisableCapability(true);
				EnableDisableLogicalSystemGroup(false);
				this.DataBind();

				CoarseFilterDropDownList.Enabled = false;
				FineFilterDropDownList.Enabled = false;
				ArchitectureDropDownList.Enabled = false;
				BuildDropDownList.Enabled = false;
				StacksRadioButtonList.DataSource = null;
				StacksRadioButtonList.DataBind();

                AcceptButton.Enabled = false;
            }
            else if (pPageState == LSG_PAGE_STATE)
            {
				EnableDisableLogicalSystemGroup(true);
				this.DataBind();

				CoarseFilterDropDownList.Enabled = false;
				FineFilterDropDownList.Enabled = false;
				ArchitectureDropDownList.Enabled = false;
				BuildDropDownList.Enabled = false;
				StacksRadioButtonList.DataSource = null;
				StacksRadioButtonList.DataBind();

                AcceptButton.Enabled = false;
            }
			else if (pPageState == STACK_PAGE_STATE)
			{
				CoarseFilterDropDownList.Enabled = true;
				FineFilterDropDownList.Enabled = true;
				ArchitectureDropDownList.Enabled = true;
				BuildDropDownList.Enabled = true;
				PopulateStacks();

				AcceptButton.Enabled = false;
			}
            else if (pPageState == FINAL_PAGE_STATE)
            {
                AcceptButton.Enabled = true;
            }
        }

		#region misc
		private void BindInfrastructure()
		{
			List<InfrastructureLookup> infrastructures =
				(from itic in _ITICHierarchy.Codes
				 group itic by itic.Infrastructure.Code into i
				 select i.First().Infrastructure)
				 .ToList<InfrastructureLookup>();
			infrastructures.Add(new InfrastructureLookup());
			infrastructures.Sort(InfrastructureLookup.CompareByName);

			InfrastructureDropDownList.DataSource = infrastructures;
			InfrastructureDropDownList.DataValueField = "Code";
			InfrastructureDropDownList.DataTextField = "Name";
		}
		private void EnableDisableCapability(bool pEnable)
		{
			if (pEnable)
			{
				List<CapabilityLookup> capabilities =
					(from itic in _ITICHierarchy.Codes
					 where itic.Infrastructure.Code == CurrentProfile.ITICCode.Infrastructure.Code
					 group itic by itic.Capability.Code into c
					 select c.First().Capability)
					 .ToList<CapabilityLookup>();
				capabilities.Add(new CapabilityLookup());
				capabilities.Sort(CapabilityLookup.CompareByName);

				CapabilityDropDownList.DataSource = capabilities;
				CapabilityDropDownList.DataValueField = "Code";
				CapabilityDropDownList.DataTextField = "Name";
				CapabilityDropDownList.Enabled = true;
			}
			else
			{
				CapabilityDropDownList.DataSource = new List<CapabilityLookup> { new CapabilityLookup() };
				CapabilityDropDownList.DataValueField = "Code";
				CapabilityDropDownList.DataTextField = "Name";
				CapabilityDropDownList.Enabled = false;
			}
		}
		private void EnableDisableLogicalSystemGroup(bool pEnable)
		{
			if (pEnable)
			{
				List<LogicalSystemGroupLookup> lsg =
					(from itic in _ITICHierarchy.Codes
					 where itic.Capability.Code == CurrentProfile.ITICCode.Capability.Code
					 group itic by itic.LogicalSystemGroup.Code into l
					 select l.First().LogicalSystemGroup)
					 .ToList<LogicalSystemGroupLookup>();
				lsg.Add(new LogicalSystemGroupLookup());
				lsg.Sort(LogicalSystemGroupLookup.CompareByName);

				LogicalSystemGroupDropDownList.DataSource = lsg;
				LogicalSystemGroupDropDownList.DataValueField = "Code";
				LogicalSystemGroupDropDownList.DataTextField = "Name";
				LogicalSystemGroupDropDownList.Enabled = true;
			}
			else
			{
				LogicalSystemGroupDropDownList.DataSource = new List<LogicalSystemGroupLookup> { new LogicalSystemGroupLookup() };
				LogicalSystemGroupDropDownList.DataValueField = "Code";
				LogicalSystemGroupDropDownList.DataTextField = "Name";
				LogicalSystemGroupDropDownList.Enabled = false;
			}
		}

		private void InitFilters()
		{
			_platformStackHierarchy.MajorFilters.Insert(0, new FilterLookup());
			CoarseFilterDropDownList.DataSource = _platformStackHierarchy.MajorFilters;
			CoarseFilterDropDownList.DataTextField = "Code";
			CoarseFilterDropDownList.DataValueField = "Code";
			CoarseFilterDropDownList.DataBind();

			_platformStackHierarchy.MinorFilters.Insert(0, new FilterLookup());
			FineFilterDropDownList.DataSource = _platformStackHierarchy.MinorFilters;
			FineFilterDropDownList.DataTextField = "Code";
			FineFilterDropDownList.DataValueField = "Code";
			FineFilterDropDownList.DataBind();

			_platformStackHierarchy.ArchitectureFilters.Insert(0, new FilterLookup());
			ArchitectureDropDownList.DataSource = _platformStackHierarchy.ArchitectureFilters;
			ArchitectureDropDownList.DataTextField = "Code";
			ArchitectureDropDownList.DataValueField = "Code";
			ArchitectureDropDownList.DataBind();

			_platformStackHierarchy.BuildFilters.Insert(0, new FilterLookup());
			BuildDropDownList.DataSource = _platformStackHierarchy.BuildFilters;
			BuildDropDownList.DataTextField = "Code";
			BuildDropDownList.DataValueField = "Code";
			BuildDropDownList.DataBind();
		}
		private void PopulateStacks()
		{
			StacksRadioButtonList.DataSource = _platformStackHierarchy.GatherPlatformStacks(this.MajorFilter, this.MinorFilter, this.ArchitectureFilter, this.BuildFilter);
			StacksRadioButtonList.DataTextField = "Name";
			StacksRadioButtonList.DataValueField = "Name";
			StacksRadioButtonList.DataBind();
		}

		private void SetSelection(DropDownList pControl, string pValue)
		{
			if (pControl == null || String.IsNullOrWhiteSpace(pValue))
				return;

			for (int i = 0; i < pControl.Items.Count; i++)
			{
				if (pControl.Items[i].Value == pValue)
				{
					pControl.SelectedIndex = i;
					return;
				}
			}
		}
		#endregion

	}

}
