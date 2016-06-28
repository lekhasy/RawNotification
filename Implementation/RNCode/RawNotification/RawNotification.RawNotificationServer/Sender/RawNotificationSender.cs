using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer;
using RawNotification.MobileClient.SharedModels.NetworkPackets;

namespace RawNotification.RawNotificationServer.Sender
{
    internal partial class RawNotificationSender
    {
        #region Attributes, Event, Property

        Windows10.Windows10RawNotificationSender<long> windows10sender;
        private Params.RawNotificationSenderParams _Params;

        private ServerInfo _LastestServerInfo;

        #endregion

        #region Constructor
        internal RawNotificationSender(Params.RawNotificationSenderParams rawNotificationParams, EventHandler<ServerInfo> ServerInfoChanged)
        {
            _LastestServerInfo = new ServerInfo();
            this.ErrorInfoChanged = ServerInfoChanged;
            _UpdateTimer = new System.Threading.Timer(FireEvent, new object(), Timeout.Infinite, Timeout.Infinite);
            _Params = rawNotificationParams;

            //khởi tạo các sender cho các OS khác ở đây
            InitializeWindows10RawNotificationSender();
        }
        #endregion

        Semaphore SubmitsendSemaphore = new Semaphore(1, 1);

        // đối tượng DataContext dùng cho việc gửi thông báo
        Notification_DBDataContext db = new Notification_DBDataContext();

        /// <summary>
        /// Gửi toàn bộ các thông báo chưa được gửi đi trong cơ sở dữ liệu tới các thiết bị
        /// </summary>
        internal async void SubmitSendAsync()
        {

            SubmitsendSemaphore.WaitOne();

            // khi gửi một thông báo mới thì tất cả các lỗi trước đó coi như chưa xảy ra
            _LastestServerInfo.Reset();
            RegisterTimer();
            // xóa tất cả các thông báo mà người dùng đã đọc rồi đi
            RemoveAllReadedNotification(db);

            #region lấy tất cả các thông báo cần gửi tới thiết bị
            var result = from dv in db.Devices
                         join dvnoti in db.DeviceNotifications on dv.DeviceID equals dvnoti.DeviceID
                         join recnoti in db.ReceiverNotifications on dvnoti.ReceiverNotificationID equals recnoti.ReceiverNotificationID
                         join notifi in db.Notifications on recnoti.NotificationID equals notifi.NotificationID
                         select new { dv, dvnoti, recnoti, notifi };
            #endregion

            #region Gửi thông báo tới cho các thiết bị
            // do lượng thông báo gửi đi có thể khá nhiều nên ở đây có thể ta sẽ sử dụng Multithread của Task (thực ra cũng là threadpool)
            // việc tối đa bao nhêu thread gửi cùng lúc thì do threadpool chịu trách nhiệm
            // Ta sử dụng một hàng đợi để thêm các task vào
            Queue<Task> TaskQueue = new Queue<Task>();
            try
            {
                foreach (var noti in result)
                {
                    // với một thông báo cần gửi thì ta sẽ gửi nó bằng một task riêng biệt
                    Task t = Task.Run(() =>
                    {
                        switch (noti.dv.OSID)
                        {
                            case (int)OperatingSystemIDTemplates.Windows10:
                                {
                                    windows10sender.Send(noti.dv, noti.notifi.NotificationID, noti.dvnoti);
                                    break;
                                }
                                // thêm các xử lí cho các hệ điều hành khác ở đây
                        }
                    });

                    // đưa task đó vào hàng đợi
                    TaskQueue.Enqueue(t);
                }
            }
            catch (System.Data.SqlClient.SqlException Sqlex)
            {
                OnDBInteractionErrorOccurred(Sqlex);
                SubmitsendSemaphore.Release();
                return;
            }

            // thread trước khi submitchange DB thì cần phải dừng lại ở đây để chờ tất cả được gửi xong
            while (TaskQueue.Count > 0)
            {
                await TaskQueue.Dequeue();
            }

            // xuống đến đây thì có nghĩa là tất cả các task đã thực hiện xong, tiến hành submitchange
            try
            {
                db.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.SqlClient.SqlException Sqlex)
            {
                OnDBInteractionErrorOccurred(Sqlex);
            }
            SubmitsendSemaphore.Release();
            #endregion

        }



        private void RemoveAllReadedNotification(Notification_DBDataContext db)
        {
            try
            {
                db.ReceiverNotifications.DeleteAllOnSubmit(db.ReceiverNotifications.Where(r => r.Readed));
                db.SubmitChanges();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                OnDBInteractionErrorOccurred(ex);
            }
        }

        private System.Threading.Timer _UpdateTimer;

        public EventHandler<ServerInfo> ErrorInfoChanged;

        // tất cả thông tin về lỗi khi được gán sẽ không được gán vào property bởi vì mỗi lần gán vào property, nó sẽ phải chạy cạp nhật giao diện, làm giảm hiệu suất hệ thống
        // giải pháp là gán vào biến private, sau đó sẽ bật timer lên cho 1 giây sau nó mới bắn ra sự kiện
        // 1 giây là con số chấp nhận được

        private void FireEvent(object state)
        {
            Monitor.Enter(_LastestServerInfo);
            ErrorInfoChanged(this, GetServerInfo());
            System.Diagnostics.Debug.WriteLine("Đã bắn sự kiện InfoChanged");
            Monitor.Exit(_LastestServerInfo);
        }

        internal ServerInfo GetServerInfo()
        {
            Monitor.Enter(_LastestServerInfo);
            ServerInfo clone = _LastestServerInfo.Clone();
            Monitor.Exit(_LastestServerInfo);
            return clone;
        }

        private void OnInternetOrFireWallErrorOccurred(object sender, Exception ex)
        {
            _LastestServerInfo.ChangeErrorInfo(ex, SenderServerErrorType.InternetError);
            RegisterTimer();
        }


        private void OnDBInteractionErrorOccurred(System.Data.SqlClient.SqlException ex)
        {
            _LastestServerInfo.ChangeErrorInfo(ex, SenderServerErrorType.DBInteractionError);
            RegisterTimer();
        }

        /// <summary>
        /// Gọi hàm này bất cứ lúc nào muốn cập nhật lại trạng thái của server cho client
        /// Nó sẽ tạm ngưng 1 giây sau đó mới gửi lại cho client biết về trạng thái của nó
        /// </summary>
        private void RegisterTimer()
        {
            // callback sẽ được gọi sau 1000 mili giây( 1 giây ) và không được lặp lại
            _UpdateTimer.Change(1000, System.Threading.Timeout.Infinite);
        }

    }

}
