using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;

namespace TR.Profile.Data
{
	public sealed class ITICDbQuery : OracleDbQuery<ITICDbTable>
	{

		public string SelectInfrastructureByCode
		{
			get
			{
				return string.Format(
					"SELECT {0} FROM {1} WHERE {2}=:{2}", 
					/*0*/CreateTableColumnsString(Table.ColumnNames), /*1*/Table.TableName,
					/*2*/ ITICDbTable.INFRASTRUCTURE_CODE_COLUMN);
			}
		}
		public string SelectCapabilityByCode
		{
			get
			{
				return string.Format(
					"SELECT {0} FROM {1} WHERE {2}=:{2}",
					/*0*/CreateTableColumnsString(Table.ColumnNames), /*1*/Table.TableName,
					/*2*/ ITICDbTable.CAPABILITY_CODE_COLUMN);
			}
		}
		public string SelectLogicalSystemGroupByCode
		{
			get
			{
				return string.Format(
					"SELECT {0} FROM {1} WHERE {2}=:{2}",
					/*0*/CreateTableColumnsString(Table.ColumnNames), /*1*/Table.TableName,
					/*2*/ ITICDbTable.LOGICAL_SYSTEM_GROUP_CODE_COLUMN);
			}
		}

	}

}
