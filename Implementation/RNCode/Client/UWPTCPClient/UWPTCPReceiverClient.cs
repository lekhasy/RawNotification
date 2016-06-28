using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPTCPClient
{
    public class UWPTCPReceiverClient
    {
        Windows.Networking.Sockets.StreamSocket sock = new Windows.Networking.Sockets.StreamSocket();
        public string ServerName { get; private set; }
        public string ServiceName { get; private set; }
        private Windows.Storage.Streams.DataWriter writer;
        private Windows.Storage.Streams.DataReader reader;
        private Action<byte[]> DataReceived;

        public event EventHandler ConnectionClosed;

        public UWPTCPReceiverClient(string serverName, string serviceName, Action<byte[]> dataReceived)
        {
            ServerName = serverName;
            ServiceName = serviceName;
            DataReceived += dataReceived;
        }

        private async void StartReceiveDataAsync()
        {
            // bắt đầu nhận dữ liệu về
            // kiểm tra đã đủ 4 byte chưa
            uint size = await reader.LoadAsync(sizeof(int));
            if (size != sizeof(uint))
            {
                Disconnect();
                return;
            }

            byte[] fourbytefirst = new byte[4];
            // đủ rồi thì đọc 4 byte đầu lên để xem kích thước dữ liệu
            reader.ReadBytes(fourbytefirst);

            int Packagesize = BitConverter.ToInt32(fourbytefirst, 0);

            // kiểm tra xem dữ liệu đã về đủ hết chưa
            uint datalength = await reader.LoadAsync((uint)Packagesize);
            if (datalength != Packagesize)
            {
                Disconnect();
                return;
            }

            // đủ rồi thì tạo mảng byte để lưu dữ liệu
            byte[] data = new byte[datalength];

            reader.ReadBytes(data);
            DataReceived(data);
            StartReceiveDataAsync();
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                sock = new Windows.Networking.Sockets.StreamSocket();
                Windows.Networking.HostName hostname = new Windows.Networking.HostName(ServerName);
                await sock.ConnectAsync(hostname, ServiceName);
                writer = new Windows.Storage.Streams.DataWriter(sock.OutputStream);
                reader = new Windows.Storage.Streams.DataReader(sock.InputStream);
                StartReceiveDataAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            sock.Dispose();
            OnConnectionClosed();
        }

        /// <summary>
        /// Gửi một gói tin đi
        /// </summary>
        /// <param name="data">gói tin cần gửi</param>
        /// <returns>true nếu gửi thành công, false nếu thất bại</returns>
        public async Task<bool> SendAsync(byte[] data)
        {
            try
            {
                byte[] senddata = new byte[data.Length + sizeof(int)];
                // đưa độ dài của data về dạng bytes rồi copy vào send
                Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, senddata, 0, sizeof(int));

                // copy dữ liệu còn lại vào send
                Buffer.BlockCopy(data, 0, senddata, sizeof(int), data.Length);

                writer.WriteBytes(senddata);
                await writer.StoreAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void OnConnectionClosed()
        {
            if (ConnectionClosed!=null)
            {
                ConnectionClosed(this, new EventArgs());
            }
        }
    }
}
