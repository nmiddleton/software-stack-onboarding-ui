using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;

namespace TR.Profile.UI
{
	public partial class NewProvisioningPlanPage : Page
	{
		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				NameTextBox.Text = this.CurrentProfile.ActiveProvisioningPlan.Type;
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, string.Empty);
			}
		}

		protected void AcceptButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(NameTextBox.Text))
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, "Specify name");
				return;
			}

			if (this.CurrentProfile.ProvisioningPlans.Any(pp=>pp.Type == NameTextBox.Text))
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, "Specify another name");
				return;
			}

			this.CurrentProfile.ActiveProvisioningPlan = this.CurrentProfile.DuplicateProvisioningPlan(
				NameTextBox.Text,
				this.CurrentProfile.ActiveProvisioningPlan);

			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}
		protected void CancelButton_Click(object sender, EventArgs e)
		{
			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}
	
	}

}
