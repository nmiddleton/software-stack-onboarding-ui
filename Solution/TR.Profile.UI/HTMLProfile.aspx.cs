using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TR.Profile.Domain;
using TR.Profile.Common;

namespace TR.Profile.UI
{
	public partial class HTMLProfilePage : Page
	{
		private const string CONTENT_TYPE = "text/plain";
		private const string CONTENT_DISPOSITION_HEADER = "Content-Disposition";
		private const string CONTENT_LENGTH_HEADER = "Content-Length";

		private Domain.Profile CurrentProfile
		{
			get { return this.Session[UIHelper.PROFILE_SESSION_KEY] as Domain.Profile; }
			set { this.Session[UIHelper.PROFILE_SESSION_KEY] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.CurrentProfile != null)
			{
				string xml = this.CurrentProfile.Xml;

				this.Response.Clear();
				this.Response.AddHeader(CONTENT_DISPOSITION_HEADER, CreateFilenameString(this.CurrentProfile));
				this.Response.AddHeader(CONTENT_LENGTH_HEADER, xml.Length.ToString());
				this.Response.ContentType = CONTENT_TYPE;

				try
				{
					string xsltPath = System.IO.Path.Combine(this.Request.PhysicalApplicationPath, "Profile.xslt");
					string html = XmlHelper.Transform(IOHelper.ReadText(xsltPath), this.CurrentProfile.Xml);
					this.Response.Write(html);
				}
				catch (Exception ex)
				{
					this.Response.Write(ex.Message);
				}

				this.Response.Flush();
				this.Response.End();
			}
		}

		private string CreateFilenameString(Domain.Profile pProfile)
		{
			if (pProfile == null)
				return null;

			return string.Format("attachment; filename={0}.html", pProfile.FileName);
		}

	}

}
