using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using RawNotification.DataAccess;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.BusinessLogic.BLImplements
{
    public class DeviceBL : BaseServiceBL, BLInterfaces.IDeviceBL
    {
        public DeviceBL() : base()
        {
        }

        public DeviceBL(IRawNotificationDB db) : base(db)
        {
        }

        public BaseServiceResult AddDevice(Device deviceInfo)
        {
            try
            {
                var receiver = DB.ReceiverDA.GetReceiverById(deviceInfo.ReceiverNewID);
                if (receiver == null) return new BaseServiceResult(ResultStatusCodes.NotFound, "Receiver not found");

                var Device = DB.DeviceDA.GetDeviceByIMEI(deviceInfo.IMEI);

                if (Device == null)
                    DB.DeviceDA.InsertDevice(deviceInfo);
                else
                {
                    // just update URI is enought
                    if (deviceInfo.ReceiverNewID == Device.ReceiverNewID)
                    {
                        Device.OSId = deviceInfo.OSId;
                        Device.URI = deviceInfo.URI;
                        DB.DeviceDA.UpdateDevice(Device);
                    }
                    else
                    {
                        // this means that device not be used by old user anymore
                        DB.DeviceDA.DeleteDevice(Device.Id, Device.ReceiverNewID);
                        DB.DeviceDA.InsertDevice(deviceInfo);
                    }
                }

                DB.commit();
                return new BaseServiceResult(ResultStatusCodes.OK);
            } catch (Exception ex)
            {
                _Logger.Error("Add Device", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }

        public BaseServiceResult RemoveDeviceByIMEI(string DeviceIMEI)
        {
            try
            {
                var device = DB.DeviceDA.GetDeviceByIMEI(DeviceIMEI);
                if (device == null)
                {
                    return new BaseServiceResult(ResultStatusCodes.NotFound);
                }
                DB.DeviceDA.DeleteDevice(device.Id, device.ReceiverNewID);
                DB.commit();
                return new BaseServiceResult(ResultStatusCodes.OK, null);
            } catch(Exception ex)
            {
                _Logger.Error("Add Device", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }
    }
}
