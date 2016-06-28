using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer;
namespace RawNotification.RawNotificationServer
{
    public class RawNotificationServer
    {
        Sender.RawNotificationSender _RawSender;
        Communicate.ClientCommunicator _ClientCommunicator;
        ServerCommunicate.ServerCommunicator _ServerCommunicator;

        public RawNotificationServer(Params.RawNotificationServerParam param, int serverListenPort, int serverNotifyPort)
        {
            _ClientCommunicator = new Communicate.ClientCommunicator(param.ClientCommunicatorParam);
            _RawSender = new Sender.RawNotificationSender(param.RawNotificationSenderParam, OnErrorInfoChanged);
            _ServerCommunicator = new ServerCommunicate.ServerCommunicator(serverListenPort, serverNotifyPort, GetServerInfo, SendAllNotification );
        }

        private void OnErrorInfoChanged(object sender, ServerInfo info)
        {
            _ServerCommunicator.SendServerInfoToAllClient(info);
        }

        private ServerInfo GetServerInfo()
        {
            return _RawSender.GetServerInfo();
        }

        private void SendAllNotification()
        {
             _RawSender.SubmitSendAsync();
        }

        /// <summary>
        /// Bắt đầu khởi động server, Client có thể yêu cầu nhận nội dung thông báo sau khi hàm này chạy xong
        /// </summary>
        public void Start()
        {
            _ClientCommunicator.StartCommunicate();
            _ServerCommunicator.StartCommunicate();
        }

        /// <summary>
        /// Dừng Server, Client không thể yêu cầu nội dung thông báo nữa
        /// </summary>
        public void Stop()
        {
            _ClientCommunicator.StopCommunicate();
            _ServerCommunicator.StopCommunicate();
        }
    }
}
