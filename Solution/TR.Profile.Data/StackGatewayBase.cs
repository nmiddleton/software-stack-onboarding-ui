///
///                           $Id: StackGatewayBase.cs 13939 2010-11-24 16:03:20Z neil.middleton $
///              $LastChangedDate: 2010-11-24 16:03:20 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13939 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Data/ITICGateway.cs $
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using TR.Profile.Domain;
using System.Data;
using TR.Profile.Common.Data;
using System.Threading.Tasks;
using System.Threading;
using TR.Profile.Common;

namespace TR.Profile.Data
{
    public abstract class StackGatewayBase
    {
        #region ctor
        public StackGatewayBase(DbTransaction pTransaction)
        {
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");

            _transaction = pTransaction;
        }
        #endregion

        private DbTransaction _transaction;

		protected abstract string SelectAllQuery { get; }

        public void Load(StackHierarchy pHierarchy)
        {
			if (pHierarchy == null)
				return;

			using (IDataReader reader = DbHelper.ExecuteReader(_transaction, this.SelectAllQuery))
			{
				while (reader.Read())
				{
					PopulateHierarchy(reader, pHierarchy);
				}
			}
		}

		protected abstract void PopulateHierarchy(IDataReader pReader, StackHierarchy pHierarchy);

		protected FilterLookup LoadMajorFilter(object pHelper)
		{
			HierarchyParallelHelper helper = pHelper as HierarchyParallelHelper;
			if (helper == null)
				return null;

			FilterLookup result = helper.Hierarchy.GenerateMajorFilter(
				DbHelper.GetString(helper.Reader, StackDbTable.MAJOR_FILTER_COLUMN));
			result.IsLoaded = true;
			return result;
		}
		protected FilterLookup LoadMinorFilter(object pHelper)
		{
			HierarchyParallelHelper helper = pHelper as HierarchyParallelHelper;
			if (helper == null)
				return null;

			FilterLookup result = helper.Hierarchy.GenerateMinorFilter(
				DbHelper.GetString(helper.Reader, StackDbTable.MINOR_FILTER_COLUMN));
			result.IsLoaded = true;
			return result;
		}
		protected StackLookup LoadPlatformStack(object pHelper)
		{
			HierarchyParallelHelper helper = pHelper as HierarchyParallelHelper;
			if (helper == null)
				return null;

			StackLookup result = helper.Hierarchy.GeneratePlatformStackLookup(
				DbHelper.GetString(helper.Reader, StackDbTable.NAME_COLUMN),
				DbHelper.GetString(helper.Reader, StackDbTable.CODE_COLUMN),
				DbHelper.GetString(helper.Reader, StackDbTable.DESCRIPTION_COLUMN));
			result.IsLoaded = true;
			return result;
		}

		#region LoadHelper class
		protected sealed class HierarchyParallelHelper
		{
			#region ctor
			public HierarchyParallelHelper(IDataReader pReader, StackHierarchy pHierarchy)
			{
				this.Reader = pReader;
				this.Hierarchy = pHierarchy;
			}
			#endregion

			public IDataReader Reader { get; private set; }
			public StackHierarchy Hierarchy { get; private set; }
		}
		#endregion
	}

}
