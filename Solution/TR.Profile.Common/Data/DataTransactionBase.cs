
///
///                           $Id: DataTransactionBase.cs 13997 2010-11-25 10:42:25Z neil.middleton $
///              $LastChangedDate: 2010-11-25 10:42:25 +0000 (Thu, 25 Nov 2010) $
///          $LastChangedRevision: 13997 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Data/DataTransactionBase.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data;

namespace TR.Profile.Common.Data
{
    public abstract class DataTransactionBase : IDisposable
    {
        protected string _connectionString;

        private DbConnection _connection = null;
        private IsolationLevel _isolationLevel = IsolationLevel.Unspecified;
        private DbTransaction _transaction = null;

        #region ctor
        public DataTransactionBase(string pConnectionString)
        {
            _connectionString = pConnectionString;
            _isolationLevel = IsolationLevel.Unspecified;
        }
        #endregion

        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = CreateConnection();
                    _connection.Open();
                }

                return _connection;
            }
        }
        public DbTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = Connection.BeginTransaction(_isolationLevel);
                }
                return _transaction;
            }
        }
        public bool IsOpened
        {
            get { return (_connection != null && _connection.State == ConnectionState.Open); }
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            finally
            {
                Connection.Close();
                _connection = null;
            }
        }
        public void Rollback()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    try { _transaction.Rollback(); }
                    catch { ; }
                }
                _connection.Close();
                _connection = null;
                _transaction = null;
            }
            System.GC.SuppressFinalize(this);
        }

        protected abstract DbConnection CreateConnection();

    }

}
