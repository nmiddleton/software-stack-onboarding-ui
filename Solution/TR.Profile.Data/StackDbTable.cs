///
///                           $Id: StackDbTable.cs 13939 2010-11-24 16:03:20Z neil.middleton $
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
	public class StackDbTable : IDbTable
	{
		public const string ID_COLUMN = "stack_id";
		public const string CODE_COLUMN = "guidisplayname";
		public const string NAME_COLUMN = "stack_name";
		public const string MAJOR_FILTER_COLUMN = "guimajorfilter";
		public const string MINOR_FILTER_COLUMN = "guiminorfilter";
		public const string DESCRIPTION_COLUMN = "guihelp";

		private ICollection<string> _keyColumnNames = new List<string>
        {
            ID_COLUMN
        };
		protected ICollection<string> _nonKeyColumnNames = new List<string>
        {
            CODE_COLUMN,
            NAME_COLUMN,
            MAJOR_FILTER_COLUMN,
			MINOR_FILTER_COLUMN,
			DESCRIPTION_COLUMN
        };

		public virtual string TableName
		{
			get { throw new NotImplementedException(); }
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
