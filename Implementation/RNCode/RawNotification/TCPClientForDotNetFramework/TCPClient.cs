using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TCPClientForDotNetFramework
{
    public class TCPClient
    {
        TcpClient client;

        NetworkStream nstream;

        public event Action ConnectionClosed;

        public TCPClient()
        {
            client = new TcpClient(AddressFamily.InterNetwork);
        }

        object nstreamlock = new object();

        /// <summary>
        /// Kết nối tới server. Exception được bắn khi gặp lỗi
        /// </summary>
        /// <param name="host">Tên hoặc IP của host</param>
        /// <param name="port">Port của host</param>
        /// <returns></returns>
        public async Task ConnectAsync(string host, int port)
        {
            Monitor.Enter(nstreamlock);
            try
            {
                client.Close();
                client = new TcpClient();
                await client.ConnectAsync(host, port);
                nstream = client.GetStream();
            }
            catch
            {
                Monitor.Exit(nstreamlock);
                throw;
            }
            Monitor.Exit(nstreamlock);
        }

        public void CloseConnection()
        {
            Monitor.Enter(nstreamlock);
            OnConnectionClosed();
            Monitor.Exit(nstreamlock);
        }

        public Task<bool> SendAsync(byte[] buffers)
        {
            return Task.Run(new Func<bool>(() =>
            {
                // nếu bên ngoài có 2 thread gọi hàm này thì nó sẽ được xử lí đồng bộ
                Monitor.Enter(nstreamlock);
                bool result = Send(buffers);
                Monitor.Exit(nstreamlock);
                return result;
            }));
        }

        private bool Send(byte[] buffers)
        {
            // khai báo một mảng byte có số byte > item 4 ô
            byte[] send = new byte[buffers.Length + 4];

            // đưa độ dài của item về dạng bytes rồi copy vào send
            Buffer.BlockCopy(BitConverter.GetBytes(buffers.Length), 0, send, 0, 4);

            // copy dữ liệu còn lại vào send
            Buffer.BlockCopy(buffers, 0, send, 4, buffers.Length);

            // gửi đồng bộ
            try
            {
                nstream.Write(send, 0, send.Length);
            }
            catch
            {
                OnConnectionClosed();
                return false;
            }
            return true;
        }

        private byte[] Receive()
        {
            int size = sizeof(int);
            byte[] data;

            // đọc request từ buffer
            // đọc 4 byte đầu tiên vào data để biết kích thước gói tin
            data = new byte[size];
            try
            {
                nstream.Read(data, 0, size);
                if (data.Length == 0)
                {
                    OnConnectionClosed();
                    return null;
                }
                size = BitConverter.ToInt32(data, 0);

                data = new byte[size];

                nstream.Read(data, 0, size);
                if (data.Length == 0)
                {
                    OnConnectionClosed();
                    return null;
                }
                return data;
            }
            catch
            {
                OnConnectionClosed();
                return null;
            }
        }

        /// <summary>
        /// Gửi một số gói tin và đợi gói hồi đáp từ server
        /// </summary>
        /// <param name="buffers">gói trả về từ server</param>
        /// <returns></returns>
        public async Task<SendAndReceiveResult> SendAndReceiveAsync(byte[] buffer)
        {
            return await Task.Run(new Func<SendAndReceiveResult>(() =>
            {
                Monitor.Enter(nstreamlock);
                if (!Send(buffer)) // send không thành công
                {
                    return new SendAndReceiveResult(false, ErrorPoint.Sending, null);
                }
                byte[] data = Receive();
                if (data == null) // receive không thành công
                {
                    return new SendAndReceiveResult(false, ErrorPoint.Receiving, null);
                }
                Monitor.Exit(nstreamlock);
                return new SendAndReceiveResult(true, ErrorPoint.Sending, data);
            }));
        }


        private Task<byte[]> ReceiveAsync()
        {
            return Task.Run(new Func<byte[]>(() =>
            {
                Monitor.Enter(nstreamlock);
                int size = sizeof(int);
                byte[] data;

                // đọc request từ buffer
                // đọc 4 byte đầu tiên vào data để biết kích thước gói tin
                data = new byte[size];
                try
                {
                    nstream.Read(data, 0, size);
                    if (data.Length == 0)
                    {
                        OnConnectionClosed();
                        Monitor.Exit(nstreamlock);
                        return null;
                    }
                    size = BitConverter.ToInt32(data, 0);
                    data = new byte[size];
                    nstream.Read(data, 0, size);
                    if (data.Length == 0)
                    {
                        OnConnectionClosed();
                        Monitor.Exit(nstreamlock);
                        return null;
                    }
                    Monitor.Exit(nstreamlock);
                    return data;
                }
                catch
                {
                    OnConnectionClosed();
                    Monitor.Exit(nstreamlock);
                    return null;
                }
            }));
        }

        private void OnConnectionClosed()
        {
            nstream.Close();
            if (ConnectionClosed != null) ConnectionClosed();
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
