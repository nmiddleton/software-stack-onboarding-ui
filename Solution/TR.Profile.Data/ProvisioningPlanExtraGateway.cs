///
///                           $Id: ProvisioningPlanExtraGateway.cs 14089 2010-11-30 10:16:09Z neil.middleton $
///              $LastChangedDate: 2010-11-30 10:16:09 +0000 (Tue, 30 Nov 2010) $
///          $LastChangedRevision: 14089 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICGateway.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using TR.Profile.Domain;
using TR.Profile.Common;
using TR.Profile.Common.Data;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace TR.Profile.Data
{
	public sealed class ProvisioningPlanExtraGateway
	{
		#region ctor
		public ProvisioningPlanExtraGateway(DbTransaction pTransaction)
        {
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");

            _transaction = pTransaction;
        }
        #endregion

        private DbTransaction _transaction;

		public void Load(ICollection<PlanActionLookup> pInitialWorkflows, ICollection<PlanActionLookup> pCommonPlanActions, ICollection<PlanActionLookup> pFinalWorkflows)
		{
			if (pInitialWorkflows == null || pCommonPlanActions == null || pFinalWorkflows == null)
				return;

			DbQueryBase<ProvisioningPlanExtraDbTable> query = new OracleDbQuery<ProvisioningPlanExtraDbTable>();
			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, query.SelectAllQuery))
			{
				while (reader.Read())
				{
					using (EventWaitHandle synchronizer = new AutoResetEvent(false))
					{
						Task iTask = Task.Factory.StartNew(LoadItems, new HierarchyParallelContext(reader, pInitialWorkflows, ProvisioningPlanExtraDbTable.FILTER_INITIAL_VALUE));
						Task lTask = Task.Factory.StartNew(LoadItems, new HierarchyParallelContext(reader, pFinalWorkflows, ProvisioningPlanExtraDbTable.FILTER_FINAL_VALUE));
						Task.Factory.ContinueWhenAll(new Task[] { iTask, lTask }, (obj) => { synchronizer.Set(); });
						synchronizer.WaitOne();
					}
				}
			}
			PlanActionLookup item = new PlanActionLookup()
			{
				Code = "Tivoli Provisioning Manager Inventory Discovery",
				Name = "Tivoli Provisioning Manager Inventory Discovery",
				Description = "HELP: Perform Full Inventory Scan",
				IsLoaded = true,
				IsSelected = true
			};
			pCommonPlanActions.Add(item);
		}
		public void Load(ICollection<PlanActionLookup> pPostConfigurationActions)
		{
			if (pPostConfigurationActions == null)
				return;

			DbQueryBase<ProvisioningPlanExtraDbTable> query = new OracleDbQuery<ProvisioningPlanExtraDbTable>();
			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, query.SelectAllQuery))
			{
				while (reader.Read())
				{
					LoadItems(new HierarchyParallelContext(reader, pPostConfigurationActions, ProvisioningPlanExtraDbTable.FILTER_ADVANCED_VALUE));
				}
			}
		}

		private void LoadItems(object pContext)
		{
			HierarchyParallelContext context = pContext as HierarchyParallelContext;
			if (context == null)
				return;

			if (DbHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.TYPE_COLUMN) != context.Filter)
				return;

			string description = DbHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.DESCRIPTION_COLUMN);
			PlanActionLookup item = new PlanActionLookup()
			{
				Code = DbHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.CODE_COLUMN),
				Name = DbHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.NAME_COLUMN),
				Description = (description != null)? description.Replace('\'', ' ') : string.Empty,
				DefaultValue = DbHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.DEFAULL_VALUE_COLUMN),
				RunOrder = DbHelper.GetNullableDecimal(context.Reader, ProvisioningPlanExtraDbTable.RUN_ORDER_COLUMN),
				IsLoaded = true
			};
			try { item.IsSelected = Convert.ToBoolean(SqlHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.IS_SELECTED_BY_DEFAULT_COLUMN)); }
			catch { item.IsSelected = false; }
			try { item.IsTextBox = Convert.ToBoolean(SqlHelper.GetString(context.Reader, ProvisioningPlanExtraDbTable.IS_TEXTBOX_COLUMN)); }
			catch { item.IsTextBox = false; }

			context.Items.Add(item);
		}

		#region HierarchyParallelContext
		private sealed class HierarchyParallelContext
		{
			#region ctor
			public HierarchyParallelContext(IDataReader pReader, ICollection<PlanActionLookup> pItems, string pFilter)
			{
				this.Reader = pReader;
				this.Items = pItems;
				this.Filter = pFilter;
			}
			#endregion

			public IDataReader Reader { get; private set; }
			public ICollection<PlanActionLookup> Items { get; private set; }
			public string Filter { get; private set; }
		}
		#endregion

	}

}
