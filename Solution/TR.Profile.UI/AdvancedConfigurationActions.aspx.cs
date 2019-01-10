///
///                           $Id: PostConfigurationActions.aspx.cs 14023 2010-11-26 15:17:41Z neil.middleton $
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
	public partial class AdvancedConfigurationActionsPage : Page
	{
		private Exception _exception = null;
		private ICollection<PlanActionLookup> _items;

		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				_items = new List<PlanActionLookup>();
				try
				{
					using (DataTransactionBase dt = new OracleDataTransaction(Configuration.StacksConnectionString))
					{
						ProvisioningPlanExtraGateway gateway = new ProvisioningPlanExtraGateway(dt.Transaction);
						gateway.Load(_items);
					}
				}
				catch (Exception ex)
				{
					_exception = ex;
				}
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
				// Init Controls
				ITICLabel.Text = this.CurrentProfile.ITICCode.Code;
				InfrastructureLabel.Text = this.CurrentProfile.ITICCode.Infrastructure.Name;
				CapabilityLabel.Text = this.CurrentProfile.ITICCode.Capability.Name;
				LogicalSystemGroupLabel.Text = this.CurrentProfile.ITICCode.LogicalSystemGroup.Name;
				FilenameLabel.Text = this.CurrentProfile.Context.Name;

				// Bindings
				WorkflowsRepeater.DataSource = _items;
				WorkflowsRepeater.DataBind();

				// Check CheckBoxes
				foreach (RepeaterItem item in WorkflowsRepeater.Items)
				{
					string code = item.Controls.OfType<HiddenField>().First().Value;
					PlanActionLookup lookup = this.CurrentProfile.ActiveProvisioningPlan.AdvancedConfigurationActions.FirstOrDefault((a) => a.Code == code);
					if (lookup != null)
					{
						if (lookup.IsTextBox)
						{
							item.Controls.OfType<TextBox>().First().Text = lookup.Value;
							item.Controls.OfType<CheckBox>().First().Checked = true;
						}
						else // if(lookup.IsCheckBox)
						{
							item.Controls.OfType<CheckBox>().First().Checked = lookup.IsSelected;
						}
					}
				}

				// Set initial State
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}

		protected void CustomerObjectsButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToCustomerObjectsPage(this.CurrentProfile, this.Response);
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
			this.CurrentProfile.ActiveProvisioningPlan.AdvancedConfigurationActions.Clear();
			foreach (RepeaterItem item in WorkflowsRepeater.Items)
			{
				CheckBox checkBox = item.Controls.OfType<CheckBox>().First();
				if (checkBox.Checked)
				{
					HiddenField hiddenField = item.Controls.OfType<HiddenField>().First();
					TextBox textBox = item.Controls.OfType<TextBox>().First();
					this.CurrentProfile.ActiveProvisioningPlan.AdvancedConfigurationActions.Add(
						new PlanActionLookup(hiddenField.Value, true, textBox.Text) { IsTextBox = textBox.Visible });
				}
			}
		}

	}

}
