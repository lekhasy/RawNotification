using RawNotification.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.BusinessLogic
{
    public class BaseServiceBL : IBaseBL
    {
        protected IRawNotificationDB DB;
        protected log4net.ILog _Logger;

        public BaseServiceBL(IRawNotificationDB db)
        {
            DB = db;
            _Logger = log4net.LogManager.GetLogger(this.GetType().Name);
        }

        public BaseServiceBL()
        {
            DB = new RawNotificationDB();
            _Logger = log4net.LogManager.GetLogger(this.GetType().Name);
        }

        public void Dispose()
        {
            DB.Dispose();
            // call this method in order to say with GC that we don't need to call destructor any more (~BaseBL() method)
            GC.SuppressFinalize(this);
        }
        ~BaseServiceBL()
        {
            DB.Dispose();
        }
    }
}