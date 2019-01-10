///
///                           $Id: DbHelper.cs 14589 2011-01-13 12:15:52Z neil.middleton $
///              $LastChangedDate: 2011-01-13 12:15:52 +0000 (Thu, 13 Jan 2011) $
///          $LastChangedRevision: 14589 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/DbHelper.cs $
///

﻿//#define DEBUG_ME

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;

namespace TR.Profile.Common.Data
{
    public class DbHelper
	{
#if DEBUG_ME && DEBUG
        private const string DebugCategory = "[TR.Profile.Common.Data.DbHelper]";
#endif

		public static IDataReader ExecuteReader(DbTransaction transaction, string query)
        {
            return ExecuteReader(transaction, query, null);
        }
        public static IDataReader ExecuteReader(DbTransaction transaction, string query, ICollection<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction", "transaction");
            if (query == null)
                throw new ArgumentNullException("query", "query");
            if (query == string.Empty)
                throw new ArgumentException("query", "query");

            using (DbCommand command = PrepareCommand(transaction, query, parameters))
            {
                return command.ExecuteReader();
            }
        }
        public static int ExecuteNonQuery(DbTransaction transaction, string query)
        {
            return ExecuteNonQuery(transaction, query, null);
        }
        public static int ExecuteNonQuery(DbTransaction transaction, string query, ICollection<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction", "transaction");
            if (query == null)
                throw new ArgumentNullException("query", "query");
            if (query == string.Empty)
                throw new ArgumentException("query", "query");

            using (DbCommand command = PrepareCommand(transaction, query, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }
        public static int ExecuteNonQueryProcedure(DbTransaction transaction, string procedure, ICollection<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction", "transaction");
            if (String.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure", "procedure");

            using (DbCommand command = PrepareCommandProcedure(transaction, procedure, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(DbTransaction transaction, string query)
        {
            return ExecuteScalar(transaction, query, null);
        }
        public static object ExecuteScalar(DbTransaction transaction, string query, ICollection<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction", "transaction");
            if (query == null)
                throw new ArgumentNullException("query", "query");
            if (query == string.Empty)
                throw new ArgumentException("query", "query");

            using (DbCommand command = PrepareCommand(transaction, query, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        protected static DbCommand PrepareCommand(DbTransaction transaction, string query)
        {
            return PrepareCommand(transaction, query, null);
        }
        protected static DbCommand PrepareCommand(DbTransaction transaction, string query, ICollection<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction", "transaction");
            if (query == null)
                throw new ArgumentNullException("query", "query");
            if (query == string.Empty)
                throw new ArgumentException("query", "query");

            DbCommand result = transaction.Connection.CreateCommand();
            result.CommandText = query;
            result.Transaction = transaction;
            result.Parameters.Clear();
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    result.Parameters.Add(parameter);
                }
			}

#if DEBUG_ME && DEBUG
			System.Diagnostics.Debug.WriteLine(string.Empty);
			PrintParameters(parameters);
			System.Diagnostics.Debug.WriteLine(string.Format("query:{0}", query), DebugCategory);
#endif
			return result;
        }
        protected static DbCommand PrepareCommandProcedure(DbTransaction transaction, string procedure, ICollection<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction", "transaction");
            if (String.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure", "procedure");

            DbCommand result = transaction.Connection.CreateCommand();
            result.CommandText = procedure;
            result.Transaction = transaction;
            result.CommandType = CommandType.StoredProcedure;
            result.Parameters.Clear();
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    result.Parameters.Add(parameter);
                }
			}

#if DEBUG_ME && DEBUG
			System.Diagnostics.Debug.WriteLine(string.Empty);
			PrintParameters(parameters);
			System.Diagnostics.Debug.WriteLine(string.Format("procedure:{0}", procedure), DebugCategory);
#endif
			return result;
        }

        public static Guid GetGuid(IDataReader pReader, string pName)
        {
            if (pReader == null || pReader.IsClosed)
                return Guid.Empty;
            if (String.IsNullOrEmpty(pName))
                throw new ArgumentNullException("pName");

            return (pReader[pName] == DBNull.Value) ?
                Guid.Empty :
                (Guid)pReader[pName];
        }
        public static string GetString(IDataReader pReader, string pName)
        {
            if (pReader == null || pReader.IsClosed)
                return null;
            if (String.IsNullOrEmpty(pName))
                throw new ArgumentNullException("pName");

            return (pReader[pName] == DBNull.Value) ?
                null :
                (string)pReader[pName];
        }
        public static int GetInt(IDataReader pReader, string pName)
        {
            if (pReader == null || pReader.IsClosed)
                return 0;
            if (String.IsNullOrEmpty(pName))
                throw new ArgumentNullException("pName");

            return (pReader[pName] == DBNull.Value) ?
                0 :
                (int)pReader[pName];
        }
		public static int? GetNullableInt(IDataReader pReader, string pName)
		{
			if (pReader == null || pReader.IsClosed)
				return ((int?)null);
			if (String.IsNullOrEmpty(pName))
				throw new ArgumentNullException("pName");

			return (pReader[pName] == DBNull.Value) ?
				((int?)null) :
				(int)pReader[pName];
		}
		public static decimal GetDecimal(IDataReader pReader, string pName)
		{
			if (pReader == null || pReader.IsClosed)
				return 0;
			if (String.IsNullOrEmpty(pName))
				throw new ArgumentNullException("pName");

			return (pReader[pName] == DBNull.Value) ?
				0 :
				(decimal)pReader[pName];
		}
		public static decimal? GetNullableDecimal(IDataReader pReader, string pName)
		{
			if (pReader == null || pReader.IsClosed)
				return 0;
			if (String.IsNullOrEmpty(pName))
				throw new ArgumentNullException("pName");

			return (pReader[pName] == DBNull.Value) ?
				((decimal?)null) :
				(decimal)pReader[pName];
		}
		public static double GetDouble(IDataReader pReader, string pName)
        {
            if (pReader == null || pReader.IsClosed)
                return 0;
            if (String.IsNullOrEmpty(pName))
                throw new ArgumentNullException("pName");

            return (pReader[pName] == DBNull.Value) ?
                0 :
                (double)pReader[pName];
        }
        public static DateTime GetDateTime(IDataReader pReader, string pName)
        {
            if (pReader == null || pReader.IsClosed)
                return DateTime.MinValue;
            if (String.IsNullOrEmpty(pName))
                throw new ArgumentNullException("pName");

            return (pReader[pName] == DBNull.Value) ?
                DateTime.MinValue :
                (DateTime)pReader[pName];
        }
        public static byte[] GetByteArray(IDataReader pReader, string pName)
        {
            if (pReader == null || pReader.IsClosed)
                return null;
            if (String.IsNullOrEmpty(pName))
                throw new ArgumentNullException("pName");

            return (pReader[pName] == DBNull.Value) ?
                null :
                (byte[])pReader[pName];
		}


#if DEBUG_ME && DEBUG
		static void PrintParameters(ICollection<DbParameter> parameters)
		{
			if (parameters == null)
				return;

			foreach (var item in parameters)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("query parameter; name:{0}, value:{1}", item.ParameterName, item.Value), DebugCategory);
			}
		}
#endif

	}

}
