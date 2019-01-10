using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Common;
using TR.Profile.Domain;

namespace TR.Profile.UI
{
	public partial class DocumentPage : Page
	{
		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}

		private string BackToPage
		{
			get { return this.Request.Params[Navigator.BACK_TO_PARAMETER]; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				string xsltPath = System.IO.Path.Combine(this.Request.PhysicalApplicationPath, "Profile.xslt");

				try
				{
					DocumentLabel.Text = XmlHelper.Transform(IOHelper.ReadText(xsltPath), this.CurrentProfile.Xml);
				}
				catch (Exception ex)
				{
					UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.Error, ex.Message);
					return;
				}

				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
			else
			{
				UIHelper.SetMessage(MessageImage, MessageLabel, MessageType.None, null);
			}
		}

		protected void Back_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(BackToPage))
				return;

			this.Response.Redirect(BackToPage);
		}

	}

}
