using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreBLCore;
using RawNotification.DotNetCoreDataProviders;
using RawNotification.DotNetCoreDataProviders.Interfaces;
using RawNotification.DotNetCoreUserCodeModel;
using RawNotification.Models.ClientCommunicatorModels;
using RawNotification.Models.DBModels;
using RawNotification.Models;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;

namespace RawNotification.DotNetCoreBL
{
    public static class RNAdapter
    {
        private static Dictionary<int, Action<NotificationInfoForRequesting>> ReceivedNotification = new Dictionary<int, Action<NotificationInfoForRequesting>>(4);

        public static void RegisterNotificationReceivedAsyncEvent(Action<NotificationInfoForRequesting> handler, int Id)
        {
            Action<NotificationInfoForRequesting> value;
            if (ReceivedNotification.TryGetValue(Id, out value))
            {
                ReceivedNotification.Remove(Id);
                ReceivedNotification.Add(Id, handler);
            }
            else
            {
                ReceivedNotification.Add(Id, handler);
            }
        }

        public async static Task<byte[]> GetNotificationContentAsync(long parameter, string NotificationAccessKey)
        {
            return await RNAdapterCore.GetNotificationContentAsync(parameter, NotificationAccessKey);
        }

        private static void FireNotificationReceivedEvent(NotificationInfoForRequesting args)
        {
            foreach (var item in ReceivedNotification)
            {
                item.Value.Invoke(args);
            }
        }

        public async static Task<IEnumerable<NotificationInfoForRequesting>> GetAllPreviewData()
        {
            return await RNAdapterCore.GetAllPreviewContentAsync();
        }

        /// <summary>
        /// Intialize RN Adapter
        /// </summary>
        /// <param name="refreshTimerInterval">Interval for Background timer, it will send URI to server after this perioid of time, measure in minute. This value must be greater than 15</param>
        /// <param name="UserID">UserId that received from login server</param>
        /// <param name="Token">Token that have receceived from login server</param>
        /// <returns></returns>
        public static async Task<BaseServiceResult> InitializeAsync(TimeSpan refreshTimerInterval, long UserID = 0, string Token = null)
        {
            if (Token != null)
            {
                long prevuserId = -1;
                try
                {
                    prevuserId = Settings.UserNewId;
                }
                catch { }

                if (prevuserId != -1 && UserID != prevuserId)
                {
                    IDataProvider provider = new DataProvider();
                    await (await provider.GetNotifiInfoDataProviderAsync()).RemoveAllData();
                }
                Settings.UserNewId = UserID;
                Settings.Token = Token;
            }
            try
            {
                if (DotNetCoreBLCore.RNAdapterCore.NotificationReceived == null)
                {
                    RNAdapterCore.NotificationReceived += FireNotificationReceivedEvent;
                }
                return await DotNetCoreBLCore.RNAdapterCore.SendDeviceInfoToServerAsync();
            }
            finally
            {
                #region Xin quyền chạy nền
                // yêu cầu quyền chạy Background từ user
                // câu lệnh này sẽ đưa cho user một thông báo yêu cầu cấp quyền một lần duy nhất
                // nếu user chọn, lần sau nó sẽ không hiện lên nữa.
                BackgroundExecutionManager.RemoveAccess();
                BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
                #endregion

                Utilities.RegisterNotificationBackgroundTask();
                Utilities.RegisterTimerBackgroundTask(refreshTimerInterval);
            }
        }

        public static async Task Logout(bool keepData)
        {
            IDataProvider provider = new DataProvider();
            try
            {
                if (!keepData)
                    await (await provider.GetNotifiInfoDataProviderAsync()).RemoveAllData();
                await provider.GetServiceProviderAsync().LogoutAsync(Utilities.GetDeviceIMEI(), Settings.UserNewId, Settings.Token);
            }
            catch { }
            finally
            {
                Settings.Token = null;
                Settings.UserNewId = 0;
            }
        }
    }
}