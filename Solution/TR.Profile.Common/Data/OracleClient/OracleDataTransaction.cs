
///
///                           $Id: OracleDataTransaction.cs 13936 2010-11-24 14:57:22Z neil.middleton $
///              $LastChangedDate: 2010-11-24 14:57:22 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13936 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/OracleClient/OracleDataTransaction.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.OracleClient;

namespace TR.Profile.Common.Data
{
    public sealed class OracleDataTransaction : DataTransactionBase
    {
        #region ctor
        public OracleDataTransaction(string pConnectionString)
            : base(pConnectionString)
        {
        }
        #endregion

        protected override DbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }

    }

}
