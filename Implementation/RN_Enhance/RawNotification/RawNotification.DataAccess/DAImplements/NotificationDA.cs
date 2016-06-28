using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using RawNotification.Models.DBModels;
using Dapper;
using DapperExtensions;
using RawNotification.Models;
using RawNotification.Models.ServerBusinessModels;

namespace RawNotification.DataAccess.DAImplements
{
    public class NotificationDA : BaseDA, DAInterfaces.INotificationDA
    {

        public NotificationDA(IDbTransaction trans) : base(trans)
        {

        }

        public IEnumerable<PendingNotificationInfo> GetAllPendingDeviceNotificationInfo()
        {
            return connection.Query<PendingNotificationInfo>(RawNotificationDB.StoreProcedureList.GetAllPendingDeviceNotificationInfoProcName, commandType: CommandType.StoredProcedure, transaction:transaction);
        }

        public Notification GetNotificationById(long Id)
        {
            return connection.Get<Notification>(Id,transaction: transaction);
        }

        public void InsertNotification(Notification notifi)
        {
            connection.Insert(notifi, transaction: transaction);
            notifi.Id = getLastestIdentity();
        }

        public void RemoveAllReadNotification()
        {
            connection.Execute(RawNotificationDB.StoreProcedureList.RemoveAllReadNotificationProcName, transaction: transaction);
        }
    }
}
