using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Demo_UWP.ViewModels
{
    public class MainPageViewModel :SuperViewModel
    {
        Frame _RootFrame;
        Frame _MainWindowFrame;

        string _Header = "";

        public string Header
        {
            get { return _Header;}
            set { _Header = value; OnPropertyChanged("Header"); }
        }

        public MainPageViewModel(Frame rootframe, Frame mainwindowFrame)
        {
            _RootFrame = rootframe;
            _MainWindowFrame = mainwindowFrame;
        }

        public async void LogoutAsync()
        {
            MessageDialog dialog = new MessageDialog("Are you sure?");
            dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new UICommand("No") { Id = 1 });

            IUICommand c = await dialog.ShowAsync();
            int id = (int)c.Id;
            switch (id)
            {
                case 0: {
                        dialog.Content = "Do you want to keep notification data?";
                        c = await dialog.ShowAsync();
                        await RawNotification.DotNetCoreBL.RNAdapter.Logout((int)c.Id == 0? true : false);
                        Settings.LoginSuccessed = false;
                        _RootFrame.Navigate(typeof(LoginPage));
                    } break;
            }
        }

        public void NavigateToHome()
        {
            _MainWindowFrame.Navigate(typeof(Pages.DataPage));
            Header = "Home";
        }

        public void NavigateToSettingPage()
        {
            _MainWindowFrame.Navigate(typeof(Pages.SettingPage));
            Header = "Setting";
        }
    }
}
