///
///                           $Id: OracleHelper.cs 14165 2010-12-03 15:09:09Z neil.middleton $
///              $LastChangedDate: 2010-12-03 15:09:09 +0000 (Fri, 03 Dec 2010) $
///          $LastChangedRevision: 14165 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/OracleClient/OracleHelper.cs $
///

#define DEBUG_ME

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;
using System.Text;

namespace TR.Profile.Common.Data
{
    public sealed class OracleHelper : DbHelper
	{
#if DEBUG_ME && DEBUG
		private const string DebugCategory = "[TR.Profile.Common.Data.OracleHelper]";
#endif
        private const char PORT_DELIMITER = ',';


        public static void FillDataSet(DbTransaction transaction, string query, DataSet ds)
        {
            FillDataSet(transaction, query, null, ds);
        }

        public static void FillDataSet(DbTransaction transaction, string query, DbParameter[] parameters, DataSet ds)
		{
#if DEBUG_ME && DEBUG
			System.Diagnostics.Debug.WriteLine(string.Empty);
            PrintParameters(parameters);
            System.Diagnostics.Debug.WriteLine(string.Format("query:{0}", query), DebugCategory);
#endif

            using (DbCommand command = DbHelper.PrepareCommand(transaction, query, parameters))
            {
                OracleDataAdapter adapter = new OracleDataAdapter((OracleCommand)command);
                adapter.Fill(ds);
            }
        }

        public static string BuildConnectionString(string pHost, int pPort, string pService, string pLogin, string pPassword)
        {
            if (pHost.Contains(PORT_DELIMITER))
            {
                StringBuilder addressList = new StringBuilder();
                foreach (string host in pHost.Split(new char[] { PORT_DELIMITER }))
                {
                    addressList.AppendFormat("(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))", host, pPort);
                }
                return String.Format("SERVER=(DESCRIPTION=(FAILOVER=OFF)(ADDRESS_LIST=(LOAD_BALANCE=ON){0})(CONNECT_DATA=(SERVICE_NAME={2})));uid={3};pwd={4};", addressList, null, pService, pLogin, pPassword);
            }
            else
            {
                return String.Format("SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));uid={3};pwd={4};", pHost, pPort, pService, pLogin, pPassword);
                //return String.Format("SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})));uid={3};pwd={4};", pHost, pPort, pService, pLogin, pPassword);
                //return String.Format("SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));uid={3};pwd={4};", pHost, pPort, pService, pLogin, pPassword);
                //return String.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};", pHost, pPort, pService, pLogin, pPassword);
            }
        }

#if DEBUG_ME && DEBUG
		static void PrintParameters(ICollection<DbParameter> parameters)
        {
            if (parameters == null)
                return;

            foreach (var item in parameters)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("query parameter; name:{0}, value:{1}", item.ParameterName, item.Value), DebugCategory);
            }
        }
#endif

    }

}
