using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Demo_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ViewModels.MainPageViewModel vm;
        

        public MainPage()
        {
            this.InitializeComponent();
            vm = new ViewModels.MainPageViewModel(Window.Current.Content as Frame, mainPageFrame);
            this.DataContext = vm;
            mainPageFrame.Navigate(typeof(Pages.DataPage));
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SplitviewMenu.IsPaneOpen = !SplitviewMenu.IsPaneOpen;
        }

        private void NavigationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ismanipulating) return;
            SplitviewMenu.IsPaneOpen = false;
            try
            {
                ismanipulating = true;
                (list2.SelectedItem as ListBoxItem).IsSelected = false;
            } catch { } finally
            {
                ismanipulating = false;
            }
            if (HomeListBoxItem.IsSelected)
            {
                vm.NavigateToHome();
            }
        }

        bool ismanipulating = false;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ismanipulating) return;
            try
            {
                ismanipulating = true;
                (NavigationList.SelectedItem as ListBoxItem).IsSelected = false;
            } catch { } finally
            {
                ismanipulating = false;
            }
            SplitviewMenu.IsPaneOpen = false;
            if (LogoutListBoxItem.IsSelected)
            {
                vm.LogoutAsync();
            }
            else if (SettingListBoxItem.IsSelected)
            {
                vm.NavigateToSettingPage();
            }
        }

        private async void BtnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            await vm.RemoveAllDataAsync();
        }
    }
}
