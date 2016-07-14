using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreUserCodeModel;
using Windows.UI.Popups;

namespace Demo_UWP.ViewModels
{
    public class ContentDetailPageViewModel : SuperViewModel
    {
        string _Content = string.Empty;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged("Content"); }
        }
        public async Task InitializeContent(PreviewModel model)
        {
            DoLongWork();
            try
            {
                byte[] data = await RawNotification.DotNetCoreBL.RNAdapter.GetNotificationContentAsync(model.NotificationId, model.NotificationAccessKey);
                RawNotification.SharedLibs.JSONObjectSerializer<string> serializer = new RawNotification.SharedLibs.JSONObjectSerializer<string>();
                Content = serializer.BytesToObject(data);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            finally
            {
                FinishDoLongWork();
            }
        }
    }
}
