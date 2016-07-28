using System.Collections.Generic;
using System.Data;
using RawNotification.Models.DBModels;
using DapperExtensions;
using System.Linq;
using System;
using Dapper;

namespace RawNotification.DataAccess.DAImplements
{
    public class DeviceDA : BaseDA, DAInterfaces.IDeviceDA
    {
        public DeviceDA(IDbTransaction trans) : base(trans)
        {
            
        }

        public void DeleteDevice(long deviceId, long receiverId)
        {
            connection.Execute(sql: RawNotificationDB.StoreProcedureList.RemoveDeviceProcName, param: new { ipDeviceId = deviceId, ipReceiverId = receiverId }, commandType: CommandType.StoredProcedure, transaction:transaction);
        }

        public IEnumerable<Device> GetAllReceiverDevice(long receiverId)
        {
            //return connection.Query<Device>("SELECT * FROM Device WHERE ReceiverNewID = @ReceiverID", new { ReceiverID = receiverId });
            var eqalpredicate = Predicates.Field<Device>(d => d.ReceiverNewID,Operator.Eq, receiverId);
            return connection.GetList<Device>(eqalpredicate, transaction: transaction);
        }

        public Device GetDeviceByIMEI(string IMEI)
        {
            var equalPredicate = Predicates.Field<Device>(d => d.IMEI, Operator.Eq, IMEI);
            return connection.GetList<Device>(predicate: equalPredicate, transaction: transaction).FirstOrDefault();
        }

        public void InsertDevice(Device deviceInfo)
        {
            connection.Execute(RawNotificationDB.StoreProcedureList.InsertDeviceProcname,
                new {
                    ipReceiverNewID = deviceInfo.ReceiverNewID,
                    ipIMEI = deviceInfo.IMEI,
                    ipURI = deviceInfo.URI,
                    ipOSId = deviceInfo.OSId
                }, commandType: CommandType.StoredProcedure, transaction: transaction);
            deviceInfo.Id = getLastestIdentity();
        }

        /// <summary>
        /// Insert a Device to DB withought update Idenitty for the entity
        /// </summary>
        /// <param name="device">The inserted Device</param>
        public void InsertNewDevice(Device device)
        {
            connection.Insert(device, transaction: transaction);
        }

        public void UpdateDevice(Device Device)
        {
            connection.Update(Device, transaction: transaction);
        }
    }
}
