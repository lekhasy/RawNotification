using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreBLCore;
using RawNotification.DotNetCoreLibs;
using RawNotification.SharedLibs;
using RawNotification.Models.ClientCommunicatorModels;
using Windows.ApplicationModel.Background;

namespace RawNotification.DotNetCoreBGTasks
{
    public sealed class RawReceiverBackgroundTask : IBackgroundTask
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

            // TODO: làm những thứ bạn muốn trong background task ở đây
            // Lưu ý: tất cả các phương thức async nào nằm ngoài khu này đều sẽ không thực hiện được
            string content = (taskInstance.TriggerDetails as Windows.Networking.PushNotifications.RawNotification).Content;

            byte[] contentinBytes = RNAdapterCore.NotificationReceivedAsync(content);

            DotNetCoreUserCodes.UserCodes.OnNotificationReceivedInBGTask( new NotificationInfoForRequesting
                { NotificationPreviewContent = contentinBytes } );

            // sau khi xử lí xong, ta thông báo với hệ thống là hàm run đã thực hiện xong và hệ thống có thể đóng hàm run lại
            //việc này đồng nghĩa với background task sẽ kết thúc
            defferal.Complete();
        }
    }
}