using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreBL;
using RawNotification.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Demo_UWP.ViewModels
{
    public class LoadingPageViewModel : SuperViewModel
    {
        string _LoadingStatus = string.Empty;
        public string LoadingStatus
        {
            get { return _LoadingStatus; }
            set { _LoadingStatus = value; OnPropertyChanged("LoadingStatus"); }
        }

        Frame _RootFrame;

        public LoadingPageViewModel(Frame frame)
        {
            _RootFrame = frame;
        }

        public async void StartLoading()
        {
            if (Settings.LoginSuccessed)
            {
                BaseServiceResult AddDeviceResult = await RNAdapter.InitializeAsync(new TimeSpan(0, 15, 0));
                _RootFrame.Navigate(typeof(MainPage));
                return;
            }
            if (string.IsNullOrEmpty(Settings.Pasword))
            {
                _RootFrame.Navigate(typeof(LoginPage));
                return;
            }
            string resultContent = null;

            try
            {
                LoadingStatus = "Contacting to login server";
                var LoginService = new Demo_LoginService.LoginServerClient();
                var Loginresult = await LoginService.LoginAsync(Settings.UserName, Settings.Pasword);
                switch (Loginresult.StatusCode)
                {
                    case ResultStatusCodes.OK: break;
                    case ResultStatusCodes.NotFound:
                        resultContent = "Login info not exists.";
                        break;
                    case ResultStatusCodes.InternalServerError:
                        resultContent = "An internal error occurred on server, try again later.";
                        break;
                    case ResultStatusCodes.ServiceUnavailable:
                        resultContent = Loginresult.Message;
                        break;
                    case ResultStatusCodes.UnknownError:
                        resultContent = "An unknow error occurred on server";
                        break;
                    default:
                        resultContent = "not implemented";
                        break;
                }

                if (Loginresult.StatusCode != ResultStatusCodes.OK)
                {
                    await new MessageDialog(resultContent).ShowAsync();
                    _RootFrame.Navigate(typeof(LoginPage));
                    return;
                }


                // Save token
                var Token = Loginresult.Data;
                var UserNewId = Loginresult.DataT1;
                LoadingStatus = "Contacting to RN server";
                LoadingStatus = "Register new device";
                BaseServiceResult AddDeviceResult = await RNAdapter.InitializeAsync(new TimeSpan(0, 15, 0), UserNewId, Token);
                switch (AddDeviceResult.StatusCode)
                {
                    case ResultStatusCodes.OK:
                        {
                            Settings.LoginSuccessed = true;
                            _RootFrame.Navigate(typeof(MainPage));
                        }

                        break;
                    case ResultStatusCodes.UnAuthorised:
                        resultContent = "An unauthorize error occurred, please login again.";
                        break;
                    case ResultStatusCodes.NotFound:
                        goto case ResultStatusCodes.UnAuthorised;
                    case ResultStatusCodes.InternalServerError:
                        resultContent = "An internal error occurred on server, try again later.";
                        break;
                    case ResultStatusCodes.ServiceUnavailable:
                        resultContent = AddDeviceResult.Message;
                        break;
                    case ResultStatusCodes.UnknownError:
                        resultContent = "An unknow error occurred on server";
                        break;
                    default:
                        resultContent = "not implemented";
                        break;
                }
                if (AddDeviceResult.StatusCode != ResultStatusCodes.OK)
                {
                    await new MessageDialog(resultContent).ShowAsync();
                    _RootFrame.Navigate(typeof(LoginPage));
                    return;
                }
            } catch (Exception ex)
            {
                await new MessageDialog("Login failed"  + Environment.NewLine  + ex.Message).ShowAsync();
                _RootFrame.Navigate(typeof(LoginPage));
                return;
            }
        }
    }
}