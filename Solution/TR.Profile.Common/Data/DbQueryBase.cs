///
///                           $Id: DbQueryBase.cs 14243 2010-12-14 17:02:41Z neil.middleton $
///              $LastChangedDate: 2010-12-14 17:02:41 +0000 (Tue, 14 Dec 2010) $
///          $LastChangedRevision: 14243 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/DbQueryBase.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR.Profile.Common.Data
{
    public abstract class DbQueryBase<TTable>
        where TTable : IDbTable, new()
    {
        #region ctor
        public DbQueryBase()
        {
            Table = new TTable();
        }
        #endregion

        public TTable Table { get; private set; }

        public virtual string SelectAllQuery
        {
            get { return string.Format("SELECT {0} FROM {1}", /*0*/CreateTableColumnsString(Table.ColumnNames), /*1*/Table.TableName); }
        }
        public string SelectByKeyQuery
        {
            get { return string.Format("{0} WHERE {1}", /*0*/SelectAllQuery, /*1*/CreateANDString(this.Table.KeyColumnNames)); }
        }

        protected abstract string CreateColumnsString<TColumn>(ICollection<TColumn> pItems);
        protected abstract string CreateTableColumnsString<TColumn>(ICollection<TColumn> pItems);
        protected abstract string CreateParametersString<TColumn>(ICollection<TColumn> pItems);
        protected abstract string CreateANDString<TColumn>(ICollection<TColumn> pItems);
        protected abstract string CreateCommaEqString<TColumn>(ICollection<TColumn> pItems);

        #region string constructors miscellaneous
        protected Action<StringBuilder> _commaConjunctionConstructor =
            (sb) =>
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
            };
        protected Action<StringBuilder> _andConjunctionConstructor =
            (sb) =>
            {
                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }
            };

        protected string CreateString<TColumn>(ICollection<TColumn> pItems, Action<StringBuilder> pConjunctionConstructor, Action<StringBuilder, TColumn> pElementConstructor)
        {
            if (pItems == null || pConjunctionConstructor == null || pElementConstructor == null)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            pItems.ToList<TColumn>().ForEach(
                item =>
                {
                    pConjunctionConstructor(result);
                    pElementConstructor(result, item);
                });
            return result.ToString();
        }
        protected string CreateString<TTableZ, TColumn>(ICollection<TColumn> pItems, Action<StringBuilder> pConjunctionConstructor, Action<StringBuilder, TTableZ, TColumn> pElementConstructor, TTableZ pTable)
            where TTableZ : IDbTable
        {
            if (pItems == null || pConjunctionConstructor == null || pElementConstructor == null || pTable == null)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            pItems.ToList<TColumn>().ForEach(
                item =>
                {
                    pConjunctionConstructor(result);
                    pElementConstructor(result, pTable, item);
                });
            return result.ToString();
        }
        #endregion

    }

}
