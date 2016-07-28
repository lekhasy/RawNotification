using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using RawNotification.DataAccess.DAInterfaces;
using RawNotification.DataAccess.DAImplements;

namespace RawNotification.DataAccess
{
    /// <summary>
    /// *** Remember: do not use multi thread with this class.
    /// each instance of this class just use one connection inside one commit.
    /// If you are using multi thread in one connection, it may not works properly
    /// If you really want to use multi thread, you should use that in client side by creating one service client instance for each thread
    /// 
    /// </summary>
    public class RawNotificationDB : IRawNotificationDB
    {
        public static class StoreProcedureList
        {
            public const string RemoveAllReadNotificationProcName = "RemoveAllReadNotification";
            public const string GetAllPendingDeviceNotificationInfoProcName = "GetAllPendingDeviceNotificationInfo";
            public const string RemoveDeviceProcName = "RemoveDevice";
            public const string RemoveDeviceByIMEIProcName = "RemoveDevice";
            public const string RemoveAllReceiverHaveNoDeviceProcName = "RemoveAllReceiverHaveNoDevice";
            public const string InsertDeviceProcname = "InsertDevice";
        }

        private IDbConnection mConnection;
        private IDbTransaction mTransaction;
        private bool mDisposed;


        #region DataAccess Layer

        //*** Notice: After add a DA to UnitOfWork, don't forget assign your DA = null in resetRepositories() Method - this is important
        #region Attributes
        // declare DA Interfaces here
        private IDeviceDA _DeviceDA;

        private INotificationDA _NotificationDA;

        private IReceiverDA _ReceiverDA;

        private IDeviceNotificationDA _DeviceNotificationDA;

        private IReceiverNotificationDA _ReceiverNotificationDA;

        private ILoginTokenDA _LoginTokenDA;

        #endregion

        #region Properties
        // declare DA properties here

        public IDeviceDA DeviceDA
        {
            get { return _DeviceDA ?? (_DeviceDA = new DeviceDA(mTransaction)); }
        }

        public INotificationDA NotificationDA
        {
            get { return _NotificationDA ?? (_NotificationDA = new NotificationDA(mTransaction)); }
        }

        public IReceiverDA ReceiverDA
        {
            get { return _ReceiverDA ?? (_ReceiverDA = new ReceiverDA(mTransaction)); }
        }

        public IReceiverNotificationDA ReceiverNotificationDA
        {
            get { return _ReceiverNotificationDA ?? (_ReceiverNotificationDA = new ReceiverNotificationDA(mTransaction)); }
        }

        public IDeviceNotificationDA DeviceNotificationDA
        {
            get { return _DeviceNotificationDA ?? (_DeviceNotificationDA = new DeviceNotificationDA(mTransaction)); }
        }

        public ILoginTokenDA LoginTokenDA
        {
            get { return _LoginTokenDA ?? (_LoginTokenDA = new LoginTokenDA(mTransaction)); }
        }
        #endregion

        #endregion
        public RawNotificationDB(IDbConnection connection = null)
        {
            mConnection = connection ?? openConnection();
            mConnection.Open();
            mTransaction = mConnection.BeginTransaction();
        }

        public void commit()
        {
            try
            {
                mTransaction.Commit();
            }
            catch
            {
                mTransaction.Rollback();
                throw;
            }
            finally
            {
                mTransaction.Dispose();
                mTransaction = mConnection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            //set all repository = null
            _NotificationDA = null;
            _DeviceDA = null;
            _ReceiverDA = null;
            _DeviceNotificationDA = null;
            _ReceiverNotificationDA = null;
            _LoginTokenDA = null;
        }
        
        /// <summary>
        /// if this class used inside a using statement, this method will be called by GC
        /// </summary>
        public void Dispose()
        {
            dispose();
            // call this method in order to say with GC that we don't need to call destructor any more (~RawNotificationDB() method)
            GC.SuppressFinalize(this);
        }

        private void dispose()
        {
            // dispose this instance if isn't disposed
            // we have to check this because dispose method can be called multiple times by system.
            if (!mDisposed)
            {
                if (mTransaction != null)
                {
                    mTransaction.Dispose();
                    mTransaction = null;
                }
                if (mConnection != null)
                {
                    mConnection.Dispose();
                    mConnection = null;
                }
                mDisposed = true;
            }
        }

        /// <summary>
        /// If this class used without using of using statement, this method will be called when it run out of it's scope
        /// </summary>
        ~RawNotificationDB()
        {
            dispose();
        }

        private static IDbConnection openConnection()
        {
            string dbName = ConfigurationManager.AppSettings["DBName"];
            var dbServer = ConfigurationManager.AppSettings["DBSerNameOrIP"];
            var dbUser = ConfigurationManager.AppSettings["DBUserId"];
            var dbPass = ConfigurationManager.AppSettings["DBUserPassword"];

            string strConnection = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                                    dbServer,
                                    dbName,
                                    dbUser,
                                    dbPass
                                );

            return new SqlConnection(strConnection);
        }
    }
}