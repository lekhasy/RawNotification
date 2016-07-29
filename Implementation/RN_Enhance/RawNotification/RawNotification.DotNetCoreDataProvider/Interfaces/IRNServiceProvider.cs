using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models;
using RawNotification.Models.DBModels;

namespace RawNotification.DotNetCoreDataProviders.Interfaces
{
    public interface IRNServiceProvider
    {
        Task<BaseServiceResult<long, string>> AddDeviceAsync(long userNewId, Device device, string token);
        Task<BaseServiceResult<long, string>> UpdateDeviceInfoAsync(Device DeviceInfo, string DeviceImEI, string DeviceToken);
        Task<BaseServiceResult<bool>> CheckIfNotificationReadAsync(long ReceiverNotificationID);
        Task<BaseServiceResult> LogoutAsync(string DeviceIMEI, string DeviceToken);
        Task<BaseServiceResult<byte[]>> GetNotificationContentAsync(long notificationId, string notificationAccessKey, string DeviceToken, string DeviceIMEI);
    }
}
