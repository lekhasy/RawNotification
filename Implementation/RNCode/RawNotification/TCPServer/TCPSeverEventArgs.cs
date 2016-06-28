using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public class TCPSeverEventArgs : EventArgs
    {
        Action<object> _ChangeStateAction;
        Action<byte[]> _RespondToClient;
        Action _CloseConnection;
        public TCPSeverEventArgs(NetworkStream stream, object state, byte[] data, Action<object> changeStateAction, Action<byte[]> respondToClient, Action closeConnection)
        {
            CurentStream = stream;
            State = state;
            Data = data;
            this._ChangeStateAction = changeStateAction;
            this._RespondToClient = respondToClient;
            this._CloseConnection = closeConnection;
        }

        public NetworkStream CurentStream { get; private set; }
        public object State { get; private set; }
        public byte[] Data { get; private set; }

        public void ChangeState(object value)
        {
            _ChangeStateAction(value);
        }

        public void RespondToClient(byte[] data)
        {
            _RespondToClient(data);
        }

        public void CloseConnection()
        {
            _CloseConnection();
        }
    }
}
