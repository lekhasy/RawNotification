using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    public class TCPServer
    {
        private TcpListener ThisSever;
        public event EventHandler<TCPSeverEventArgs> OnPacketReceived;
        private LinkedList<ClientWorkSpace> AllClient = new LinkedList<ClientWorkSpace>();

        private bool IsPassiveServer;

        public TCPServer(int ListenPort, bool isPassiveServer = false)
        {
            System.Diagnostics.Debug.WriteLine("Listenning port : " + ListenPort);
            ThisSever = new TcpListener(IPAddress.Any, ListenPort);
            IsPassiveServer = isPassiveServer;
        }

        public void Start()
        {
            ThisSever.Start(5);
            WaitForConection();
        }

        public void Stop()
        {
            ThisSever.Stop();
            lock(AllClient)
            {
                var curentnode = AllClient.First;
                while (curentnode != null)
                {
                    curentnode.Value.CloseConnection();
                    curentnode = curentnode.Next;
                }
            }
        }

        public void SenDataToAllConnectedClient(byte[] data)
        {
            lock (AllClient)
            {
                var curentnode = AllClient.First;
                while (curentnode != null)
                {
                    ThreadPool.QueueUserWorkItem(((o) =>
                    {
                        (o as ClientWorkSpace).ResponseToClient(data);
                    }),curentnode.Value);
                    curentnode = curentnode.Next;
                }
            }
        }

        void WaitForConection()
        {
            ThisSever.BeginAcceptTcpClient(new AsyncCallback(delegate (IAsyncResult iar)
            {
                TcpClient Client = null;
                try
                {
                    Client = ThisSever.EndAcceptTcpClient(iar);
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("server closed");
                    return;
                }
                System.Diagnostics.Debug.WriteLine("A connection from : " + (Client.Client.RemoteEndPoint as IPEndPoint).ToString());
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
                {
                    ClientWorkSpace Workspace = new ClientWorkSpace(Client, OnPacketReceived);
                    LinkedListNode<ClientWorkSpace> node = new LinkedListNode<ClientWorkSpace>(Workspace);
                    lock (AllClient)
                    {
                        AllClient.AddLast(node);
                    }
                    if (IsPassiveServer)
                    {
                        Workspace.HoldConnection();
                    }
                    else
                    {
                        Workspace.StartReceiveRequest();
                    }
                    lock (AllClient)
                    {
                        AllClient.Remove(node);
                    }
                }));
                WaitForConection();
            }), ThisSever);
        }
    }
}