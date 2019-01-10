///
///                           $Id: Global.asax.cs 14799 2011-01-24 14:11:42Z neil.middleton $
///              $LastChangedDate: 2011-01-24 14:11:42 +0000 (Mon, 24 Jan 2011) $
///          $LastChangedRevision: 14799 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/App_Code/Global.asax.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TR.Profile.Common;

namespace TR.Profile.UI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Inject configuration
            Configuration.Version = SettingsHelper.Instance.Version;

            Configuration.ITICConnectionString = SettingsHelper.Instance.ITICConnectionString;
            Configuration.StacksConnectionString = SettingsHelper.Instance.StacksConnectionString;

			Configuration.HomePageUrl = SettingsHelper.Instance.HomePageUrl;
        }

        void Application_End(object sender, EventArgs e)
        {
        }
        void Application_Error(object sender, EventArgs e)
        {
        }
        void Session_Start(object sender, EventArgs e)
        {
        }
        void Session_End(object sender, EventArgs e)
        {
        }

    }

}
