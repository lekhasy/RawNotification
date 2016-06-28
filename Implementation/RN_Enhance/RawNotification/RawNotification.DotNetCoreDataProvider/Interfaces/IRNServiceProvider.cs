using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.DotNetCoreDataProviders.Interfaces
{
    public interface IRNServiceProvider
    {
        Task<BaseServiceResult> AddDeviceAsync(long userNewId, Device device, string token);
        Task<BaseServiceResult<bool>> CheckIfNotificationReadAsync(long ReceiverNotificationID);
        Task<BaseServiceResult> LogoutAsync(string DeviceIMEI, long userNewID, string token);
        Task<BaseServiceResult<byte[]>> GetNotificationContentAsync(long notificationId, string notificationAccessKey, long userNewId, string token);
    }
}
