using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreDataProviders.RNClientService;
using RawNotification.Models;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.DotNetCoreDataProviders.Implements
{
    public class RNServiceProvider : Interfaces.IRNServiceProvider
    {

        IRNClientCommunicator _Service;

        IRNClientCommunicator Service
        {
            get
            {
                return new RNClientCommunicatorClient();
            }
        }

        public Task<BaseServiceResult> AddDeviceAsync(long userNewId, Device device, string token)
        {
            return Service.AddDeviceAsync(userNewId, device, token);
        }

        public Task<BaseServiceResult<bool>> CheckIfNotificationReadAsync(long ReceiverNotificationID)
        {
            return Service.CheckIfNotificationReadAsync(ReceiverNotificationID);
        }

        public async Task<BaseServiceResult<byte[]>> GetNotificationContentAsync(long notificationId, string notificationAccessKey, long userNewId, string token)
        {
            return await Service.GetNotificationContentAsync(notificationId, notificationAccessKey, token, userNewId);
        }

        public Task<BaseServiceResult> LogoutAsync(string DeviceIMEI, long userNewID, string token)
        {
            return Service.RemoveDeviceAsync(DeviceIMEI, userNewID, token);
        }
    }
}