///
///                           $Id: AdvancedSummary.aspx.cs 14023 2010-11-26 15:17:41Z neil.middleton $
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

namespace TR.Profile.UI
{
	public partial class AdvancedSummaryPage : Page
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
				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				ProfileNameLabel.Text = this.CurrentProfile.Context.Name;

				AdvancedPlanPropertiesRepeater.DataSource = this.CurrentProfile.ActiveProvisioningPlan.Properties;
				AdvancedPlanPropertiesRepeater.DataBind();
			}
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
		}

		protected void GoBackButton_Click(object sender, EventArgs e)
		{
			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}
		protected void AcceptButton_Click(object sender, EventArgs e)
		{
			UpdateGlobalProperties();
			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}

		private void UpdateGlobalProperties()
		{
			List<GlobalPropertyLookup> properties = this.CurrentProfile.ActiveProvisioningPlan.Properties;
			foreach (RepeaterItem item in AdvancedPlanPropertiesRepeater.Items)
			{
				string code = item.Controls.OfType<HiddenField>().First().Value;
				GlobalPropertyLookup property = properties.First((gp) => gp.Code == code);
				if (property.IsTextBox)
				{	// text box
					property.DefaultValue = item.Controls.OfType<TextBox>().First().Text;
				}
				else
				{	// check box
					property.IsSelected = item.Controls.OfType<CheckBox>().First().Checked;
				}
			}
			this.CurrentProfile.ActiveProvisioningPlan.Properties = properties;
		}

	}

}
