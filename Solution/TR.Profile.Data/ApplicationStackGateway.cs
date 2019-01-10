///
///                           $Id: ApplicationStackGateway.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICDbTable.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using TR.Profile.Common.Data;
using System.Data;
using TR.Profile.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace TR.Profile.Data
{
	public sealed class ApplicationStackGateway : StackGatewayBase
	{
		#region ctor
		public ApplicationStackGateway(DbTransaction pTransaction)
			: base(pTransaction)
        {
			;
        }
        #endregion

		private OracleDbQuery<ApplicationStackDbTable> _query = new OracleDbQuery<ApplicationStackDbTable>();

		protected override string SelectAllQuery
		{
			get { return _query.SelectAllQuery; }
		}

		protected override void PopulateHierarchy(IDataReader pReader, StackHierarchy pHierarchy)
		{
			if (pReader == null || pHierarchy == null)
				return;

			Task<FilterLookup> majorTask;
			Task<FilterLookup> minorTask;
			Task<StackLookup> stackTask;
			using (EventWaitHandle synchronizer = new AutoResetEvent(false))
			{
				majorTask = Task.Factory.StartNew<FilterLookup>(base.LoadMajorFilter, new HierarchyParallelHelper(pReader, pHierarchy));
				minorTask = Task.Factory.StartNew<FilterLookup>(base.LoadMinorFilter, new HierarchyParallelHelper(pReader, pHierarchy));
				stackTask = Task.Factory.StartNew<StackLookup>(base.LoadPlatformStack, new HierarchyParallelHelper(pReader, pHierarchy));
				Task.Factory.ContinueWhenAll(new Task[] { majorTask, minorTask, stackTask }, (obj) => { synchronizer.Set(); });
				synchronizer.WaitOne();
			}
			pHierarchy.AddStack(majorTask.Result, minorTask.Result, stackTask.Result);
		}

	}

}
