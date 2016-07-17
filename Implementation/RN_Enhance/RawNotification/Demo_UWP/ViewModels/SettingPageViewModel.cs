using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_UWP.QLKHDataService;
using RawNotification.Models.ServerBusinessModels.Exceptions;
using Windows.UI.Popups;

namespace Demo_UWP.ViewModels
{
    public class SettingPageViewModel : SuperViewModel
    {
        ObservableCollection<KhachHangInfo> _Customers;
        private string _PreviewData;
        private string _ContentData;

        public ObservableCollection<KhachHangInfo> Customers
        {
            get { return _Customers; }
            set
            {
                _Customers = value;
                OnPropertyChanged("Customers");
            }
        }

        public string PreviewData
        {
            get { return _PreviewData; }
            set
            {
                _PreviewData = value;
                OnPropertyChanged("PreviewData");
            }
        }

        public string ContentData
        {
            get { return _ContentData; }
            set
            {
                _ContentData = value;
                OnPropertyChanged("ContentData");
            }
        }

        public async Task RefreshCustomerListAsync()
        {
            DoLongWork();
            try
            {
                QLKHDataServiceClient sv = new QLKHDataServiceClient();
                var result = await sv.GetAllKhachHangsAsync();
                result.FireMessageExceptionForServiceResult();
                Customers = result.Data;
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);
                await dialog.ShowAsync();
            }
            FinishDoLongWork();
        }

        public async Task AddNotificationAsync()
        {
            DoLongWork();
            try
            {
                RawNotification.SharedLibs.JSONObjectSerializer<string> serializer = new RawNotification.SharedLibs.JSONObjectSerializer<string>();
                byte[] previewDatainBytes = serializer.ObjectToBytes(PreviewData);
                byte[] contentDatainBytes = serializer.ObjectToBytes(ContentData);
                ObservableCollection<string> selectedList = new ObservableCollection<string>(_Customers.Where(c => c.Selected == true).Select(c => c.Id.ToString()));

                RNServerService.RNServerCommunicatorClient sv = new RNServerService.RNServerCommunicatorClient();
                var result = await sv.AddNotificationAsync(contentDatainBytes, previewDatainBytes, selectedList);
                if (result.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                {
                    throw new Exception(result.Message);
                }
                else
                {
                    await new MessageDialog("Adding notification successfull", "Success").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error").ShowAsync();
            }
            FinishDoLongWork();
        }

        public async Task SendNotificationAsync()
        {
            DoLongWork();
            try
            {
                RNServerService.RNServerCommunicatorClient service = new RNServerService.RNServerCommunicatorClient();
                var result = await service.SendAllNotificationAsync();

                if (result.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                {
                    throw new Exception(result.Message);
                }
                else
                {
                    await new MessageDialog("Adding notification successfull", "Success").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error").ShowAsync();
            }
            FinishDoLongWork();
        }
    }
}
