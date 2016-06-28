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
using Notification_Helper_Client;
using System.Runtime.Serialization;
using Windows.Networking.Sockets;
using RawNotification.MobileClient.SharedModels.NetworkPackets.FromClient;
using RawNotification.MobileClient.SharedModels.NetworkPackets.FromServer;

namespace RawNotification.MobileClient
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
            Windows.Networking.PushNotifications.RawNotification notification = taskInstance.TriggerDetails as Windows.Networking.PushNotifications.RawNotification;
            string content = notification.Content;
            try
            {
                // lấy id
                long notificationid = long.Parse(content);
                System.Diagnostics.Debug.WriteLine("Notification ID : " + notificationid.ToString());

                await MobileInterface.MobileInterface.ReceivedNewNotificatonID(notificationid);
            }
            catch (Exception ex)
            {
                // đường truyền lỗi nên không nhận được id
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            // sau khi xử lí xong, ta thông báo với hệ thống là hàm run đã thực hiện xong và hệ thống có thể đóng hàm run lại
            //việc này đồng nghĩa với background task sẽ kết thúc
            defferal.Complete();
        }
    }

    internal sealed class UserRun
    {
        public static void Run(Object nitifobj)
        {
         

    }
}


    internal class MyBitConverter<T>
    {
        private System.Runtime.Serialization.Json.DataContractJsonSerializer se;
        private System.IO.MemoryStream mstream;

        internal MyBitConverter()
        {
            se = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            mstream = new System.IO.MemoryStream();
        }



        public byte[] ObjectToBytes(T Input)
        {
            mstream = new System.IO.MemoryStream();
            se.WriteObject(mstream, Input);
            return mstream.ToArray();
        }

        public T BytesToObject(byte[] data)
        {
            return (T)se.ReadObject(new System.IO.MemoryStream(data));
        }

    }

    [DataContract(Namespace = "NotificationModels", Name = "NotificationDataPacket")]
    public class NotificationDataPacket
    {
        [DataMember]
        internal string Line1 { get; private set; }

        [DataMember]
        internal string Line2 { get; private set; }

        internal NotificationDataPacket(string line1, string line2)
        {
            Line1 = line1;
            Line2 = line2;
        }
    }
}