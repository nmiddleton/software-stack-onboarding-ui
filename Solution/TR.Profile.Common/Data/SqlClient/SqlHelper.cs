///
///                           $Id: SqlHelper.cs 14589 2011-01-13 12:15:52Z neil.middleton $
///              $LastChangedDate: 2011-01-13 12:15:52 +0000 (Thu, 13 Jan 2011) $
///          $LastChangedRevision: 14589 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/SqlClient/SqlHelper.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace TR.Profile.Common.Data
{
    public sealed class SqlHelper : DbHelper
    {
        public static void AddParameter(ICollection<DbParameter> pParameters, string pName, byte[] pValue)
        {
            if (pParameters == null || String.IsNullOrEmpty(pName))
                return;

            DbParameter parameter;
            if (pValue == null)
            {
                parameter = new SqlParameter(pName, DBNull.Value)
                {
                    SqlDbType = SqlDbType.Image
                };
            }
            else
            {
                parameter = new SqlParameter(pName, pValue);
            }
            pParameters.Add(parameter);
        }
        public static void AddParameter(ICollection<DbParameter> pParameters, string pName, string pValue)
        {
            if (pParameters == null || String.IsNullOrEmpty(pName))
                return;

            DbParameter parameter;
            if (pValue == null)
            {
                parameter = new SqlParameter(pName, DBNull.Value)
                {
                    SqlDbType = SqlDbType.VarChar
                };
            }
            else
            {
                parameter = new SqlParameter(pName, pValue);
            }
            pParameters.Add(parameter);
        }
        public static void AddParameter(ICollection<DbParameter> pParameters, string pName, Guid pValue)
        {
            if (pParameters == null || String.IsNullOrEmpty(pName))
                return;

            pParameters.Add(new SqlParameter(pName, pValue));
        }
        public static void AddParameter(ICollection<DbParameter> pParameters, string pName, int pValue)
        {
            if (pParameters == null || String.IsNullOrEmpty(pName))
                return;

            pParameters.Add(new SqlParameter(pName, pValue));
        }

    }

}
