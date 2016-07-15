using System;
using System.ServiceModel;
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
        public BaseServiceResult<byte[]> GetNotificationContent(long NotificationId, string NotificationAccessKey, string ReceiverToken, long ReceiverId)
        {
            try
            {
                using (IReceiverBL ReceiverBL = new ReceiverBL())
                {
                    if (!ReceiverBL.CheckTokenValid(ReceiverToken, ReceiverId))
                    {
                        return new BaseServiceResult<byte[]>(ResultStatusCodes.UnAuthorised, null);
                    }
                }

                using (var NotificationBL = new NotificationBL())
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
                using (INotificationBL NotificationBL = new NotificationBL())
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
        public BaseServiceResult AddDevice(long ReceiverId, Device deviceInfo, string ReceiverToken)
        {
            try
            {
                using (IReceiverBL ReceiverBL = new ReceiverBL())
                {
                    if (!ReceiverBL.CheckTokenValid(ReceiverToken, ReceiverId))
                    {
                        return new BaseServiceResult(ResultStatusCodes.UnAuthorised, null);
                    }
                }

                using (var DeviceBL = new DeviceBL())
                {
                    return DeviceBL.AddDevice(deviceInfo);
                }
            } catch(Exception ex)
            {
                Logger.Error("AddDevice", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }

        public BaseServiceResult RemoveDevice(string DeviceIMEI, long ReceiverId, string ReceiverToken)
        {
            try
            {
                using (var ReceiverBL = new ReceiverBL())
                {
                    if (!ReceiverBL.CheckTokenValid(ReceiverToken, ReceiverId))
                    {
                        return new BaseServiceResult<byte[]>(ResultStatusCodes.UnAuthorised, null);
                    }
                }

                using (IDeviceBL DeviceBL = new DeviceBL())
                {
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