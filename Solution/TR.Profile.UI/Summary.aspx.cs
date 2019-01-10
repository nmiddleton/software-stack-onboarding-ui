///
///                           $Id: SummaryPage.aspx.cs 14023 2010-11-26 15:17:41Z neil.middleton $
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
using TR.Profile.Common;
using TR.Profile.Common.Data;
using TR.Profile.Data;

namespace TR.Profile.UI
{
	public partial class SummaryPage : Page
	{
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
				this.CurrentProfile.ActiveProvisioningPlan.PopulatePlanActions();

				BindProvisioningPlan();

				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				ProfileNameLabel.Text = this.CurrentProfile.Context.Name;

				FilenameLabel.Text = this.CurrentProfile.FileName;
				VersionTextBox.Text = this.CurrentProfile.Context.ProfileVersion;
			}
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
		}

		protected void AdvancedPlanPropertiesButton_Click1(object sender, EventArgs e)
		{
			Navigator.RedirectToAdvancedSummaryPage(this.CurrentProfile, this.Response);
		}

		protected void PlanActionsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CurrentProfile.ActiveProvisioningPlan = this.CurrentProfile.ProvisioningPlans.First(pp => pp.Type == ProvisioningPlansDropDownList.SelectedValue);
		}
		protected void DuplicateProvisioningPlanImageButton_Click(object sender, ImageClickEventArgs e)
		{
			Navigator.RedirectToNewProvisioningPlanPage(this.CurrentProfile, this.Response);
		}
		protected void DeleteProvisioningPlanImageButton_Click(object sender, ImageClickEventArgs e)
		{
			if (this.CurrentProfile.ProvisioningPlans.Count == 1)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, "The last provisioning plan is not allowed to be delete.");
				return;
			}

			ProvisioningPlan pp = this.CurrentProfile.ActiveProvisioningPlan;
			this.CurrentProfile.ProvisioningPlans.Remove(this.CurrentProfile.ActiveProvisioningPlan);
			this.CurrentProfile.ActiveProvisioningPlan = this.CurrentProfile.ProvisioningPlans[0];

			BindProvisioningPlan();

			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, string.Format("{0} was deleted.", pp.Type));
		}

		protected void SaveVersionImageButton_Click(object sender, EventArgs e)
		{
			this.CurrentProfile.Context.ProfileVersion = VersionTextBox.Text;
			FilenameLabel.Text = this.CurrentProfile.FileName;
		}

		protected void ShowHTMLButton_Click(object sender, EventArgs e)
		{
			Navigator.RedirectToDocumentPage(this.CurrentProfile, this.Response, Navigator.SUMMARY_PAGE_URL);
		}
		protected void SaveHTMLButton_Click(object sender, EventArgs e)
		{
			Response.Redirect("HTMLProfile.aspx");
		}
		protected void SaveXMLButton_Click(object sender, EventArgs e)
		{
			Response.Redirect("XMLProfile.aspx");
		}

		protected void GoHomeButton_Click(object sender, EventArgs e)
		{
			Navigator.RedirectToDefaultPage(this.Response);
		}

		private int GetIndex(List<ProvisioningPlan> pprovisioningPlans, string pType)
		{
			int i = pprovisioningPlans.Count - 1;
			while (i >= 0 && pprovisioningPlans[i].Type != pType)
			{
				i--;
			}
			return i;
		}

		private void BindProvisioningPlan()
		{
			ProvisioningPlansDropDownList.DataSource = this.CurrentProfile.ProvisioningPlans;
			ProvisioningPlansDropDownList.DataTextField = "Type";
			ProvisioningPlansDropDownList.DataValueField = "Type";
			ProvisioningPlansDropDownList.DataBind();

			ProvisioningPlansDropDownList.SelectedIndex = GetIndex(this.CurrentProfile.ProvisioningPlans, this.CurrentProfile.ActiveProvisioningPlan.Type);

			DeleteProvisioningPlanImageButton.Visible = (this.CurrentProfile.ProvisioningPlans.Count > 1);
		}

	}

}
