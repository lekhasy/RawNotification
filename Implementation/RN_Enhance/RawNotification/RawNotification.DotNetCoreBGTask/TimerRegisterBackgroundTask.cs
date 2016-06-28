using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace RawNotification.DotNetCoreBGTasks
{
    public sealed class TimerRegisterBackgroundTask : IBackgroundTask
    {
        // when this trigger is hit, that means we have internet connection
        // but anny way, connection may be corrupted
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            //nếu sử dụng một phương thức async nào trong hàm này, hàm run sẽ kết thúc trước khi phương thức đó thực hiện xong.
            // sử dụng defferal để thông báo với hệ thống rằng chưa được phép kết thúc phương thức run
            // nếu bạn không sử dụng phương thức async nào, bạn có thể bỏ defferal đi.
            BackgroundTaskDeferral defferal = taskInstance.GetDeferral();
            try
            {
                //await DotNetCoreBL.RNAdapter.SendDeviceInfoToServerAsync();
            } finally
            {
                defferal.Complete();
            }
        }
    }
}