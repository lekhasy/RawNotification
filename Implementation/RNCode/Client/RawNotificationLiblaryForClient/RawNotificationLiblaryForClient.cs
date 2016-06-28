using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;


namespace RawNotification.MobileClient.LiblaryForClient
{
    public sealed class RawNotificationClient
    {
        private static string BackgroundTaskName = "RawReceiver";

        public RawNotificationClient(string serverName, int serverPort)
        {
            ChangeServerPort(serverPort);
            ChangeServerName(serverName);
        }

        public void RegisterUserNameWithRNS(string newUserName)
        {
            LocalSettings.LocalSettingsManager.UserName = newUserName;
            MobileInterface.MobileInterface.SendURI();
        }
        
        public void ChangeServerPort(int newServerPort)
        {
            LocalSettings.LocalSettingsManager.RNSClientPort = newServerPort.ToString();
        }
        public void ChangeServerName(string newServerName)
        {
            LocalSettings.LocalSettingsManager.ServerIP = newServerName;
        }

        public async Task<BackgroundAccessStatus> RegisterBackgroundTaskAsync()
        {
            return await _RegisterBackgroundTaskAsync(BackgroundTaskName, typeof(RawNotification.MobileClient.BackgroundTask).FullName, null);
        }



        /// <summary>
        /// hàm này dùng để gửi URI tới server, chỉ dùng để đối phó, dùng xong thì xóa
        /// </summary>
        /// <returns></returns>

        private async void SendURI()
        {
            //Windows.Networking.PushNotifications.PushNotificationChannel chanel = await Notification_Helper_Client.Normal_Task.getChanel();
            //System.Diagnostics.Debug.WriteLine(chanel.Uri);
            //System.Diagnostics.Debug.WriteLine(Notification_Helper_Client.Device.ID);
            //if (chanel.Uri == null) return;
        }

        /// <summary>
        /// Đăng ký một Background task dùng để nhận notification
        /// </summary>
        /// /// <param name="name">Tên của task</param>
        /// <param name="EntryPoint">Tên class kế thừa từ interface IBackgroundTask. Ví dụ: "BackgroundTasks.BackgroundTask"</param>
        /// <returns>trả về một đối tượng BackgroundAccessStatus</returns>
        /// "BackgroundTasks.BackgroundTask"
        private async Task<BackgroundAccessStatus> _RegisterBackgroundTaskAsync(string name, string EntryPoint, IEnumerable<SystemConditionType> conditions)
        {
            #region Xin quyền chạy nền
            // yêu cầu quyền chạy Background từ user
            // câu lệnh này sẽ đưa cho user một thông báo yêu cầu cấp quyền một lần duy nhất
            // nếu user chọn, lần sau nó sẽ không hiện lên nữa.
            BackgroundExecutionManager.RemoveAccess();
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.Denied || status == BackgroundAccessStatus.Unspecified)
            {
                return status;
            }
            #endregion

            #region Đăng ký background task
            // lấy tất cả các Background task mà ứng dụng đã đăng ký
            var iter = BackgroundTaskRegistration.AllTasks;

            // kiểm tra lần lượt xem Background task với tên này đã được đăng kí trước đó hay chưa
            
            foreach (var item in iter)
            {
                if (item.Value.Name == name)
                {
                    // background task với tên này đã được đăng kí trước đó nên không cần đăng ký lại nữa
                    item.Value.Unregister(true);
                }
            }

            // nếu chưa được đăng kí thì  đăng kí một Background Task mới
            var builder = new BackgroundTaskBuilder();
            builder.Name = name;
            builder.TaskEntryPoint = EntryPoint;
            builder.SetTrigger(new PushNotificationTrigger());
            if (conditions != null)
            {
                foreach (var item in conditions)
                {// thêm tất cả các condition vào
                    builder.AddCondition(new SystemCondition(item));
                }
            }


            BackgroundTaskRegistration task = builder.Register();

            System.Diagnostics.Debug.WriteLine("background task registered kaka");

            System.Diagnostics.Debug.WriteLine("task : " + task.TaskId.ToString());
            #endregion

            return status;
        }

    }
}
