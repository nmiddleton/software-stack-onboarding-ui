///
///                           $Id: GlobalPropertyDbTable.cs 13939 2010-11-24 16:03:20Z neil.middleton $
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
	public sealed class GlobalPropertyDbTable : IDbTable
	{
		private const string TABLE = "prov_plan_property_definition";

		public const string CODE_COLUMN = "property_name";
		public const string NAME_COLUMN = "guidescription";
		public const string DESCRIPTION_COLUMN = "help";
		public const string TYPE_COLUMN = "guitype";
		public const string DEFAULT_VALUE_COLUMN = "default_value";

		private ICollection<string> _keyColumnNames = new List<string>
        {
            CODE_COLUMN
        };
		private ICollection<string> _nonKeyColumnNames = new List<string>
        {
            NAME_COLUMN,
			DESCRIPTION_COLUMN,
			TYPE_COLUMN,
			DEFAULT_VALUE_COLUMN
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
