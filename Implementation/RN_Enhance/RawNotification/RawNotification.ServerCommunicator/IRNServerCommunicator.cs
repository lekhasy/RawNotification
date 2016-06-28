using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using RawNotification.Models;

namespace RawNotification.ServerCommunicator
{
    [ServiceContract]
    public interface IRNServerCommunicator
    {
        [OperationContract]
        Task<BaseServiceResult> SendAllNotification();

        [OperationContract]
        BaseServiceResult AddNotification(byte[] Notification, byte[] NotificationPreviewContent, IEnumerable<string> oldReceiverIdList);

        [OperationContract]
        BaseServiceResult<string, long> AddReceiver(string oldReceiverId);
    }
}