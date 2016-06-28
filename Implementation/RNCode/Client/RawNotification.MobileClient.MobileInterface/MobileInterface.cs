using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace RawNotification.MobileClient.MobileInterface
{
    public static class MobileInterface
    {
        static SharedLiblary.MyBitConverter<SharedModels.NetworkPackets.FromClient.FromClientPacket> _FromClientPacketConverter = new SharedLiblary.MyBitConverter<SharedModels.NetworkPackets.FromClient.FromClientPacket>();
        static SharedLiblary.MyBitConverter<SharedModels.NetworkPackets.FromServer.FromServerPacket> _FromServerPacketConverter = new SharedLiblary.MyBitConverter<SharedModels.NetworkPackets.FromServer.FromServerPacket>();
        static SharedLiblary.MyBitConverter<TestInterface.SharedModels.NotificationDataPacket> _NotificationContentConverter = new SharedLiblary.MyBitConverter<TestInterface.SharedModels.NotificationDataPacket>();

        public static async Task ReceivedNewNotificatonID(long notificationid)
        {
            byte[] receivedContent = await GetNotificationContentAsync(notificationid);
            var contentobject = _NotificationContentConverter.BytesToObject(receivedContent);
            Notification_Helper_Client.Toast_Notification.Show(contentobject.Line1, contentobject.Line2);
        }
        


        public static async void SendURI()
        {
            var ServerName = LocalSettings.LocalSettingsManager.ServerIP;
            var PortName = LocalSettings.LocalSettingsManager.RNSClientPort;
            var DeviceID = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation().Id.ToString();
            PushNotificationChannel chanel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            System.Diagnostics.Debug.WriteLine(chanel.Uri);
            System.Diagnostics.Debug.WriteLine(DeviceID);
            if (chanel.Uri == null) return;
            UWPTCPClient.UWPTCPClient client = new UWPTCPClient.UWPTCPClient(ServerName, PortName);
            try
            {
                await client.ConnectAsync();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("connect lỗi");
                return;
            }


            UWPTCPClient.SendAndReceiveResult Result = await client.SendAndRecieveAsync(_FromClientPacketConverter.ObjectToBytes(
                new SharedModels.NetworkPackets.FromClient.FromClientPacket(
                    SharedModels.NetworkPackets.FromClient.FromClientPacketType.Register,
                    new SharedModels.NetworkPackets.FromClient.RegisterPacketData(DeviceID,
                    chanel.Uri, SharedModels.NetworkPackets.OperatingSystemIDTemplates.Windows10,
                    LocalSettings.LocalSettingsManager.UserName
                ))));
            if (Result.IsSuccess)
            {
                if (_FromServerPacketConverter.BytesToObject(Result.Data).ResultType == SharedModels.NetworkPackets.FromServer.ServerResult.Success)
                {
                    System.Diagnostics.Debug.WriteLine("ok");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ko register được");
                }
            }
        }

        /// <summary>
        /// Trả về byte[] chứa content, việc chuyển đổi về kiểu dữ liệu dùng được thì cần phải làm ở bước bên ngoài
        /// </summary>
        /// <param name="notificationid">ID  của Notification</param>
        /// <returns></returns>
        private static async Task<byte[]> GetNotificationContentAsync(long notificationid)
        {
            var ServerName = LocalSettings.LocalSettingsManager.ServerIP;
            var PortName = LocalSettings.LocalSettingsManager.RNSClientPort;

            UWPTCPClient.UWPTCPClient client = new UWPTCPClient.UWPTCPClient(ServerName, PortName);

            if(await client.ConnectAsync())
            {
                UWPTCPClient.SendAndReceiveResult result = await client.SendAndRecieveAsync(
                    _FromClientPacketConverter.ObjectToBytes(
                        new SharedModels.NetworkPackets.FromClient.FromClientPacket(
                            SharedModels.NetworkPackets.FromClient.FromClientPacketType.GetNotificationContent,
                            new SharedModels.NetworkPackets.FromClient.GetNotificationContentPacketData(notificationid))));

                if (result.IsSuccess)
                {
                    return _FromServerPacketConverter.BytesToObject(result.Data).Data;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Lỗi khi liên lạc vs server");
                    return null;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Lỗi kết nối");
                return null;
            }
        }
    }
}
