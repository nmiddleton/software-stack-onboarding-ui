using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;
using TR.Profile.Common;
using System.IO;

namespace TR.Profile.UI
{
	public partial class ModifyProfilePage : Page
	{
		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}
		public string XsdPath
		{
			get { return Path.Combine(this.Request.PhysicalApplicationPath, SettingsHelper.XML_SCHEMA_FILENAME); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
		}

		protected void UploadButton_Click(object sender, EventArgs e)
		{
			if (!XmlFileUpload.HasFile)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Warning, "Please provide a file name");
				return;
			}

			string xml = UIHelper.UploadXml(XmlFileUpload);
			try
			{
				XmlHelper.ValidateXml(xml, this.XsdPath);
			}
			catch (Exception ex)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, ex.Message);
				return;
			}

			this.CurrentProfile = null;
			try
			{
				this.CurrentProfile = Serializer.Deserialize<Domain.Profile>(xml);
			}
			catch (Exception ex)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, ex.Message);
				return;
			}
			this.CurrentProfile.ActiveProvisioningPlan = this.CurrentProfile.ProvisioningPlans[0];
			this.CurrentProfile.SuppressXmlPreparation();

			try
			{
				UIHelper.UpdateITIC(this.CurrentProfile.ITICCode);
			}
			catch (Exception ex)
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, ex.Message);
				return;
			}

			Navigator.RedirectToSummaryPage(this.CurrentProfile, this.Response);
		}

		protected void HomeLinkButton_Click(object sender, EventArgs e)
		{
			Navigator.RedirectToHomePage(this.Response);
		}

	}

}
