using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models.ClientCommunicatorModels;
using RawNotification.Models.DBModels;

namespace RawNotification.DotNetCoreDataProviders.Implements
{
    public class NotificationInfoDataProvider : Interfaces.INotificationInfoDataProvider
    {
        const string NOTIFICATION_DB_FILENAME = "ReceiverNotifications.bin";

        const string NOTIFICATION_DB_CONTENT = "NotificationContents.bin";

        ListEntitySerializer<NotificationInfoForRequesting> Notifications;

        ListEntitySerializer<Notification> NotificationContents;

        public async static Task<NotificationInfoDataProvider> CreateNew()
        {
            NotificationInfoDataProvider provider = new NotificationInfoDataProvider();
            provider.Notifications = await ListEntitySerializer<NotificationInfoForRequesting>.CreateListAsync(NOTIFICATION_DB_FILENAME);
            provider.NotificationContents = await ListEntitySerializer<Notification>.CreateListAsync(NOTIFICATION_DB_CONTENT);
            return provider;
        }

        private NotificationInfoDataProvider()
        {

        }

        public async Task AddNotificationAsync(NotificationInfoForRequesting entity)
        {
            await Notifications.Add(entity);
        }

        public async Task AddNotificationContentAsync(long NotificationID, byte[] NotificationContent)
        {
            var notify = await NotificationContents.FirstOrDefault(n => n.Id == NotificationID);
            notify.NotificationContent = NotificationContent;
            await Notifications.SaveAsync();
        }

        public async Task<byte[]> GetNotificationContentAsync(long NotificationID)
        {
            var notification = await NotificationContents.FirstOrDefault(n => n.Id == NotificationID);
            if (notification == null)
                return null;
            return notification.NotificationContent;
        }

        public async Task<byte[]> GetNotificationPreviewContent(long NotificationID)
        {
            var notifi = await Notifications.First(n => n.NotificationId == NotificationID);
            if (notifi != null)
                return notifi.NotificationPreviewContent;
            return null;
        }

        public async Task<IEnumerable<NotificationInfoForRequesting>> GetAllPreviewContentAsync()
        {
            IEnumerable<byte[]> rtModel = new List<byte[]>();

            return await Notifications.GetIEnumrableAsync();
        }

        public Task<NotificationInfoForRequesting> GetPreviewInfoAsync(long notificationId)
        {
            return Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
        }

        public async Task RemoveAllData()
        {
            await Notifications.RemoveListData();
            await NotificationContents.RemoveListData();
        }
    }
}

