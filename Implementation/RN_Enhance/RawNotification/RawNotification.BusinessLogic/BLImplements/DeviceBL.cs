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
    internal class DeviceBL : BaseServiceBL, BLInterfaces.IDeviceBL
    {
        public DeviceBL() : base()
        {
        }

        public DeviceBL(IRawNotificationDB db) : base(db)
        {
        }
        
        // retun Device Id and token of that device
        public BaseServiceResult<long, string> AddDevice(Device deviceInfo, TimeSpan NewDeviceTokenPeriod)
        {
            try
            {
                var receiver = DB.ReceiverDA.GetReceiverById(deviceInfo.ReceiverNewID);
                if (receiver == null) return new BaseServiceResult<long, string>(ResultStatusCodes.NotFound, 0, "", "Receiver not found");

                string Token = Guid.NewGuid().ToString();
                var Device = DB.DeviceDA.GetDeviceByIMEI(deviceInfo.IMEI);

                deviceInfo.DeviceToken = Token;
                deviceInfo.TokenExpiredTime = DateTime.Now.Add(NewDeviceTokenPeriod);

                if (Device == null)
                {
                    DB.DeviceDA.InsertDevice(deviceInfo);
                }
                else
                {
                    // just update URI and token is enought
                    if (deviceInfo.ReceiverNewID == Device.ReceiverNewID)
                    {
                        // device still be used with same receiver
                        Device.OSId = deviceInfo.OSId;
                        Device.URI = deviceInfo.URI;
                        Device.DeviceToken = Token;
                        Device.TokenExpiredTime = DateTime.Now.Add(NewDeviceTokenPeriod);
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
                return new BaseServiceResult<long, string>(ResultStatusCodes.OK, deviceInfo.Id, Token);
            }
            catch (Exception ex)
            {
                _Logger.Error("Add Device", ex);
                return BaseServiceResult<long, string>.InternalErrorResult;
            }
        }

        public bool CheckDeviceTokenValid(string DeviceToken, string DeviceIMEI)
        {
            if (DeviceToken == null) return false;

            var Device = DB.DeviceDA.GetDeviceByIMEI(DeviceIMEI);
            if (Device != null && Device.DeviceToken == DeviceToken) return true;
            return false;
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
            }
            catch (Exception ex)
            {
                _Logger.Error("Add Device", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }
    }
}
