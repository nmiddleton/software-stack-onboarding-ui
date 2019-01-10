///
///                           $Id: ProvisioningPlanExtraDbTable.cs 13939 2010-11-24 16:03:20Z neil.middleton $
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
	public sealed class ProvisioningPlanExtraDbTable : IDbTable
	{
		private const string TABLE = "green_channel_workflows_view";

		public const string ID_COLUMN = "tcdriverid";
		public const string CODE_COLUMN = "workflowname";
		public const string NAME_COLUMN = "guidisplayname";
		public const string DESCRIPTION_COLUMN = "guihelp";
		public const string TYPE_COLUMN = "tcdrivername";
		public const string IS_SELECTED_BY_DEFAULT_COLUMN = "guiselected";
		public const string IS_TEXTBOX_COLUMN = "guitextboxvisible";
		public const string DEFAULL_VALUE_COLUMN = "guitextboxdefaultvalue";
		public const string RUN_ORDER_COLUMN = "guiforcerunorder";

		public const string IS_SELECTED_VALUE = "TRUE";
		public const string FILTER_INITIAL_VALUE = "ProvisioningPlan_Initial";
		public const string FILTER_FINAL_VALUE = "ProvisioningPlan_Final";
		public const string FILTER_ADVANCED_VALUE = "ProvisioningPlan_Advanced";

		private ICollection<string> _keyColumnNames = new List<string>
        {
            ID_COLUMN
        };
		private ICollection<string> _nonKeyColumnNames = new List<string>
        {
            CODE_COLUMN,
            NAME_COLUMN,
			DESCRIPTION_COLUMN,
            TYPE_COLUMN,
			IS_SELECTED_BY_DEFAULT_COLUMN,
			IS_TEXTBOX_COLUMN,
			DEFAULL_VALUE_COLUMN,
			RUN_ORDER_COLUMN
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
