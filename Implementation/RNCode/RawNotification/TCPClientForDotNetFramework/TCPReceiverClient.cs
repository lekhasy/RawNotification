using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace TCPClientForDotNetFramework
{
    public class TCPReceiverClient
    {
        TcpClient client;

        NetworkStream nstream;

        public event Action ConnectionClosed;

        public bool Connected { get { return client.Connected; } }

        Action<byte[]> OnPacketReceived;

        public TCPReceiverClient(Action<byte[]> onPacketReceived)
        {
            client = new TcpClient(AddressFamily.InterNetwork);
            OnPacketReceived = onPacketReceived;
        }

        System.Threading.Semaphore _Smp = new Semaphore(1, 1);

        object nstreamlock = new object();

        public async Task ConnectAsync(string host, int port)
        {
            _Smp.WaitOne();
            if (!Connected)
            {
                try
                {
                    client.Close();
                    client = new TcpClient();
                    await client.ConnectAsync(host, port);
                    nstream = client.GetStream();
                }
                catch
                {
                    _Smp.Release();
                    throw;
                }
                _Smp.Release();
                StartReceive();
                return;
            }
            _Smp.Release();
        }

        public void CloseConnection()
        {
            OnConnectionClosed();
        }

        private void StartReceive()
        {
            ThreadPool.QueueUserWorkItem(((o) => {
                Monitor.Enter(nstreamlock);
                while (true)
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
                            break;
                        }
                        size = BitConverter.ToInt32(data, 0);
                        data = new byte[size];
                        nstream.Read(data, 0, size);
                        if (data.Length ==0)
                        {
                            OnConnectionClosed(); break;
                        }
                        OnPacketReceived(data);
                    }
                    catch
                    {
                        OnConnectionClosed();
                        break;
                    }
                }
                Monitor.Exit(nstreamlock);
            }));
        }
        private void OnConnectionClosed()
        {
            nstream.Close();
            if (ConnectionClosed != null) ConnectionClosed();
        }
    }
}
