using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models.DBModels;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;

namespace RawNotification.DotNetCoreBL
{
    internal static class Utilities
    {
        private const string NOTIFICATION_BACKGROUND_TASK_NAME = "RawNotification_RawReceiverBG";

        public static readonly string NOTIFICATION_BACKGROUND_TASK_ENTRY_POINT = typeof(DotNetCoreBGTasks.RawReceiverBackgroundTask).FullName;

        private const string TIMER_REGISTER_BACKGROUND_TASK_NAME = "RawNotification_TimerRegisterBG";

        public static readonly string TIMER_REGISTER_BACKGROUND_TASK_ENTRY_POINT = typeof(DotNetCoreBGTasks.TimerRegisterBackgroundTask).FullName;

        internal static void RegisterNotificationBackgroundTask()
        {
            _RegisterBackgroundTask(
               NOTIFICATION_BACKGROUND_TASK_NAME,
               NOTIFICATION_BACKGROUND_TASK_ENTRY_POINT,
               new List<IBackgroundTrigger>() { new PushNotificationTrigger() }, null);
        }

        internal static void UnRegisterNotificationBackgroundTask()
        {
            _UnregisterBackgroundTask(NOTIFICATION_BACKGROUND_TASK_NAME);
        }

        internal static void UnRegisterTimerBackgroundTask()
        {
            _UnregisterBackgroundTask(TIMER_REGISTER_BACKGROUND_TASK_NAME);
        }

        internal static void RegisterTimerBackgroundTask(TimeSpan interval)
        {
            _RegisterBackgroundTask(
               TIMER_REGISTER_BACKGROUND_TASK_NAME,
               TIMER_REGISTER_BACKGROUND_TASK_ENTRY_POINT,
               new List<IBackgroundTrigger>() { new TimeTrigger((uint)(interval.TotalMinutes), false) },
               new List<SystemConditionType> { SystemConditionType.InternetAvailable });
        }

        /// <summary>
        /// Đăng ký một Background task dùng để nhận notification
        /// </summary>
        /// /// <param name="name">Tên của task</param>
        /// <param name="EntryPoint">Tên class kế thừa từ interface IBackgroundTask. Ví dụ: "BackgroundTasks.BackgroundTask"</param>
        /// <returns>trả về một đối tượng BackgroundAccessStatus</returns>
        private static void _RegisterBackgroundTask(string name, string EntryPoint, IEnumerable<IBackgroundTrigger> triggers, IEnumerable<SystemConditionType> conditions)
        {
            #region Đăng ký background task
            // lấy tất cả các Background task mà ứng dụng đã đăng ký
            var iter = BackgroundTaskRegistration.AllTasks;

            // kiểm tra lần lượt xem Background task với tên này đã được đăng kí trước đó hay chưa

            foreach (var item in iter)
            {
                if (item.Value.Name == name)
                {
                    // background task với tên này đã được đăng kí trước đó nên không cần đăng ký lại nữa
                    return;
                }
            }

            // nếu chưa được đăng kí thì  đăng kí một Background Task mới
            var builder = new BackgroundTaskBuilder();
            builder.Name = name;
            builder.TaskEntryPoint = EntryPoint;
            if (triggers != null)
            {
                foreach (var item in triggers)
                {
                    builder.SetTrigger(item);
                }
            }
            if (conditions != null)
            {
                foreach (var item in conditions)
                {// thêm tất cả các condition vào
                    builder.AddCondition(new SystemCondition(item));
                }
            }

            builder.Register();
            #endregion
        }

        private static void _UnregisterBackgroundTask(string name)
        {
            var iter = BackgroundTaskRegistration.AllTasks;

            // kiểm tra lần lượt xem Background task với tên này đã được đăng kí trước đó hay chưa

            foreach (var item in iter)
            {
                if (item.Value.Name == name)
                {
                    // hủy đăng ký background task
                    item.Value.Unregister(true);
                }
            }
        }
    }
}