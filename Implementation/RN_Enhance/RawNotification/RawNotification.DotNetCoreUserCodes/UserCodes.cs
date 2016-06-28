using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreUserCodeModel;
using RawNotification.Models.ClientCommunicatorModels;

namespace RawNotification.DotNetCoreUserCodes
{
    public static class UserCodes
    {
        public static void OnNotificationReceivedInBGTask(NotificationInfoForRequesting args)
        {
            SharedLibs.JSONObjectSerializer<string> serializer = new SharedLibs.JSONObjectSerializer<string>();
            string content = serializer.BytesToObject(args.NotificationPreviewContent);
            DotNetCoreLibs.ToastNotificationHelper.Show("New Notification received in BG", content);
        }
    }
}