///
///                           $Id: Default.master.cs 14443 2011-01-10 18:01:28Z neil.middleton $
///              $LastChangedDate: 2011-01-10 18:01:28 +0000 (Mon, 10 Jan 2011) $
///          $LastChangedRevision: 14443 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/Default.master.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultMasterPage : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.VersionLabel.Text = string.Format("#{0}", TR.Profile.Common.Configuration.Version);
        }
    }

}
