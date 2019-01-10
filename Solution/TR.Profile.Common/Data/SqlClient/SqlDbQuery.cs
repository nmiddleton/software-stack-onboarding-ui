
///
///                           $Id: SqlDbQuery.cs 13936 2010-11-24 14:57:22Z neil.middleton $
///              $LastChangedDate: 2010-11-24 14:57:22 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13936 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/SqlClient/SqlDbQuery.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR.Profile.Common.Data
{
    public sealed class SqlDbQuery<TTable> : DbQueryBase<TTable>
        where TTable : IDbTable, new()
    {

        /// <summary>
        /// SQL format
        /// </summary>
        /// <returns>[Field1], [Field2]</returns>
        protected override string CreateColumnsString<TColumn>(ICollection<TColumn> pItems)
        {
            if (pItems == null)
                return string.Empty;

            Action<StringBuilder, TColumn> _columnElementConstructor =
            (sb, column) =>
            {
                sb.AppendFormat("[{0}]", column);
            };

            return CreateString(pItems, _commaConjunctionConstructor, _columnElementConstructor);
        }

        /// <summary>
        /// SQL format
        /// </summary>
        /// <returns>[Table].[Field1], [Table].[Field2]</returns>
        protected override string CreateTableColumnsString<TColumn>(ICollection<TColumn> pItems)
        {
            if (pItems == null)
                return string.Empty;

            Action<StringBuilder, TTable, TColumn> _tableColumnElementConstructor =
            (sb, table, column) =>
            {
                sb.AppendFormat("[{0}].[{1}]", table.TableName, column);
            };

            return CreateString(pItems, _commaConjunctionConstructor, _tableColumnElementConstructor, Table);
        }

        /// <summary>
        /// SQL format
        /// </summary>
        /// <returns>@Field1, @Field2</returns>
        protected override string CreateParametersString<TColumn>(ICollection<TColumn> pItems)
        {
            if (pItems == null)
                return string.Empty;

            Action<StringBuilder, TColumn> _parameterElementConstructor =
            (sb, field) =>
            {
                sb.AppendFormat("@{0}", field);
            };

            return CreateString(pItems, _commaConjunctionConstructor, _parameterElementConstructor);
        }

        /// <summary>
        /// SQL format
        /// </summary>
        /// <returns>[Field1] = @Field1 and [Field2] = @Field2</returns>
        protected override string CreateANDString<TColumn>(ICollection<TColumn> pItems)
        {
            if (pItems == null)
                return string.Empty;

            Action<StringBuilder, TColumn> _eqElementConstructor =
            (sb, column) =>
            {
                sb.AppendFormat("[{0}] = @{0}", column);
            };

            return CreateString<TColumn>(pItems, _andConjunctionConstructor, _eqElementConstructor);
        }

        /// <summary>
        /// SQL format
        /// </summary>
        /// <returns>[Field1] = @Field1, [Field2] = @Field2</returns>
        protected override string CreateCommaEqString<TColumn>(ICollection<TColumn> pItems)
        {
            if (pItems == null)
                return string.Empty;

            Action<StringBuilder, TColumn> _eqElementConstructor =
            (sb, column) =>
            {
                sb.AppendFormat("[{0}] = @{0}", column);
            };

            return CreateString<TColumn>(pItems, _commaConjunctionConstructor, _eqElementConstructor);
        }

    }

}
