using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;

namespace TR.Profile.Data
{
	public sealed class LocalPropertiesDbQuery : OracleDbQuery<LocalPropertyDbTable>
	{
		public override string SelectAllQuery
		{
			get
			{
				return string.Format("{0} ORDER BY {1}, {2}", 
					/*0*/base.SelectAllQuery,
					/*1*/LocalPropertyDbTable.DATACENTRE_PREFIX_COLUMN, /*1*/LocalPropertyDbTable.DATACENTRE_SUFFIX_COLUMN);
			}
		}

	}

}
