using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.Networking.PushNotifications;
using System.Runtime.Serialization;
using Windows.Networking.Sockets;
using RawNotificationBackgroundTaskForClient.Models;

namespace RawNotificationBackgroundTaskForClient
{

    public sealed class BackgroundTask : IBackgroundTask
    {
        /// <summary>
        /// Phương thức này sẽ chạy khi Trigger được kích hoạt, không cần gọi tới nó
        /// </summary>
        /// <param name="taskInstance">hệ thống sẽ tự động tạo instance này khi chạy</param>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            //nếu sử dụng một phương thức async nào trong hàm này, hàm run sẽ kết thúc trước khi phương thức đó thực hiện xong.
            // sử dụng defferal để thông báo với hệ thống rằng chưa được phép kết thúc phương thức run
            // nếu bạn không sử dụng phương thức async nào, bạn có thể bỏ defferal đi.
            BackgroundTaskDeferral defferal = taskInstance.GetDeferral();

            System.Diagnostics.Debug.WriteLine("Đã nhận được toast");

            // TODO: làm những thứ bạn muốn trong background task ở đây
            // Lưu ý: tất cả các phương thức async nào nằm ngoài khu này đều sẽ không thực hiện được
            string content = (taskInstance.TriggerDetails as RawNotification).Content;
            try
            {
                // lấy id
                long notificationid = long.Parse(content);
                System.Diagnostics.Debug.WriteLine("Notification ID : " + notificationid.ToString());

                #region Bắt đầu liên lạc trực tiếp với server để lấy nội dung thông báo

                CommunicatePacket packet = new CommunicatePacket(CommunicateType.GetNotificationContent, new GetNotificationContentCommunicateData(notificationid));
                try
                {
                    UWPTCPClient.UWPTCPClient client = new UWPTCPClient.UWPTCPClient("115.74.126.7", "22112", ((data) =>
                    {
                        MyBitConverter<SendObject> contentconverter = new MyBitConverter<SendObject>();
                        SendObject notifyContent = contentconverter.BytesToObject(data);
                        UserCode.Run(notifyContent);

                        // sau khi xử lí xong, ta thông báo với hệ thống là hàm run đã thực hiện xong và hệ thống có thể đóng hàm run lại
                        //việc này đồng nghĩa với background task sẽ kết thúc
                        defferal.Complete();
                    }));
                    await client.ConnectAsync();
                    await client.SendAsync(new Models.MyBitConverter<CommunicatePacket>().ObjectToBytes(new CommunicatePacket(CommunicateType.GetNotificationContent, new GetNotificationContentCommunicateData(notificationid))));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                #endregion

            }
            catch (Exception ex)
            {
                // đường truyền lỗi nên không nhận được id
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }
    }
}