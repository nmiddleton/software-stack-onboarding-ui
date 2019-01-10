///
///                           $Id: PlatformStackGateway.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using TR.Profile.Common.Data;
using System.Data.Common;
using System.Data;
using TR.Profile.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace TR.Profile.Data
{
	public sealed class PlatformStackGateway : StackGatewayBase
	{
		#region ctor
		public PlatformStackGateway(DbTransaction pTransaction) : base(pTransaction)
        {
			;
        }
        #endregion

		private OracleDbQuery<PlatformStackDbTable> _query = new OracleDbQuery<PlatformStackDbTable>();

		protected override string SelectAllQuery
		{
			get { return _query.SelectAllQuery; }
		}

		protected override void PopulateHierarchy(IDataReader pReader, StackHierarchy pHierarchy)
		{
			if (pReader == null || pHierarchy == null)
				return;

			PlatformStackHierarchy hierarchy = pHierarchy as PlatformStackHierarchy;
			if (hierarchy == null)
				return;

			Task<FilterLookup> platformTask;
			Task<FilterLookup> versionTask;
			Task<FilterLookup> architectureTask;
			Task<FilterLookup> buildTask;
			Task<StackLookup> stackTask;
			using (EventWaitHandle synchronizer = new AutoResetEvent(false))
			{
				platformTask = Task.Factory.StartNew<FilterLookup>(base.LoadMajorFilter, new HierarchyParallelHelper(pReader, pHierarchy));
				versionTask = Task.Factory.StartNew<FilterLookup>(base.LoadMinorFilter, new HierarchyParallelHelper(pReader, pHierarchy));
				architectureTask = Task.Factory.StartNew<FilterLookup>(LoadArchitectureFilter, new HierarchyParallelHelper(pReader, pHierarchy));
				buildTask = Task.Factory.StartNew<FilterLookup>(LoadBuildFilter, new HierarchyParallelHelper(pReader, pHierarchy));
				stackTask = Task.Factory.StartNew<StackLookup>(base.LoadPlatformStack, new HierarchyParallelHelper(pReader, pHierarchy));
				Task.Factory.ContinueWhenAll(new Task[] { platformTask, versionTask, stackTask }, (obj) => { synchronizer.Set(); });
				synchronizer.WaitOne();
			}
			hierarchy.AddPlatformStack(platformTask.Result, versionTask.Result, architectureTask.Result, buildTask.Result, stackTask.Result);
		}

		private FilterLookup LoadArchitectureFilter(object pHelper)
		{
			HierarchyParallelHelper helper = pHelper as HierarchyParallelHelper;
			if (helper == null)
				return null;
			PlatformStackHierarchy hierarchy = helper.Hierarchy as PlatformStackHierarchy;
			if (hierarchy == null)
				return null;

			FilterLookup result = hierarchy.GenerateArchitectureFilter(
				DbHelper.GetString(helper.Reader, PlatformStackDbTable.ARCHITECTURE_COLUMN));
			result.IsLoaded = true;
			return result;
		}
		private FilterLookup LoadBuildFilter(object pHelper)
		{
			HierarchyParallelHelper helper = pHelper as HierarchyParallelHelper;
			if (helper == null)
				return null;
			PlatformStackHierarchy hierarchy = helper.Hierarchy as PlatformStackHierarchy;
			if (hierarchy == null)
				return null;

			FilterLookup result = hierarchy.GenerateBuildFilter(
				DbHelper.GetString(helper.Reader, PlatformStackDbTable.BUILD_COLUMN));
			result.IsLoaded = true;
			return result;
		}

	}

}
