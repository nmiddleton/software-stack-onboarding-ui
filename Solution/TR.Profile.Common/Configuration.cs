///
///                           $Id: Configuration.cs 14799 2011-01-24 14:11:42Z neil.middleton $
///              $LastChangedDate: 2011-01-24 14:11:42 +0000 (Mon, 24 Jan 2011) $
///          $LastChangedRevision: 14799 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Configuration.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TR.Profile.Common
{
    public static class Configuration
    {
        public static string ITICConnectionString { get; set; }
        public static string StacksConnectionString { get; set; }

		public static string HomePageUrl { get; set; }

		public static Version Version { get; set; }

    }

}
