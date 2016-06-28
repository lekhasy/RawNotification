using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace TCPServer
{
    internal class ClientWorkSpace
    {
        private NetworkStream nstream;
        private EventHandler<TCPSeverEventArgs> OnPacketReceived;
        private object state;

        public ClientWorkSpace(TcpClient client, EventHandler<TCPSeverEventArgs> OnPacketReceived)
        {
            nstream = client.GetStream();

            this.OnPacketReceived = OnPacketReceived;
        }

        internal void CloseConnection()
        {
            nstream.Close();
        }

        internal void ResponseToClient(byte[] datas)
        {
            // nếu bên ngoài có 2 thread gọi hàm này thì nó sẽ được xử lí đồng bộ
            Monitor.Enter(nstream);
            // khai báo một mảng byte có số byte > item 4 ô
            byte[] send = new byte[datas.Length + 4];

            System.Diagnostics.Debug.WriteLine(datas.Length);
            // đưa độ dài của item về dạng bytes rồi copy vào send
            Buffer.BlockCopy(BitConverter.GetBytes(datas.Length), 0, send, 0, 4);

            // copy dữ liệu còn lại vào send
            Buffer.BlockCopy(datas, 0, send, 4, datas.Length);

            // gửi đồng bộ
            try
            {
                nstream.Write(send, 0, send.Length);
            }
            catch
            {
                nstream.Close();
            }
            Monitor.Exit(nstream);
        }

        private void changeState(object value)
        {
            state = value;
        }

        internal void HoldConnection()
        {
            try
            {
                if (nstream.Read(new byte[1], 0, 1) == 0)
                    nstream.Close();
                return;
            }
            catch
            {
                nstream.Close();
                return;
            }
        }

        internal void StartReceiveRequest()
        {
            Monitor.Enter(nstream);
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
                        nstream.Close();
                        break;
                    }
                    size = BitConverter.ToInt32(data, 0);
                    data = new byte[size];
                    nstream.Read(data, 0, size);
                    OnPacketReceived(this, new TCPSeverEventArgs(nstream, state, data, changeState, ResponseToClient, CloseConnection));
                }
                catch(Exception ex)
                {
                    nstream.Close();
                    break;
                }
            }
            Monitor.Exit(nstream);
        }

        ~ClientWorkSpace()
        {
            System.Diagnostics.Debug.WriteLine("A Client WorkSpace was Disposed");
        }
    }
}
