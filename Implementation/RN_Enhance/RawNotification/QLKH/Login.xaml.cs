using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QLKH
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : ModernWindow
    {
        ViewModels.LoginViewModel vm = new ViewModels.LoginViewModel();
        public Login()
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("Appico.ico", UriKind.RelativeOrAbsolute));
            vm.HandledExceptionOccured += (o, e) =>
            {
                Dispatcher.Invoke((() =>
               {
                   new ErrorDialog(e.Title, e.Description, e.ErrorException.ToString()).ShowDialog();
               }));

            };
            vm.LoginResultEvent += (s, e) =>
            {
                if (e)
                {
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    new ModernDialog { Title = "Sai thông tin", Content = "ID hoặc mật khẩu không đúng" }.ShowDialog();
                }
            };

            this.Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                App.AppInitialize();
            }
            catch (System.Security.SecurityException sec_ex)
            {
                ViewModels.HandledError err = ViewModels.ErrorTemplates.GetFileSecurityError(sec_ex);
                new ErrorDialog(err.Title, err.Description, err.ErrorException.ToString()).ShowDialog();
            }
            catch (PathTooLongException path_ex)
            {
                ViewModels.HandledError err = ViewModels.ErrorTemplates.GetPathTooLongException(path_ex);
                new ErrorDialog(err.Title, err.Description, err.ErrorException.ToString()).ShowDialog();
            }
            catch (System.Data.SqlClient.SqlException sql_ex)
            {
                ViewModels.HandledError err = ViewModels.ErrorTemplates.GetDBInteractionError(sql_ex);
                new ErrorDialog(err.Title, err.Description,err.ErrorException.ToString()).ShowDialog();
            }
            catch (Exception ex)
            {
                new ErrorDialog("Lỗi tạo file", @"Không thể khởi tạo Log file.
ứng dụng sẽ hoạt động mà không ghi log file", ex.ToString()).ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.Login(txt_ID.Text, password.Password);
        }
    }
}