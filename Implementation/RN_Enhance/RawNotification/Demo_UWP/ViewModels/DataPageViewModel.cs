using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreUserCodeModel;
using RawNotification.SharedLibs;
using RawNotification.Models.ClientCommunicatorModels;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Demo_UWP.ViewModels
{
    public class DataPageViewModel : SuperViewModel
    {
        const int DataPageViewModel_Register = 01;
        const int DataPageViewModel_NotificationRemovedRegister = 02;
        int _SelectedDataIndex;
        
        public int SelectedDataIndex
        {
            get { return _SelectedDataIndex; }
            set
            {
                _SelectedDataIndex = value;
                _CurrentFrame.Navigate(typeof(Pages.ContentDetailPage), Messages[value]);
            }
        }

        public Visibility NoNotificationVisibility
        {
            get { return _NoNotificationVisibility; }
            set
            {
                _NoNotificationVisibility = value;
                OnPropertyChanged("NoNotificationVisibility");
            }
        }

        ObservableCollection<PreviewModel> _Messages;

        public ObservableCollection<PreviewModel> Messages
        {
            get { return _Messages; }
            set
            {
                _Messages = value;
                OnPropertyChanged("Messages");
            }
        }

        Frame _CurrentFrame;
        private Visibility _NoNotificationVisibility = Visibility.Collapsed;

        public static async Task<DataPageViewModel> CreateNewAsync(Frame rootFrame)
        {
            var rtModel = new DataPageViewModel(rootFrame);
            await rtModel.InitializeMessages();
            return rtModel;
        }

        private DataPageViewModel(Frame currentFrame)
        {
            _CurrentFrame = currentFrame;
            RawNotification.DotNetCoreBL.RNAdapter.RegisterNotificationReceivedAsyncEvent(new Action<NotificationInfoForRequesting>(RNAdapter_ReceivedNotification1), DataPageViewModel_Register);
            RawNotification.DotNetCoreBL.RNAdapter.RegisterNotificationListItemRemoved(new Action<IEnumerable<long>>(RNAdapter_RemovedNotification), DataPageViewModel_NotificationRemovedRegister);
        }

        private async void RNAdapter_RemovedNotification(IEnumerable<long> obj)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await InitializeMessages();
            });
        }

        private async Task InitializeMessages()
        {
            JSONObjectSerializer<string> serializer = new JSONObjectSerializer<string>();
            IEnumerable<NotificationInfoForRequesting> previewRawdatas = await RawNotification.DotNetCoreBL.RNAdapter.GetAllPreviewData();

            List<PreviewModel> previewData = new List<PreviewModel>(previewRawdatas.Count());
            
            foreach (var item in previewRawdatas)
            {
                previewData.Add(new PreviewModel { Content = serializer.BytesToObject(item.NotificationPreviewContent), NotificationId = item.NotificationId, NotificationAccessKey = item.NotificationAccessKey });
            }
            Messages = new ObservableCollection<PreviewModel>(previewData);
            NoNotificationVisibility = _Messages.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public async Task labeltapped()
        {
            await InitializeMessages();
        }

        private  async void RNAdapter_ReceivedNotification1(NotificationInfoForRequesting args)
        {
            JSONObjectSerializer<string> serializer = new JSONObjectSerializer<string>();
            var content = serializer.BytesToObject(args.NotificationPreviewContent);

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                Messages.Add(new PreviewModel { Content = content, NotificationAccessKey = args.NotificationAccessKey, NotificationId = args.NotificationId});
                NoNotificationVisibility = _Messages.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
            });
        }
    }
}
