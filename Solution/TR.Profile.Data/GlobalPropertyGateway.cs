///
///                           $Id: GlobalPropertyGateway.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;
using TR.Profile.Domain;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;

namespace TR.Profile.Data
{
	public sealed class GlobalPropertyGateway : DataGatewayBase<GlobalPropertyLookup, GlobalPropertyDbTable, OracleDbQuery<GlobalPropertyDbTable>>
	{
		public GlobalPropertyGateway(DbTransaction pTransaction)
			: base(pTransaction)
		{
			_transaction = pTransaction;
		}

		protected override void Fill(GlobalPropertyLookup pDomain, IDataReader pReader)
		{
			if (pDomain == null || pReader == null)
				return;

			pDomain.Code = SqlHelper.GetString(pReader, GlobalPropertyDbTable.CODE_COLUMN);
			pDomain.Name = SqlHelper.GetString(pReader, GlobalPropertyDbTable.NAME_COLUMN);
			pDomain.Description = SqlHelper.GetString(pReader, GlobalPropertyDbTable.DESCRIPTION_COLUMN);
			pDomain.DataType = SqlHelper.GetString(pReader, GlobalPropertyDbTable.TYPE_COLUMN);
			if (pDomain.IsTextBox)
			{
				pDomain.DefaultValue = SqlHelper.GetString(pReader, GlobalPropertyDbTable.DEFAULT_VALUE_COLUMN);
			}
			else if (pDomain.IsCheckBox)
			{
				try { pDomain.IsSelected = Convert.ToBoolean(SqlHelper.GetString(pReader, GlobalPropertyDbTable.DEFAULT_VALUE_COLUMN));}
				catch { ; }
			}
		}

		protected override ICollection<DbParameter> MapKey(GlobalPropertyLookup pDomain)
		{
			if (pDomain == null)
				return null;

			ICollection<DbParameter> result = new List<DbParameter>()
			{
				new OracleParameter(GlobalPropertyDbTable.CODE_COLUMN, OracleType.VarChar) { Value = pDomain.Code}
			};

			return result;
		}

	}

}
