using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreDataProviders.RNClientService;
using RawNotification.Models;
using RawNotification.Models.DBModels;

namespace RawNotification.DotNetCoreDataProviders.Implements
{
    public class RNServiceProvider : Interfaces.IRNServiceProvider
    {
        IRNClientCommunicator Service
        {
            get
            {
                return new RNClientCommunicatorClient();
            }
        }

        public Task<BaseServiceResult<long, string>> AddDeviceAsync(long userNewId, Device device, string token)
        {
            return Service.AddDeviceAsync(userNewId, device, token);
        }

        public Task<BaseServiceResult<bool>> CheckIfNotificationReadAsync(long ReceiverNotificationID)
        {
            return Service.CheckIfNotificationReadAsync(ReceiverNotificationID);
        }

        public async Task<BaseServiceResult<byte[]>> GetNotificationContentAsync(long notificationId, string notificationAccessKey,string DeviceToken, string DeviceIMEI)
        {
            return await Service.GetNotificationContentAsync(notificationId, notificationAccessKey, DeviceToken, DeviceIMEI);
        }

        public Task<BaseServiceResult> LogoutAsync(string DeviceIMEI, string DeviceToken)
        {
            return Service.RemoveDeviceAsync(DeviceIMEI, DeviceToken);
        }

        public async Task<BaseServiceResult<long, string>> UpdateDeviceInfoAsync(Device DeviceInfo, string DeviceImEI, string DeviceToken)
        {
            return await Service.UpdateDeviceInfoAsync(DeviceInfo, DeviceImEI, DeviceToken);
        }
    }
}