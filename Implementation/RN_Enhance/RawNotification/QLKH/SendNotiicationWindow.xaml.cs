using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QLKH.Models;

namespace QLKH
{
    /// <summary>
    /// Interaction logic for SendNotiicationWindow.xaml
    /// </summary>
    public partial class SendNotiicationWindow : ModernWindow
    {
        ViewModels.SendNotificationViewModel vm;

        public SendNotiicationWindow(IEnumerable<KhachHang> KhachHangs)
        {
            InitializeComponent();
            vm = new ViewModels.SendNotificationViewModel(KhachHangs);
            this.DataContext = vm;
            vm.HandledExceptionOccured += (o, e) => {
                Dispatcher.Invoke((() =>
                {
                    new ErrorDialog(e.Title, e.Description, e.ErrorException.ToString()).ShowDialog();
                }));

            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.AddNotification();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.SendAllNotification();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await vm.CheckSend100times();
        }
    }
}
