using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.DataAccess;
using RawNotification.Models.ServerBusinessModels;
using RawNotification.Models.ServerBusinessModels.Exceptions;
using RawNotification.Models;
using RawNotification.Models.DBModels;

namespace RawNotification.BusinessLogic.BLImplements
{
    internal class NotificationBL : BaseServiceBL, BLInterfaces.INotificationBL
    {
        public NotificationBL() : base()
        {
        }

        public NotificationBL(IRawNotificationDB db) : base(db)
        {
        }

        public BaseServiceResult AddNotification(byte[] notificaitonData, byte[] NotificationPreviewContent, IEnumerable<string> oldReceiverIDList)
        {
            try
            {
                string notificationAccessKey = Guid.NewGuid().ToString();

                Notification notifi = new Notification { NotificationContent = notificaitonData, NotificationAccessKey = notificationAccessKey, NotificationPreviewContent = NotificationPreviewContent };
                DB.NotificationDA.InsertNotification(notifi);

                // thêm thông báo vào cho các receiver

                ReceiverBL receiverBL = new ReceiverBL(DB);

                // lấy ra tất cả các người nhận trong Notification_DB theo các ID của người nhận có được ở bước trước
                var recievers = receiverBL.GetAllReceiverByOldID(oldReceiverIDList);

                foreach (var receiver in recievers)
                {
                    // thêm thông báo cho người nhận
                    ReceiverNotification receiverNotification = new ReceiverNotification { ReceiverNewId = receiver.Id, NotificationId = notifi.Id, IsRead = false };
                    DB.ReceiverNotificationDA.InsertReceiverNotification(receiverNotification);

                    var devices = DB.DeviceDA.GetAllReceiverDevice(receiver.Id);

                    // với mỗi thiết bị của người đó, thêm thông báo tới thiết bị
                    foreach (var device in devices)
                    {
                        DB.DeviceNotificationDA.InsertDeviceNotification(receiverNotification.Id, device.Id);
                    }
                }
                DB.commit();
                return new BaseServiceResult(ResultStatusCodes.OK, null);
            }
            catch (Exception ex)
            {
                _Logger.Error("An error ocurred while add notification", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }

        public BaseServiceResult<bool> CheckIfNotificationRead(long receiverNotificationID)
        {
            try
            {
                var result = DB.ReceiverNotificationDA.GetReceiverNotificationById(receiverNotificationID);
                if (result == null)
                {
                    return new BaseServiceResult<bool>(ResultStatusCodes.NotFound, false);
                }
                return new BaseServiceResult<bool>(ResultStatusCodes.OK, result.IsRead);
            }
            catch
            {
                return BaseServiceResult<bool>.InternalErrorResult;
            }
        }

        public BaseServiceResult<byte[]> GetNotificationContent(long notificationId, string NotificationAccessKey)
        {
            try
            {
                var notification = DB.NotificationDA.GetNotificationById(notificationId);
                if (notification == null)
                {
                    return new BaseServiceResult<byte[]>(ResultStatusCodes.NotFound, null);
                }
                if (notification.NotificationAccessKey != NotificationAccessKey)
                {
                    return new BaseServiceResult<byte[]>(ResultStatusCodes.InvalidToken, null);
                }
                return new BaseServiceResult<byte[]>(ResultStatusCodes.OK, notification.NotificationContent);
            }
            catch
            {
                return BaseServiceResult<byte[]>.InternalErrorResult;
            }
        }
    }
}