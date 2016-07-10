using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models.ClientCommunicatorModels;
using RawNotification.Models.DBModels;

namespace RawNotification.DotNetCoreDataProviders.Interfaces
{
    public interface INotificationInfoDataProvider
    {
        Task AddNotificationAsync(NotificationInfoForRequesting entity);
        Task AddNotificationContentAsync(long NotificationID, byte[] NotificationContent);
        Task<byte[]> GetNotificationContentAsync(long NotificationID);
        Task<IEnumerable<NotificationInfoForRequesting>> GetAllPreviewContentAsync();
        Task<NotificationInfoForRequesting> GetPreviewInfoAsync(long notificationId);
        Task RemoveAllData();
    }
}