using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;
using TR.Profile.Common.Data;
using TR.Profile.Common;
using TR.Profile.Data;

namespace TR.Profile.UI
{
	public partial class DomainNameResolutionPage : Page
	{
		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}

#warning exception

		protected void Page_Init(object sender, EventArgs e)
		{
			;
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

				// Check CheckBoxes
				if (this.CurrentProfile.LocalProperties.Count == 0)
				{
					OptionsRadioButtonList.SelectedIndex = 0;
					LocalPropertiesContext.Initialize(this.Session);
				}
				else if (this.CurrentProfile.LocalProperties.Count > 0 && this.CurrentProfile.LocalProperties.All((lp) => String.IsNullOrWhiteSpace(lp.DatacentreSuffix)))
				{
					OptionsRadioButtonList.SelectedIndex = 1;
					LocalPropertiesContext.Initialize(this.Session);
				}
				else
				{
					OptionsRadioButtonList.SelectedIndex = 2;
					if (this.CurrentProfile.LocalProperties.Count == 0)
					{
						this.CurrentProfile.LocalProperties = LocalPropertiesContext.Load();
					}
					LocalPropertiesContext.Initialize(this.Session, this.CurrentProfile.LocalProperties);
				}
				SetState();

				// Set initial State
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}

		protected void CustomerObjectsButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToCustomerObjectsPage(this.CurrentProfile, this.Response);
		}
		protected void PostConfigurationActionsButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToAdvancedConfigurationActionsPage(this.CurrentProfile, this.Response);
		}
		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			UpdateProfile();
			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}

		protected void OptionsRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetState();
		}

		private void SetState()
		{
			if (OptionsRadioButtonList.SelectedIndex == 0)
			{	// Default [3 columns/RO]
				OptionsGridView.Visible = true;
				OptionsGridView.Columns[0].Visible = true;	// Infrastructure
				OptionsGridView.Columns[1].Visible = true;	// Custom / Datacentre Suffix
				OptionsGridView.Columns[1].HeaderText = "Datacentre Suffix";
				OptionsGridView.Columns[2].Visible = true;	// Clientsited Suffix
				OptionsGridView.Columns[3].Visible = false;	// Edit / Delete
			}
			else if (OptionsRadioButtonList.SelectedIndex == 1)
			{	// Host file [hidden]
				OptionsGridView.Visible = false;
			}
			else if (OptionsRadioButtonList.SelectedIndex == 2)
			{	// Manual DNS [2 columns/2nd-ed]
				OptionsGridView.Visible = true;
				OptionsGridView.Columns[0].Visible = true;	// Infrastructure
				OptionsGridView.Columns[1].Visible = true;	// Custom / Datacentre Suffix
				OptionsGridView.Columns[1].HeaderText = "Custom Suffix";
				OptionsGridView.Columns[2].Visible = false;	// Clientsited Suffix
				OptionsGridView.Columns[3].Visible = true;	// Edit / Delete
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		private void UpdateProfile()
		{
			this.CurrentProfile.LocalProperties.Clear();
			if (OptionsRadioButtonList.SelectedIndex == 0)
			{	// Default [nothing]
			}
			else if (OptionsRadioButtonList.SelectedIndex == 1)
			{	// Host file [blank]
				foreach (LocalPropertyLookup item in LocalPropertiesContext.Load())
				{
					item.DatacentreSuffix = string.Empty;
					this.CurrentProfile.LocalProperties.Add(item);
				}
			}
			else if (OptionsRadioButtonList.SelectedIndex == 2)
			{	// Manual DNS [copy]
				foreach (LocalPropertyLookup item in LocalPropertiesContext.Get(this.Session))
				{
					this.CurrentProfile.LocalProperties.Add(item);
				}
			}
			else
			{
				throw new NotImplementedException();
			}
		}

	}

}
