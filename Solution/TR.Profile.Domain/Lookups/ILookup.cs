///
///                           $Id: ILookup.cs 14000 2010-11-25 16:07:32Z neil.middleton $
///              $LastChangedDate: 2010-11-25 16:07:32 +0000 (Thu, 25 Nov 2010) $
///          $LastChangedRevision: 14000 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Domain/Lookups/ILookup.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;

namespace TR.Profile.Domain
{
    public interface ILookup : IPersistent
    {
        string Code { get; }

    }

}
