using System.ServiceModel;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.ClientCommunicator
{
    [ServiceContract]
    public interface IRNClientCommunicator
    {
        [OperationContract]
        BaseServiceResult AddDevice(long ReceiverId, Device deviceInfo, string ReceiverToken);
        [OperationContract]
        BaseServiceResult<byte[]> GetNotificationContent(long NotificationId, string NotificationAccessKey, string ReceiverToken, long ReceiverId);
        [OperationContract]
        BaseServiceResult<bool> CheckIfNotificationRead(long ReceiverNotificationID);
        [OperationContract]
        BaseServiceResult RemoveDevice(string DeviceIMEI, long ReceiverId, string ReceiverToken);
    }
}