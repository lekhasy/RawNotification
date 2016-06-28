using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Demo_UWP.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataPage : Page
    {
        ViewModels.DataPageViewModel vm;
        bool isLoaded = false;
        public DataPage()
        {
            this.InitializeComponent();
        }

        private void ListBoxData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoaded || ListBoxData.SelectedIndex == -1) return;
            vm.SelectedDataIndex = ListBoxData.SelectedIndex;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm = await DataPageViewModel.CreateNewAsync(this.Frame);
            this.DataContext = vm;
            isLoaded = true;
            ListBoxData.SelectedIndex = -1;
        }
    }
}