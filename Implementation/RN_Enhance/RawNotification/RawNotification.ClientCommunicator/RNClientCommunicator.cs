using System;
using System.ServiceModel;
using RawNotification.BusinessLogic;
using RawNotification.BusinessLogic.BLImplements;
using RawNotification.BusinessLogic.BLInterfaces;
using RawNotification.Models;
using RawNotification.Models.DBModels;

namespace RawNotification.ClientCommunicator
{
    [ServiceBehavior(MaxItemsInObjectGraph = 2147483646, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RNClientCommunicator : IRNClientCommunicator
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BaseServiceResult<byte[]> GetNotificationContent(long NotificationId, string NotificationAccessKey, string DeviceToken, string DeviceIMEI)
        {
            try
            {
                using (var DeviceBL = RNBusinessLogics.GetDeviceBL())
                {
                    if (!DeviceBL.CheckDeviceTokenValid(DeviceToken, DeviceIMEI))
                    {
                        return new BaseServiceResult<byte[]>(ResultStatusCodes.UnAuthorised, null);
                    }
                }

                using (var NotificationBL = RNBusinessLogics.GetNotificationBL())
                {
                    return NotificationBL.GetNotificationContent(NotificationId, NotificationAccessKey);
                }
            } catch(Exception ex)
            {
                Logger.Error("GetNotificationContent", ex);
                return BaseServiceResult<byte[]>.InternalErrorResult;
            }
        }

        public BaseServiceResult<bool> CheckIfNotificationRead(long ReceiverNotificationID)
        {
            try
            {
                using (INotificationBL NotificationBL = RNBusinessLogics.GetNotificationBL())
                {
                     return NotificationBL.CheckIfNotificationRead(ReceiverNotificationID);
                }
            } catch(Exception ex)
            {
                Logger.Error("GetNotificationContent", ex);
                return BaseServiceResult<bool>.InternalErrorResult;
            }
        }

        // return StatusCodes: 
        // UnAuthorised: Invalid receiver Token
        // InternalServerError: Internal error
        public BaseServiceResult<long, string> AddDevice(long ReceiverId, Device deviceInfo, string LoginToken)
        {
            try
            {
                using (var ReceiverBL = RNBusinessLogics.GetReceiverBL())
                    if (!ReceiverBL.CheckLoginTokenValid(LoginToken, ReceiverId))
                        return new BaseServiceResult<long, string>(ResultStatusCodes.UnAuthorised, 0, "");

                using (var DeviceBL = RNBusinessLogics.GetDeviceBL())
                    return DeviceBL.AddDevice(deviceInfo, Settings.NewDeviceTokenPeriod.Value);

            } catch(Exception ex)
            {
                Logger.Error("AddDevice", ex);
                return BaseServiceResult<long, string>.InternalErrorResult;
            }
        }

        public BaseServiceResult<long, string> UpdateDeviceInfo(Device deviceInfo, string DeviceIMEI, string DeviceToken)
        {
            try
            {
                using (var DeviceBL = RNBusinessLogics.GetDeviceBL())
                    if (!DeviceBL.CheckDeviceTokenValid(DeviceToken, DeviceIMEI))
                        return new BaseServiceResult<long, string>(ResultStatusCodes.UnAuthorised, 0, "");

                using (var DeviceBL = RNBusinessLogics.GetDeviceBL())
                    return DeviceBL.AddDevice(deviceInfo, Settings.NewDeviceTokenPeriod.Value);
            }
            catch (Exception ex)
            {
                Logger.Error("AddDevice", ex);
                return BaseServiceResult<long, string>.InternalErrorResult;
            }
        }

        public BaseServiceResult RemoveDevice(string DeviceIMEI, string DeviceToken)
        {
            try
            {
                using (var DeviceBL = RNBusinessLogics.GetDeviceBL())
                {
                    if (!DeviceBL.CheckDeviceTokenValid(DeviceToken, DeviceIMEI))
                        return new BaseServiceResult<byte[]>(ResultStatusCodes.UnAuthorised, null);
                    return DeviceBL.RemoveDeviceByIMEI(DeviceIMEI);
                }
            } catch (Exception ex)
            {
                Logger.Error("AddDevice", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }
    }
}