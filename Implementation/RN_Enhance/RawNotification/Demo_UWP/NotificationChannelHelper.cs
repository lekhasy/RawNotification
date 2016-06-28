using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace Demo_UWP
{
    public static class NotificationChannelHelper
    {

        public static PushNotificationChannel CurrentChannel
        { get; private set; }

        /// <summary>
        /// Request a new URI and set timer for renewing URI
        /// </summary>
        /// <returns></returns>
        public static async Task<string> RenewChannelAsync()
        {
            CurrentChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            return CurrentChannel.Uri;
        }
    }
}
