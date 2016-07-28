using System.ServiceModel;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.ClientCommunicator
{
    [ServiceContract]
    public interface IRNClientCommunicator
    {
        [OperationContract]
        BaseServiceResult<long, string> AddDevice(long ReceiverId, Device deviceInfo, string LoginToken);
        [OperationContract]
        BaseServiceResult<byte[]> GetNotificationContent(long NotificationId, string NotificationAccessKey, string DeviceToken, string DeviceIMEI);
        [OperationContract]
        BaseServiceResult<bool> CheckIfNotificationRead(long ReceiverNotificationID);
        [OperationContract]
        BaseServiceResult RemoveDevice(string DeviceIMEI, string DeviceToken);
    }
}