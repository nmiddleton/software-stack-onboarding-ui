///
///                           $Id: Test.aspx.cs 14373 2010-12-29 15:14:19Z neil.middleton $
///              $LastChangedDate: 2010-12-29 15:14:19 +0000 (Wed, 29 Dec 2010) $
///          $LastChangedRevision: 14373 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/Test.aspx.cs $
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
    public partial class TestPage : Page
    {

		protected void Page_Load(object sender, EventArgs e)
        {
			if (!this.IsPostBack)
			{
				LocalPropertiesContext.Initialize(this.Session);
			}
		}

	}

}
