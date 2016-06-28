using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UWPTCPClient
{
    public class UWPTCPClient
    {
        Windows.Networking.Sockets.StreamSocket sock = new Windows.Networking.Sockets.StreamSocket();
        public string ServerName { get; private set; }
        public string ServiceName { get; private set; }
        private Windows.Storage.Streams.DataWriter writer;
        private Windows.Storage.Streams.DataReader reader;

        public event EventHandler ConnectionClosed;

        object nstreamlock = new object();

        public UWPTCPClient(string serverName, string serviceName)
        {
            ServerName = serverName;
            ServiceName = serviceName;
        }

        private async Task<byte[]> ReceiveDataAsync()
        {
            // bắt đầu nhận dữ liệu về
            // kiểm tra đã đủ 4 byte chưa
            uint size = await reader.LoadAsync(sizeof(int));
            System.Diagnostics.Debug.WriteLine("Load 1");
            if (size != sizeof(uint))
            {
                Disconnect();
                return null;
            }

            byte[] fourbytefirst = new byte[4];
            // đủ rồi thì đọc 4 byte đầu lên để xem kích thước dữ liệu
            reader.ReadBytes(fourbytefirst);

            
            int Packagesize = BitConverter.ToInt32(fourbytefirst, 0);

            // kiểm tra xem dữ liệu đã về đủ hết chưa
            uint datalength = await reader.LoadAsync((uint)Packagesize);
            System.Diagnostics.Debug.WriteLine("Load 2");
            if (datalength != Packagesize)
            {
                Disconnect();
                return null;
            }
            System.Diagnostics.Debug.WriteLine("Load 3");
            // đủ rồi thì tạo mảng byte để lưu dữ liệu
            byte[] data = new byte[datalength];
            System.Diagnostics.Debug.WriteLine("Load 4");
            reader.ReadBytes(data);
            System.Diagnostics.Debug.WriteLine("Load 5");
            return data;
        }

        public async Task<bool> ConnectAsync()
        {
            //Monitor.Enter(nstreamlock);
            try
            {
                sock = new Windows.Networking.Sockets.StreamSocket();
                Windows.Networking.HostName hostname = new Windows.Networking.HostName(ServerName);
                await sock.ConnectAsync(hostname, ServiceName);
                writer = new Windows.Storage.Streams.DataWriter(sock.OutputStream);
                reader = new Windows.Storage.Streams.DataReader(sock.InputStream);
                //Monitor.Exit(nstreamlock);
                return true;
            }
            catch
            {
                Disconnect();
               // Monitor.Exit(nstreamlock);
                return false;
            }
        }

        public async Task<SendAndReceiveResult> SendAndRecieveAsync(byte[] buffer)
        {
            return await Task.Run(new Func<Task<SendAndReceiveResult>>(async () =>
            {
                //Monitor.Enter(nstreamlock);
                if (!await SendAsync(buffer)) // send không thành công
                {
                  //  Monitor.Exit(nstreamlock);
                    return new SendAndReceiveResult(false, ErrorPoint.Sending, null);
                }
                byte[] data = await ReceiveDataAsync();
                System.Diagnostics.Debug.WriteLine("Load 6");
                if (data == null) // receive không thành công
                {
                    System.Diagnostics.Debug.WriteLine("Load 7");
                    //Monitor.Exit(nstreamlock);
                    return new SendAndReceiveResult(false, ErrorPoint.Receiving, null);
                }
                System.Diagnostics.Debug.WriteLine("Load 8");
                //Monitor.Exit(nstreamlock);
                System.Diagnostics.Debug.WriteLine("Load 9");
                return new SendAndReceiveResult(true, ErrorPoint.Sending, data);
            }));
        }
        Mutex m = new Mutex();

        public void Disconnect()
        {
           // Monitor.Enter(nstreamlock);
            sock.Dispose();
            OnConnectionClosed();
           // Monitor.Exit(nstreamlock);
        }

        /// <summary>
        /// Gửi một gói tin đi
        /// </summary>
        /// <param name="data">gói tin cần gửi</param>
        /// <returns>true nếu gửi thành công, false nếu thất bại</returns>
        public async Task<bool> SendAsync(byte[] data)
        {
           // Monitor.Enter(nstreamlock);
            try
            {
                byte[] senddata = new byte[data.Length + sizeof(int)];
                // đưa độ dài của data về dạng bytes rồi copy vào send
                Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, senddata, 0, sizeof(int));

                // copy dữ liệu còn lại vào send
                Buffer.BlockCopy(data, 0, senddata, sizeof(int), data.Length);

                writer.WriteBytes(senddata);
                await writer.StoreAsync();
               // Monitor.Exit(nstreamlock);
                return true;
            }
            catch
            {
                Disconnect();
              //  Monitor.Exit(nstreamlock);
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
    public class SendAndReceiveResult
    {
        public bool IsSuccess { get; private set; }
        public ErrorPoint ErrorPoint { get; private set; }
        public byte[] Data { get; private set; }

        internal SendAndReceiveResult(bool issuccess, ErrorPoint Point, byte[] data)
        {
            IsSuccess = issuccess;
            ErrorPoint = Point;
            Data = data;
        }
    }
    public enum ErrorPoint
    {
        Sending,
        Receiving,
    }
}