///
///                           $Id: LocalPropertyDbTable.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;

namespace TR.Profile.Data
{
	public sealed class LocalPropertyDbTable : IDbTable
	{
		private const string TABLE = "gmi_datacentre";

		public const string DATACENTRE_PREFIX_COLUMN = "gmi_datacentre";
		public const string DATACENTRE_SUFFIX_COLUMN = "gmi_environment";
		public const string DNS_DATACENTRE_COLUMN = "dns_dc";
		public const string DNS_CLIENT_SIDE_COLUMN = "dns_cs";

		private ICollection<string> _keyColumnNames = new List<string>
        {
            DATACENTRE_PREFIX_COLUMN,
            DATACENTRE_SUFFIX_COLUMN
        };
		private ICollection<string> _nonKeyColumnNames = new List<string>
        {
            DNS_DATACENTRE_COLUMN,
            DNS_CLIENT_SIDE_COLUMN
        };

		public string TableName
		{
			get { return TABLE; }
		}

		public ICollection<string> KeyColumnNames
		{
			get { return this._keyColumnNames; }
		}

		public ICollection<string> NonKeyColumnNames
		{
			get { return this._nonKeyColumnNames; }
		}

		public ICollection<string> ColumnNames
		{
			get
			{
				return (
					from c in _keyColumnNames select c)
					.Union(
					from c in _nonKeyColumnNames select c)
					.ToList<string>();
			}
		}

	}

}
