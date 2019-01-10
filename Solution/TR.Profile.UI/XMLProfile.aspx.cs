///
///                           $Id: XMLProfile.cs 14016 2010-11-26 14:07:39Z neil.middleton $
///              $LastChangedDate: 2010-11-26 14:07:39 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14016 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/MessageType.cs $
///

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
	public partial class XMLProfilePage : Page
	{
		private const string PROFILES_FOLDER = "Profiles";
		private const string EXTENSION = "xml";

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

				// Save a copy
				IOHelper.WriteText(CreateFilename(this.CurrentProfile), xml);

				this.Response.Clear();
				this.Response.AddHeader(CONTENT_DISPOSITION_HEADER, CreateFilenameString(this.CurrentProfile));
				this.Response.AddHeader(CONTENT_LENGTH_HEADER, xml.Length.ToString());
				this.Response.ContentType = CONTENT_TYPE;
				this.Response.Write(this.CurrentProfile.Xml);
				this.Response.Flush();
				this.Response.End();
			}
		}

		private string CreateFilenameString(Domain.Profile pProfile)
		{
			if (pProfile == null)
				return null;

			return string.Format("attachment; filename={0}.{1}", pProfile.FileName, EXTENSION);
		}
		private string CreateFilename(Domain.Profile pProfile)
		{
			if (pProfile == null)
				return null;

			string result = Path.Combine(this.Request.PhysicalApplicationPath, PROFILES_FOLDER);
			result = Path.Combine(result, string.Format("{0}.{1}", this.CurrentProfile.FileName, EXTENSION));

			return result;
		}

	}

}
