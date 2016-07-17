using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Demo_UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        SettingPageViewModel vm = new SettingPageViewModel();
        public SettingPage()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await vm.RefreshCustomerListAsync();
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            await vm.AddNotificationAsync();
        }

        private async void ButtonSendAll_Click(object sender, RoutedEventArgs e)
        {
            await vm.SendNotificationAsync();
        }
    }
}
