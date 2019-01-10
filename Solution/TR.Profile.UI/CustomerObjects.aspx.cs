///
///                           $Id: CustomerObjects.aspx.cs 14023 2010-11-26 15:17:41Z neil.middleton $
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

namespace TR.Profile.UI
{
	public partial class CustomerObjectsPage : Page
	{
		private const string ACTIVE_HtmlTagState = "background-color:Green; color:White;";
		private const string PASSIVE_HtmlTagState = "";

		public string HtmlTagState = string.Empty;

		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.CurrentProfile == null)
			{
				Navigator.RedirectToDefaultPage(this.Response);
			}

			if (!this.IsPostBack)
			{
				// Init Controls
				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				FilenameLabel.Text = this.CurrentProfile.Context.Name;

				// Update Stacks & Scans & Workflows
				foreach (string item in this.CurrentProfile.ActiveProvisioningPlan.CustomStacks)
				{
					StacksListBox.Items.Add(
					new ListItem(item));
				}
				foreach (string item in this.CurrentProfile.ActiveProvisioningPlan.CustomScans)
				{
					ScansListBox.Items.Add(
					new ListItem(item));
				}

				// Set initial State
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (this.IsPostBack)
			{
				WorkflowsListBox.DataSource = this.CurrentProfile.ActiveProvisioningPlan.Workflows;
				WorkflowsListBox.DataTextField = "NameValue";
				WorkflowsListBox.DataValueField = "Name";
				WorkflowsListBox.DataBind();
			}
		}

		protected void AddStackButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(StackTextBox.Text))
				return;

			if (!ContainsStack(StackTextBox.Text))
			{
				StacksListBox.Items.Add(StackTextBox.Text);
			}
			StackTextBox.Text = string.Empty;
		}
		protected void RemoveStackButton_Click(object sender, EventArgs e)
		{
			if (StacksListBox.Items.Count == 0)
				return;

			for (int i = StacksListBox.Items.Count -1; i >= 0; i--)
			{
				if (StacksListBox.Items[i].Selected)
				{
					StacksListBox.Items.RemoveAt(i);
				}
			}
		}
		protected void AddScanButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(ScanTextBox.Text))
				return;

			if (!ContainsStack(ScanTextBox.Text))
			{
				ScansListBox.Items.Add(ScanTextBox.Text);
			}
			ScanTextBox.Text = string.Empty;
		}
		protected void RemoveScanButton_Click(object sender, EventArgs e)
		{
			if (ScansListBox.Items.Count == 0)
				return;

			for (int i = ScansListBox.Items.Count - 1; i >= 0; i--)
			{
				if (ScansListBox.Items[i].Selected)
				{
					ScansListBox.Items.RemoveAt(i);
				}
			}
		}
		protected void AddWorkflowButton_Click(object sender, EventArgs e)
		{
			if (this.CurrentProfile.ActiveProvisioningPlan.Workflows.Any(w=>w.Name == WorkflowNameTextBox.Text))
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, "Please specify another name.");
				return;
			}

			this.CurrentProfile.ActiveProvisioningPlan.Workflows.Add(
				new ActionParameter(WorkflowNameTextBox.Text, WorkflowDataTextBox.Text));

			WorkflowNameTextBox.Text = string.Empty;
			WorkflowDataTextBox.Text = string.Empty;
		}
		protected void RemoveWorkflowButton_Click(object sender, EventArgs e)
		{
			ActionParameter parameter = this.CurrentProfile.ActiveProvisioningPlan.Workflows.First(w => w.Name == WorkflowsListBox.SelectedItem.Value);
			this.CurrentProfile.ActiveProvisioningPlan.Workflows.Remove(parameter);
		}

		protected void PostConfigurationActionsButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToAdvancedConfigurationActionsPage(this.CurrentProfile, this.Response);
		}
		protected void DomainNameResolutionButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToDomainNameResolutionPage(this.CurrentProfile, this.Response);
		}
		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}

		private void UpdateProfile()
		{
			AddItems(StacksListBox.Items, this.CurrentProfile.ActiveProvisioningPlan.CustomStacks);
			AddItems(ScansListBox.Items, this.CurrentProfile.ActiveProvisioningPlan.CustomScans);
		}

		#region misc
		private bool ContainsStack(string pValue)
		{
			if (String.IsNullOrWhiteSpace(pValue))
				return true;

			foreach (ListItem item in StacksListBox.Items)
			{
				if (item.Value == pValue)
				{
					return true;
				}
			}
			return false;
		}
		private bool ContainsScan(string pValue)
		{
			if (String.IsNullOrWhiteSpace(pValue))
				return true;

			foreach (ListItem item in ScansListBox.Items)
			{
				if (item.Value == pValue)
				{
					return true;
				}
			}
			return false;
		}
		private void AddItems(ListItemCollection pItems, ICollection<string> pValues)
		{
			if (pItems == null || pValues == null)
				return;

			pValues.Clear();
			foreach (ListItem item in pItems)
			{
				pValues.Add(item.Value);
			}
		}
		#endregion

	}

}
