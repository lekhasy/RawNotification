using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreDataProviders;
using RawNotification.DotNetCoreUserCodeModel;
using RawNotification.SharedLibs;
using RawNotification.Models.ClientCommunicatorModels;
using RawNotification.Models.DBModels;
using RawNotification.Models;
using Windows.Networking.PushNotifications;

namespace RawNotification.DotNetCoreBLCore
{
    public class RNAdapterCore
    {
        private static PushNotificationChannel _CurrentChannel;

        static IDataProvider DAProvider = new DataProvider();

        public static Action<NotificationInfoForRequesting> NotificationReceived = null;

        public static async Task<byte[]> GetNotificationContentAsync(long NotificationId, string NotificationAccessKey)
        {
            byte[] data = await (await DAProvider.GetNotifiInfoDataProviderAsync()).GetNotificationContentAsync(NotificationId);
            if (data == null)
            {
                data = (await DAProvider.GetServiceProviderAsync().GetNotificationContentAsync(NotificationId, NotificationAccessKey, Settings.UserNewId, Settings.Token)).Data;
            }
            return data;
        }

        /// <summary>
        /// Request a new URI and set timer for renewing URI
        /// </summary>
        /// <returns></returns>
        private static async Task<string> RenewChannelAsync()
        {
            _CurrentChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            return _CurrentChannel.Uri;
        }

        public static async Task<BaseServiceResult> SendDeviceInfoToServerAsync()
        {
            // Initialize Notification Channel
            await RenewChannelAsync();
            _CurrentChannel.PushNotificationReceived += _CurrentChannel_PushNotificationReceived;
            // Send Device Info to RN
            try
            {
                var result = await DAProvider.GetServiceProviderAsync().AddDeviceAsync(Settings.UserNewId, GetDeviceInfo(_CurrentChannel), Settings.Token);
                return result;
            } catch (Exception)
            {

            }
            return null;
        }

        public async static Task<IEnumerable<NotificationInfoForRequesting>> GetAllPreviewContentAsync()
        {
            var Provider = await DAProvider.GetNotifiInfoDataProviderAsync();
            return await Provider.GetAllPreviewContentAsync();
        }

        private static async void _CurrentChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            args.Cancel = true;
            await NotificationReceivedAsync(args.RawNotification.Content);
        }

        public static async Task<byte[]> NotificationReceivedAsync(string notifiGeneralInfoData)
        {
            JSONObjectSerializer<NotificationInfoForRequesting> serializer = new SharedLibs.JSONObjectSerializer<NotificationInfoForRequesting>();
            var notifiinfo = serializer.StringToObject(notifiGeneralInfoData);
            IDataProvider DAProvider = new DataProvider();
            await (await DAProvider.GetNotifiInfoDataProviderAsync()).AddNotificationAsync(notifiinfo);

            bool? isReadAlready = null;
            try
            {
                isReadAlready = (await DAProvider.GetServiceProviderAsync().CheckIfNotificationReadAsync(notifiinfo.ReiceiverNotificationID)).Data;
                
            } catch
            {

            }
            if (isReadAlready!=null && !isReadAlready.Value)
            {
                NotificationReceived?.Invoke(notifiinfo);
            }
            return notifiinfo.NotificationPreviewContent;
        }

        internal static Device GetDeviceInfo(PushNotificationChannel channel)
        {
            Device rtDevice = new Device();
            rtDevice.IMEI = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation().Id.ToString();
            rtDevice.OSId = (int)EnumOperatingSystem.Windows;

            rtDevice.URI = channel.Uri;
            rtDevice.ReceiverNewID = DotNetCoreBLCore.Settings.UserNewId;
            return rtDevice;
        }
    }
}
