///
///                           $Id: DataGatewayBase.cs 14185 2010-12-09 16:45:56Z neil.middleton $
///              $LastChangedDate: 2010-12-09 16:45:56 +0000 (Thu, 09 Dec 2010) $
///          $LastChangedRevision: 14185 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/DataGatewayBase.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data;

namespace TR.Profile.Common.Data
{
    public abstract class DataGatewayBase<TDomain, TTable, TQuery>
        where TDomain : IPersistent, new()
        where TQuery : DbQueryBase<TTable>, new()
        where TTable : IDbTable, new()
    {
        #region ctor
        public DataGatewayBase(DbTransaction pTransaction)
        {
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");

            _transaction = pTransaction;
            Query = new TQuery();
        }
        #endregion

        protected DbTransaction _transaction;

        protected TQuery Query { get; private set; }

        public void Load(TDomain pItem)
        {
            if (pItem == null)
                return;

            Load(pItem, Query.SelectByKeyQuery, MapKey(pItem));
        }
        protected void Load(TDomain pItem, string pQuery, ICollection<DbParameter> pParameters)
        {
            if (pItem == null || String.IsNullOrEmpty(pQuery))
                return;

            using (IDataReader reader = DbHelper.ExecuteReader(_transaction, pQuery, pParameters))
            {
                pItem.IsLoaded = false;
                if (reader.Read())
                {
                    Fill(pItem, reader);
                    pItem.IsLoaded = true;
                }
            }
        }

        public void Load(ICollection<TDomain> pItems)
        {
            if (pItems == null)
                return;

            Load(pItems, Query.SelectAllQuery, null);
        }
        protected void Load(ICollection<TDomain> pItems, string pQuery, ICollection<DbParameter> pParameters)
        {
            if (pItems == null || String.IsNullOrEmpty(pQuery))
                return;

            using (IDataReader reader = DbHelper.ExecuteReader(_transaction, pQuery, pParameters))
            {
                while (reader.Read())
                {
                    TDomain domain = new TDomain()
                    {
                        IsLoaded = true
                    };
                    Fill(domain, reader);
                    pItems.Add(domain);
                }
            }
        }

        protected abstract void Fill(TDomain pDomain, IDataReader pReader);

        protected abstract ICollection<DbParameter> MapKey(TDomain pDomain);

    }

}
