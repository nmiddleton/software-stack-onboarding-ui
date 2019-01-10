///
///                           $Id: LocalPropertyGateway.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.OracleClient;
using TR.Profile.Common.Data;
using System.Data;
using TR.Profile.Domain;

namespace TR.Profile.Data
{
	public sealed class LocalPropertyGateway : DataGatewayBase<LocalPropertyLookup, LocalPropertyDbTable, LocalPropertiesDbQuery>
	{
		public LocalPropertyGateway(DbTransaction pTransaction) : base(pTransaction)
		{
			_transaction = pTransaction;
		}

		protected override void Fill(LocalPropertyLookup pDomain, IDataReader pReader)
		{
			if (pDomain == null || pReader == null)
				return;

			pDomain.InfrastructurePrefix = SqlHelper.GetString(pReader, LocalPropertyDbTable.DATACENTRE_PREFIX_COLUMN);
			pDomain.InfrastructureSuffix = SqlHelper.GetString(pReader, LocalPropertyDbTable.DATACENTRE_SUFFIX_COLUMN);
			pDomain.Infrastructure = string.Format("{0}_{1}", pDomain.InfrastructurePrefix, pDomain.InfrastructureSuffix);
			pDomain.DatacentreSuffix = SqlHelper.GetString(pReader, LocalPropertyDbTable.DNS_DATACENTRE_COLUMN);
			pDomain.ClientsitedSuffix = SqlHelper.GetString(pReader, LocalPropertyDbTable.DNS_CLIENT_SIDE_COLUMN);
		}

		protected override ICollection<DbParameter> MapKey(LocalPropertyLookup pDomain)
		{
			if (pDomain == null)
				return null;

			ICollection<DbParameter> result = new List<DbParameter>()
			{
				new OracleParameter(LocalPropertyDbTable.DATACENTRE_PREFIX_COLUMN, OracleType.VarChar) { Value = pDomain.InfrastructurePrefix },
				new OracleParameter(LocalPropertyDbTable.DATACENTRE_SUFFIX_COLUMN, OracleType.VarChar) { Value = pDomain.InfrastructureSuffix }
			};

			return result;
		}

	}

}
