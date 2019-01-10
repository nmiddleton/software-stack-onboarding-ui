///
///                           $Id: ITICGateway.cs 14326 2010-12-20 18:51:35Z neil.middleton $
///              $LastChangedDate: 2010-12-20 18:51:35 +0000 (Mon, 20 Dec 2010) $
///          $LastChangedRevision: 14326 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICGateway.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using TR.Profile.Domain;
using System.Data;
using TR.Profile.Common.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Data.OracleClient;

namespace TR.Profile.Data
{
    public sealed class ITICGateway
    {
        #region ctor
        public ITICGateway(DbTransaction pTransaction)
        {
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");

            _transaction = pTransaction;
        }
        #endregion

        private DbTransaction _transaction;

		private ITICDbQuery _query = new ITICDbQuery();

        public void Load(ITICHierarchy pHierarchy)
        {
            if (pHierarchy == null)
                return;

			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, _query.SelectAllQuery))
	        {
		        while (reader.Read())
	            {
					using (EventWaitHandle synchronizer = new AutoResetEvent(false))
					{
						Task<InfrastructureLookup> iTask = Task.Factory.StartNew<InfrastructureLookup>(LoadInfrastructure, new HierarchyParallelContext(reader, pHierarchy));
						Task<CapabilityLookup> cTask = Task.Factory.StartNew<CapabilityLookup>(LoadCapability, new HierarchyParallelContext(reader, pHierarchy));
						Task<LogicalSystemGroupLookup> lTask = Task.Factory.StartNew<LogicalSystemGroupLookup>(LoadLogicalSystemGroup, new HierarchyParallelContext(reader, pHierarchy));
						Task.Factory.ContinueWhenAll(new Task[] { iTask, cTask, lTask }, (obj) => { synchronizer.Set(); });
						synchronizer.WaitOne();
						pHierarchy.AddITIC(iTask.Result, cTask.Result, lTask.Result);
					}
	            }
	        }

            pHierarchy.SortLookups();
        }

		public void LoadInfrastructure(InfrastructureLookup pLookup)
		{
			if (pLookup == null)
				return;

			if (!InfrastructureLookup.ValidateCode(pLookup.Code))
				return;

			ICollection<DbParameter> parameters = new List<DbParameter>
			{
				new OracleParameter(ITICDbTable.INFRASTRUCTURE_CODE_COLUMN, OracleType.VarChar) {Value = pLookup.Code}
			};
			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, _query.SelectInfrastructureByCode, parameters))
			{
				if (reader.Read())
				{
					pLookup.Name = DbHelper.GetString(reader, ITICDbTable.INFRASTRUCTURE_NAME_COLUMN);
					pLookup.IsLoaded = true;
				}
			}
		}
		public void LoadCapability(CapabilityLookup pLookup)
		{
			if (pLookup == null)
				return;

			if (!CapabilityLookup.ValidateCode(pLookup.Code))
				return;

			ICollection<DbParameter> parameters = new List<DbParameter>
			{
				new OracleParameter(ITICDbTable.CAPABILITY_CODE_COLUMN, OracleType.VarChar) {Value = pLookup.Code}
			};
			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, _query.SelectCapabilityByCode, parameters))
			{
				if (reader.Read())
				{
					pLookup.Name = DbHelper.GetString(reader, ITICDbTable.CAPABILITY_NAME_COLUMN);
					pLookup.IsLoaded = true;
				}
			}
		}
		public void LoadLogicalSystemGroup(LogicalSystemGroupLookup pLookup)
		{
			if (pLookup == null)
				return;

			if (!LogicalSystemGroupLookup.ValidateCode(pLookup.Code))
				return;

			ICollection<DbParameter> parameters = new List<DbParameter>
			{
				new OracleParameter(ITICDbTable.LOGICAL_SYSTEM_GROUP_CODE_COLUMN, OracleType.VarChar) {Value = pLookup.Code}
			};
			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, _query.SelectLogicalSystemGroupByCode, parameters))
			{
				if (reader.Read())
				{
					pLookup.Name = DbHelper.GetString(reader, ITICDbTable.LOGICAL_SYSTEM_GROUP_NAME_COLUMN);
					pLookup.IsLoaded = true;
				}
			}
		}

        private InfrastructureLookup LoadInfrastructure(object pHelper)
        {
            HierarchyParallelContext helper = pHelper as HierarchyParallelContext;
            if (helper == null)
                return null;

            InfrastructureLookup result = helper.Hierarchy.GenerateInfrastructure(
                DbHelper.GetString(helper.Reader, ITICDbTable.INFRASTRUCTURE_CODE_COLUMN),
                DbHelper.GetString(helper.Reader, ITICDbTable.INFRASTRUCTURE_NAME_COLUMN));
            result.IsLoaded = true;
            return result;
        }
        private CapabilityLookup LoadCapability(object pHelper)
        {
            HierarchyParallelContext helper = pHelper as HierarchyParallelContext;
            if (helper == null)
                return null;

            CapabilityLookup result = helper.Hierarchy.GenerateCapability(
                DbHelper.GetString(helper.Reader, ITICDbTable.CAPABILITY_CODE_COLUMN),
                DbHelper.GetString(helper.Reader, ITICDbTable.CAPABILITY_NAME_COLUMN));
            result.IsLoaded = true;
            return result;
        }
        private LogicalSystemGroupLookup LoadLogicalSystemGroup(object pHelper)
        {
            HierarchyParallelContext helper = pHelper as HierarchyParallelContext;
            if (helper == null)
                return null;

            LogicalSystemGroupLookup result = helper.Hierarchy.GenerateLogicalSystemGroup(
                DbHelper.GetString(helper.Reader, ITICDbTable.LOGICAL_SYSTEM_GROUP_CODE_COLUMN),
                DbHelper.GetString(helper.Reader, ITICDbTable.LOGICAL_SYSTEM_GROUP_NAME_COLUMN));
            result.IsLoaded = true;
            return result;
        }

		#region HierarchyParallelContext
		private sealed class HierarchyParallelContext
        {
            #region ctor
            public HierarchyParallelContext(IDataReader pReader, ITICHierarchy pHierarchy)
            {
                this.Reader = pReader;
                this.Hierarchy = pHierarchy;
            }
            #endregion

            public IDataReader Reader { get; private set; }
            public ITICHierarchy Hierarchy { get; private set; }
        }
		#endregion
    }

}
