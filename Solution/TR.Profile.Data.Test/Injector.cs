///
///                           $Id: Injector.cs 14799 2011-01-24 14:11:42Z neil.middleton $
///              $LastChangedDate: 2011-01-24 14:11:42 +0000 (Mon, 24 Jan 2011) $
///          $LastChangedRevision: 14799 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data.Test/Injector.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common;
using TR.Profile.Common.Data;

namespace TR.Profile.Data.Test
{
    internal static class Injector
    {
        const string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS; Integrated Security=SSPI; AttachDBFilename=C:\Projects\GMI\DB\GMI.mdf;User Instance=True;";

        //const string HOST = "GMIB-GMITPMC01_SUPP";
        const string HOST = "132.5.197.174";
        const int PORT = 1521;
        const string SERVICE = "ITPMDEV";

        const string ITIC_LOGIN = "gmi";
        const string ITIC_PASSWORD = "gmi";

        const string STACKS_LOGIN = "tiodb";
        const string STACKS_PASSWORD = "think4me";

        internal static void InjectConfiguration()
        {
            Configuration.ITICConnectionString = OracleHelper.BuildConnectionString(HOST, PORT, SERVICE, ITIC_LOGIN, ITIC_PASSWORD);
            Configuration.StacksConnectionString = OracleHelper.BuildConnectionString(HOST, PORT, SERVICE, STACKS_LOGIN, STACKS_PASSWORD);
        }

    }

}
