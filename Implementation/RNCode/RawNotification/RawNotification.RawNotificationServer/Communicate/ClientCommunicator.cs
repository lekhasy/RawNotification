using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.MobileClient.SharedModels.NetworkPackets.FromClient;
using RawNotification.MobileClient.SharedModels.NetworkPackets.FromServer;
using RawNotification.SharedLiblary;

namespace RawNotification.RawNotificationServer.Communicate
{
    /// <summary>
    /// Class RawNotificationClientCommunicator là nơi mà hệ thống chính đưa thông tin về receiver vào, cập nhật URI, đăng nhập ...
    /// Lớp này có một action là OnUnHandledErrorOccurred, thực ra nó là đại diện cho một hàm.
    /// Khi khởi tạo class này, ta phải cung cấp một hàm có dạng void function(Exception ex)
    /// Nó được gọi ra khi có một lỗi xảy ra khi thực hiện một trong các tác vụ trên.
    /// Do class này chỉ thực hiện các công việc dưới nền, có nghĩa là nếu có lỗi đi nữa thì người dùng cũng sẽ không
    /// có tương tác gì cả, vì vậy, ta không cần phân biệt các lỗi đó mà chỉ cần đưa ra một exception để hệ thống chính lưu lỗi đó xuống là được
    /// Sau khi bắn lỗi đó ra thì chạy tiếp chương trình như chưa có lỗi gì xảy ra cả.
    /// </summary>
    internal class ClientCommunicator
    {
        #region Event and Attributes

        private Params.RawNotificationClientCommunicatorParams _Param;

        MyBitConverter<FromClientPacket> PacketConverter = new MyBitConverter<FromClientPacket>();

        MyBitConverter<FromServerPacket> ResultConverter = new MyBitConverter<FromServerPacket>();

        TCPServer.TCPServer server;  // sever dùng để giao tiếp với client
        MyBitConverter<long> _NotifyIDConverter = new MyBitConverter<long>(); // converter dùng để convert byte thành long, số long nhận được chính là notificationID được yêu cầu bởi client
        #endregion

        public ClientCommunicator(Params.RawNotificationClientCommunicatorParams param)
        {
            _Param = param;
            server = new TCPServer.TCPServer(_Param.ListenPort);
            // đăng ký sự kiện nhận được một gói tin từ client
            server.OnPacketReceived += (s, e) =>
            {
                FromClientPacket packet = null;
                try
                {
                    packet = PacketConverter.BytesToObject(e.Data);
                }
                catch
                {
                    // Bên ngoài gửi gói tin không đúng định dạng, có thể do lỗi đường truyền cũng có thể do ý đồ phá hoại
                    e.RespondToClient(ResultConverter.ObjectToBytes(new FromServerPacket(ServerResult.DataCorrupt, null)));
                    e.CloseConnection();
                    return;
                }
                switch (packet.Type)
                {
                    case FromClientPacketType.Register:
                        {
                            RegisterPacketData data = packet.Data as RegisterPacketData;
                            e.RespondToClient(ResultConverter.ObjectToBytes(new FromServerPacket(Register(data), null)));
                            e.CloseConnection();
                        }
                        break;
                    case FromClientPacketType.GetNotificationContent:
                        {
                            GetNotificationContentPacketData data = packet.Data as GetNotificationContentPacketData;
                            ServerResult result;
                            // lấy ra content
                            byte[] content = NotificationContent(data.NotificationID, out result);
                            e.RespondToClient(ResultConverter.ObjectToBytes(new FromServerPacket(result, content)));
                            e.CloseConnection();
                        }
                        break;
                }
            };
        }

        public void StartCommunicate()
        {
            server.Start();
        }

        public void StopCommunicate()
        {
            server.Stop();
        }



        public ServerResult Register(RegisterPacketData data)
        {
            try
            {
                Device newdvc = new Device { DeviceID = data.DeviceID, OSID = (int)data.OSID, URI = data.URI };
                Notification_DBDataContext db = new Notification_DBDataContext();
                // tìm recceiver
                Receiver rcv = db.Receivers.Single(r => r.OldID == data.ReceiverOldID);
                // kiểm tra device đã tồn tại
                try
                {
                    Device dvc = db.Devices.Single(dv => dv.DeviceID == data.DeviceID);
                    //thiết bị đã tồn tại thì cập nhật URI cho thiết bị rồi xóa token key
                    dvc.URI = data.URI;
                }
                catch (InvalidOperationException) // không tồn tại thiết bị thì add thiết bị mới vào
                {
                    rcv.Devices.Add(newdvc);
                }
                db.SubmitChanges();
                return ServerResult.Success;
            }
            catch(InvalidOperationException)
            {// không tìm thấy receiver
                return ServerResult.NotFound;
            }
            catch (Exception ex)
            {
                OnUnhandledErrorOccurred(ex);
                return ServerResult.Error;
            }
        }

        /// <summary>
        /// Lấy dữ liệu thông báo
        /// </summary>
        /// <param name="NotificationID">ID thông báo</param>
        /// <returns>Mảng byte dữ liệu của thông báo</returns>
        public byte[] NotificationContent(long NotificationID, out ServerResult result)
        {
            Notification_DBDataContext db = new Notification_DBDataContext();
            byte[] notificationContent = null;
            try
            {
                notificationContent = db.Notifications.Single(n => n.NotificationID == NotificationID).NotificationContent.ToArray();
            }
            catch (InvalidOperationException) // client yêu cầu notification không tồn tại
            {
                result = ServerResult.NotFound;
                return null;
            }
            catch (System.Data.SqlClient.SqlException Sqlex)
            {
                OnDbInteractionErrorOccurred(Sqlex);
                result = ServerResult.Error;
                return null;
            }
            catch (Exception ex)
            {
                OnUnhandledErrorOccurred(ex);
                result = ServerResult.Error;
                return null;
            }
            result = ServerResult.Success;
            return notificationContent;

            #region Old Code
            //try
            //{
            //byte[] notificationContent = (from drcv in db.Receivers
            //             join drcvnoti in db.ReceiverNotifications on drcv.NewID equals drcvnoti.ReceiverNewID
            //             join notis in db.Notifications on drcvnoti.NotificationID equals notis.NotificationID
            //             where drcv.OldID == rcv.ReceiverOldID && drcvnoti.NotificationID == NotificationID
            //             select notis.NotificationContent).Take(1).ElementAt(1).ToArray();

            //    if (notificationContent == null)
            //    {
            //        return default(T);
            //    }
            //    return _DBNotificationContentConverter.BytesToObject(notificationContent);
            //}
            //catch (Exception ex)
            //{
            //    OnUnHandledErrorOccurred(ex);
            //    return default(T);
            //}
            #endregion
        }


        // khi đưa sự kiện cho người dùng xử lí, có thể họ sẽ xử lí những tác vụ khá nặng, làm cho client phải chờ đợi lâu.
        // Vì vậy, khi đưa cho người dùng xử lí, ta đưa vào một thread để giúp việc trả lời client nhanh hơn

        private void OnDbInteractionErrorOccurred(System.Data.SqlClient.SqlException Sqlex)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                _Param.DBInteractionErrorOccurred(Sqlex);
            }));
        }

        private void OnUnhandledErrorOccurred(Exception ex)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                _Param.UnhandledErrorOccurred(ex);
            }));
        }
    }
}
