using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Demo_UWP.ViewModels
{
    public class LoginViewModel : SuperViewModel
    {
        Frame _Frame;

        string _LoginStatus = string.Empty;

        public string LoginStatus
        {
            get
            {
                return _LoginStatus;
            }
            set
            {
                _LoginStatus = value;
                OnPropertyChanged("LoginStatus");
            }
        }

        Visibility _IsLogging = Visibility.Collapsed;

        public Visibility IsLogging
        {
            get
            {
                return _IsLogging;
            }
            set
            {
                _IsLogging = value;
                OnPropertyChanged("IsLogging");
            }
        }


        public LoginViewModel(Frame frame)
        {
            _Frame = frame;
            Settings.Pasword = "";
        }

        internal void LoginAsync(string UserId, string password)
        {
            Settings.UserName = UserId;
            Settings.Pasword = password;
            _Frame.Navigate(typeof(Pages.LoadingPage));
        }
    }
}