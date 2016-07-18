using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QLKH.Models;

namespace QLKH.ViewModels
{
    public class SendNotificationViewModel: SuperViewModel
    {
        private IEnumerable<KhachHang> _Receivers;

        private string _NotificationContent;
        public string NotificationContent
        {
            get
            {
                return _NotificationContent;
            }
            set
            {
                _NotificationContent = value;
                OnPropertyChanged("NotificationContent");
            }
        }
        string _NotificationPreviewContent = "Preview Title";

        public string NotificationPreviewContent
        {
            get
            {
                return _NotificationPreviewContent;
            }
            set
            {
                _NotificationPreviewContent = value;
                OnPropertyChanged("NotificationPreviewContent");
            }
        }


        public int NumberOfReceiver
        {
            get
            {
                return _Receivers.Count();
            }
        }

        public SendNotificationViewModel(IEnumerable<KhachHang> receivers)
        {
            _Receivers = receivers;
            OnPropertyChanged("NumberOfReceiver");
        }

        internal void AddNotification()
        {
            try
            {
                using (var service = AppGlobal.getRNServerService())
                {
                    RawNotification.SharedLibs.JSONObjectSerializer<string> serializer = new RawNotification.SharedLibs.JSONObjectSerializer<string>();

                    var result = service.AddNotification
                        (
                        serializer.ObjectToBytes(NotificationContent), 
                        serializer.ObjectToBytes(_NotificationPreviewContent), 
                        _Receivers.Select(r=>r.RNReceiverOldID).ToArray()
                        );
                    
                    if (result.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                    {
                        throw new Exception(result.Message);
                    }
                    else
                    {
                        MessageBox.Show("Adding notification successfull", "Success");
                    }
                }
            } catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(new HandledError(ex, "Error occurred while sending notification", "None"));
            }
        }

        internal void SendAllNotification()
        {
            try
            {
                using (var service = AppGlobal.getRNServerService())
                {
                    var result = service.SendAllNotification();

                    if (result.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                    {
                        throw new Exception(result.Message);
                    }
                    else
                    {
                        MessageBox.Show("Sending notification successfull", "Success");
                    }
                }
            } catch (Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(new HandledError(ex, "Error occurred while sending notification", "None"));
            }
            
        }

        public async Task CheckSend100times()
        {
            Queue<Task> taskQueue = new Queue<Task>();
            for (int i = 0; i < 100; i++)
            {
                Task t = new Task(() =>
                {
                    try
                    {
                        using (var service = AppGlobal.getRNServerService())
                        {
                            var result = service.SendAllNotification();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                });
                taskQueue.Enqueue(t);
                t.Start();
            }
            while (taskQueue.Count>0)
            {
                await taskQueue.Dequeue();
            }
        }

    }
}
