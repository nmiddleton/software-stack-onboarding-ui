///
///                           $Id: IDbTable.cs 14023 2010-11-26 15:17:41Z neil.middleton $
///              $LastChangedDate: 2010-11-26 15:17:41 +0000 (Fri, 26 Nov 2010) $
///          $LastChangedRevision: 14023 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/IDbTable.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR.Profile.Common.Data
{
    public interface IDbTable
    {
        string TableName { get; }

        ICollection<string> KeyColumnNames { get; }
        ICollection<string> NonKeyColumnNames { get; }
        ICollection<string> ColumnNames { get; }					// should be KeyColumnNames + NonKeyColumnNames

    }

}
