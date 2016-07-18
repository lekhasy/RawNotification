using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using RawNotification.BusinessLogic.RawNotificationSenders.RNSInterfaces;
using RawNotification.DataAccess;
using RawNotification.ServiceLib;
using RawNotification.Models.ServerBusinessModels;
using RawNotification.Models.ServerBusinessModels.Parameters;
using RawNotification.SharedLibs;
using RawNotification.Models.ClientCommunicatorModels;
using RawNotification.Models.DBModels;

namespace RawNotification.BusinessLogic
{
    public class RawNotificationSender : BaseServiceBL, IRawNotificationSender
    {
        IWNSRNSender _WNSSender;
        public RawNotificationSender(WNSParameter wnsparameter) : base()
        {
            InitializeForSenders(wnsparameter);
        }

        public RawNotificationSender(WNSParameter parameter, IRawNotificationDB db) : base(db)
        {
            InitializeForSenders(parameter);
        }

        private void InitializeForSenders(WNSParameter parameter)
        {
            _WNSSender = new RawNotificationSenders.RNSImplements.WNSRNSender(parameter);
        }

        private static object SendAllLock = new object();

        public void SendAllNotificationAsync()
        {
            Monitor.Enter(SendAllLock);
            var asyncresult = Task.Run<int>(async () =>
            {

                _Logger.Info("Begin send notifications session");

                try
                {
                    // check for inernet connection
                    if (!new InterNetworkConnectionChecker().IsInternetAvailable())
                    {
                        _Logger.Info("Internet not available");
                        return 1;
                    }

                    // delete all read already notification
                    DB.NotificationDA.RemoveAllReadNotification();

                    // lấy tất cả các thông báo cần gửi tới thiết bị
                    var result = DB.NotificationDA.GetAllPendingDeviceNotificationInfo();

                    #region Gửi thông báo tới cho các thiết bị

                    // Ta sử dụng một hàng đợi để thêm các task vào
                    Queue<Task> TaskQueue = new Queue<Task>();
                    bool IsNetworkErrorOccurred = false;
                    bool IsWNSOtherErorOccurred = false;

                    List<PendingNotificationInfo> SuccessNotifiList = new List<PendingNotificationInfo>(100);
                    List<PendingNotificationInfo> BadURINotifiList = new List<PendingNotificationInfo>(100);
                    Dictionary<long, byte[]> RequestCache = new Dictionary<long, byte[]>(100);

                    JSONObjectSerializer<NotificationInfoForRequesting> serializer = new JSONObjectSerializer<NotificationInfoForRequesting>();

                    foreach (var noti in result)
                    {
                        // với một thông báo cần gửi thì ta sẽ gửi nó bằng một task riêng biệt
                        Task task = Task.Run(() =>
                        {
                            byte[] sendData = null;
                            lock (RequestCache)
                            {
                                try
                                {
                                    sendData = RequestCache[noti.NotificationId];
                                }
                                catch
                                {
                                    sendData = serializer.ObjectToBytes(new NotificationInfoForRequesting { NotificationId = noti.NotificationId, NotificationAccessKey = noti.NotificationAccessKey, NotificationPreviewContent = noti.NotificationPreviewContent, ReiceiverNotificationID = noti.ReceiverNotificationId });
                                    RequestCache.Add(noti.NotificationId, sendData);
                                }
                            }

                            switch (noti.DeviceOSId)
                            {
                                case (int)EnumOperatingSystem.Windows:
                                    {
                                        if (IsNetworkErrorOccurred && IsWNSOtherErorOccurred) return;
                                        var sendResult = _WNSSender.SendNotificationData(noti.DeviceURI, sendData);
                                        switch (sendResult)
                                        {
                                            case EnumWNSSendResult.Success:
                                                {
                                                    SuccessNotifiList.Add(noti);
                                                }
                                                break;
                                            case EnumWNSSendResult.BadURI:
                                                {
                                                    BadURINotifiList.Add(noti);
                                                }
                                                break;
                                            case EnumWNSSendResult.NetWorkError:
                                                {
                                                    // if an Internet Error Occurred, we can cancel all task in foreach loop
                                                    // WNSSender will log this
                                                    IsNetworkErrorOccurred = true;
                                                    break;
                                                }
                                            case EnumWNSSendResult.OtherErrors:
                                                {
                                                    // WNSSender will log this
                                                    IsWNSOtherErorOccurred = true;
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                    //TODO: thêm các xử lí cho các hệ điều hành khác ở đây
                            }
                        });

                        // add task to queue
                        TaskQueue.Enqueue(task);
                    }

                    // Wait all tasks complete
                    while (TaskQueue.Count > 0)
                    {
                        await TaskQueue.Dequeue();
                    }

                    foreach (var item in BadURINotifiList)
                    {
                        DB.DeviceDA.DeleteDevice(item.DeviceId, item.ReceiverId);
                    }

                    foreach (var item in SuccessNotifiList)
                    {
                        DB.DeviceNotificationDA.DeleteDeviceNotification(item.DeviceNotificationId);
                    }

                    DB.ReceiverDA.DeleteAllReceiverHaveNoDevice();

                    DB.commit();
                    _Logger.Info("Send Done" + Environment.NewLine +
                        "Total notifications: " + result.Count().ToString() + Environment.NewLine +
                        "Bad URI: " + BadURINotifiList.Count.ToString() + Environment.NewLine +
                        "Success: " + SuccessNotifiList.Count.ToString());
                    return 1;
                }
                catch (Exception ex)
                {
                    _Logger.Error("An error occurred while sending notification", ex);
                    throw;
                }
                finally
                {
                    _Logger.Info("End send notifications session");
                }
            }).Result;
            Monitor.Exit(SendAllLock);
            #endregion
        }
    }
}