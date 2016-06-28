using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromClient;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer;

namespace RawNotification.ServerClient.ServerInterface
{
    public class ServerInterface<T>
    {
        public bool ConnectedToNotifyServer { get { return _NotifyReceiveClient.Connected; } }
        public event Action LossConnectionToServer;
        public event Action LossConnectionToNotifyServer;
        TCPClientForDotNetFramework.TCPClient _Client = new TCPClientForDotNetFramework.TCPClient();
        TCPClientForDotNetFramework.TCPReceiverClient _NotifyReceiveClient;
        Liblary.MyBitConverter<FromClientPacket> _FromClientConverter = new Liblary.MyBitConverter<FromClientPacket>();
        Liblary.MyBitConverter<FromServerPacket> FromServerConverter = new Liblary.MyBitConverter<FromServerPacket>();
        Liblary.MyBitConverter<T> _NotifyContentConverter = new Liblary.MyBitConverter<T>();
        public event Action<ServerInfo> ServerInfoChanged;

        public ServerInterface()
        {
            _NotifyReceiveClient = new TCPClientForDotNetFramework.TCPReceiverClient((ReceivedPacket) =>
            {
                if (ServerInfoChanged != null)
                {
                    ServerInfo info;
                    try
                    {
                        info = FromServerConverter.BytesToObject(ReceivedPacket).Data as ServerInfo;
                    }
                    catch
                    {
                        return;
                    }
                    ServerInfoChanged(info);
                }
            });

            _Client.ConnectionClosed += () =>
             {
                 if (LossConnectionToServer != null)
                 {
                     LossConnectionToServer();
                 }
             };
            _NotifyReceiveClient.ConnectionClosed += () =>
            {
                if (LossConnectionToNotifyServer != null)
                {
                    LossConnectionToNotifyServer();
                }
            };
        }

        public void CloseAllConnection()
        {
            _Client.CloseConnection();
            _NotifyReceiveClient.CloseConnection();
        }

        /// <summary>
        /// Kết nối tới raw notification server. Exception được bắn khi gặp lỗi
        /// </summary>
        /// <param name="host">Tên hoặc IP của host</param>
        /// <param name="port">Port của host</param>
        public async Task ConnectToRawNotificationServerAsync(string serverName, int port)
        {
            await _Client.ConnectAsync(serverName, port);
        }

        public async Task ConnectToNotifyNotificationServerInfoAsync(string serverName, int port)
        {
            await _NotifyReceiveClient.ConnectAsync(serverName, port);
        }

        /// <summary>
        /// Gửi yêu cầu thêm thông báo tới cho server
        /// </summary>
        /// <param name="notifyObject">Đối tượng dữ liệu cần gửi</param>
        /// <param name="receivers">Danh sách người nhận</param>
        /// <returns>Phương thức này sẽ trả về một đối tượng AddNotificationFSPacketData giúp mô tả kết quả từ server. 
        /// Trong trường hợp gặp sự cố kết nối, nó sẽ bắn ra một SocketException nếu lỗi khi gửi hoặc trả về null nếu lỗi xảy ra sau khi gửi hoàn thành</returns>
        public async Task<AddNotificationFSPacketData> AddNotificationAsync(T notifyObject, List<IReceiver> receivers)
        {
            AddNotificationPacketData PacketData =
                new AddNotificationPacketData(_NotifyContentConverter.ObjectToBytes(notifyObject),
                receivers.Select(r => r.RNReceiverOldID).ToList());

            FromClientPacket SendPacket = new FromClientPacket(FromClientPacketType.AddNotification, PacketData);
            TCPClientForDotNetFramework.SendAndReceiveResult result = await _Client.SendAndReceiveAsync(_FromClientConverter.ObjectToBytes(SendPacket));

            if (result.IsSuccess) // thành công thì trả về dữ liệu
            {
                try
                {
                    return FromServerConverter.BytesToObject(result.Data).Data as AddNotificationFSPacketData;
                }
                catch (Exception ex)
                {   // nhận về thành công rồi mà mở ra bị lỗi thì là do hệ thống có vấn đề
                    throw ex;
                }
            }
            else
            {// lỗi khi đang gửi thì trả về null
                if (result.ErrorPoint == TCPClientForDotNetFramework.ErrorPoint.Sending)
                {
                    throw new System.Net.Sockets.SocketException();
                }
                else
                { // lỗi trong khi nhận dữ liệu
                    return null;
                }
            }
        }

        public async Task<bool> SendAllNotificationAsync()
        {
            FromClientPacket SendPacket = new FromClientPacket(FromClientPacketType.SendAllNotification, null);

            return await _Client.SendAsync(_FromClientConverter.ObjectToBytes(SendPacket));
        }
    }
}
