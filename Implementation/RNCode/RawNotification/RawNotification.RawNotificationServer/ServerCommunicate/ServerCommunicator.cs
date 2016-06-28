using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromClient;
using RawNotification.SharedLiblary;

namespace RawNotification.RawNotificationServer.ServerCommunicate
{
    internal class ServerCommunicator
    {
        private MyBitConverter<FromClientPacket> _FromClientConverter = new MyBitConverter<FromClientPacket>();

        private MyBitConverter<FromServerPacket> _FromServerConverter = new MyBitConverter<FromServerPacket>();

        TCPServer.TCPServer server;

        TCPServer.TCPServer _NotifyServer;

        int _ListenPort;
        int _NotifyPort;

        internal ServerCommunicator(int listenPort, int notifyPort, Func<ServerInfo> OnServerInfoRequestReceived, Action OnSendAllNotificationRequestReceived)
        {
            _ListenPort = listenPort;
            _NotifyPort = notifyPort;

            server = new TCPServer.TCPServer(_ListenPort);
            _NotifyServer = new TCPServer.TCPServer(_NotifyPort, true);
            server.OnPacketReceived += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Đã nhận được yêu cầu");
                FromClientPacket ReceivedPacket = null;
                try
                {
                    ReceivedPacket = _FromClientConverter.BytesToObject(e.Data);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Gói tin bị lỗi khi vận chuyển");
                    e.RespondToClient(_FromServerConverter.ObjectToBytes(new FromServerPacket(FromServerPacketType.DataCorrupted, ex)));
                    return;
                }
                // Giữa 2 server với nhau phải luôn được kết nối, vì vậy khi giao tiếp xong thì vẫn để kết nối đó cho lần tiếp theo
                switch (ReceivedPacket.PacketType)
                {
                    case FromClientPacketType.AddNotification:
                        {
                            e.RespondToClient(_FromServerConverter.ObjectToBytes(new FromServerPacket(FromServerPacketType.AddNotificationResponse, AddNotification(ReceivedPacket.Data as AddNotificationPacketData)))); break;
                        }
                    case FromClientPacketType.RequestServerInfo:
                        {
                            e.RespondToClient(_FromServerConverter.ObjectToBytes(new FromServerPacket(FromServerPacketType.ServerInfo, OnServerInfoRequestReceived()))); break;
                        }
                    case FromClientPacketType.SendAllNotification:
                        { // nhận được yêu cầu gửi thì khỏi cần hồi âm
                            OnSendAllNotificationRequestReceived(); break;
                        }
                }
            };
        }

        public void SendServerInfoToAllClient(ServerInfo info)
        {
            _NotifyServer.SenDataToAllConnectedClient(_FromServerConverter.ObjectToBytes(new FromServerPacket(FromServerPacketType.ServerInfo, info)));
        }

        internal AddNotificationFSPacketData AddNotification(AddNotificationPacketData addNotification)
        {
            Notification_DBDataContext db = new Notification_DBDataContext();

            Notification notifi = new Notification { NotificationContent = new System.Data.Linq.Binary(addNotification.NotificationContent) };
            db.Notifications.InsertOnSubmit(notifi);

            #region thêm thông báo vào cho các receiver
            // lấy ra tất cả các người nhận trong Notification_DB theo các ID của người nhận có được ở bước trước
            var recievers = db.Receivers.Where(rc => addNotification.ReceiversOldID.Contains(rc.OldID));

            try
            {
                foreach (var receiver in recievers)
                {   // tạo thông báo cho mỗi người nhận
                    ReceiverNotification newreceivernoti = new ReceiverNotification { Notification = notifi };
                    // thêm thông báo cho người nhận
                    receiver.ReceiverNotifications.Add(newreceivernoti);
                    foreach (var device in receiver.Devices)
                    { // với mỗi thiết bị của người đó, thêm thông báo tới thiết bị
                        Console.WriteLine("add device notification");
                        device.DeviceNotifications.Add(new DeviceNotification { ReceiverNotification = newreceivernoti });
                    }
                }


                db.SubmitChanges();
                return new AddNotificationFSPacketData(true, ComminucateServerErrorType.None, null);
            }
            catch (System.Data.SqlClient.SqlException Sqlex)
            {
                return new AddNotificationFSPacketData(false, ComminucateServerErrorType.SqlError, Sqlex);
            }
            catch (Exception ex)
            {
                return new AddNotificationFSPacketData(false, ComminucateServerErrorType.Unknow, ex);
            }

            #endregion
        }

        internal void StartCommunicate()
        {
            _NotifyServer.Start();
            server.Start();
        }

        internal void StopCommunicate()
        {
            server.Stop();
            _NotifyServer.Stop();
        }
    }
}
