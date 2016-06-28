using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Collections.ObjectModel;
using System.Data;

namespace QLKH.Contents
{
    /// <summary>
    /// Interaction logic for CustomerSearch.xaml
    /// </summary>

    public partial class CustomerSearch : UserControl
    {
        ViewModels.SearchCustomerViewModel vm = new ViewModels.SearchCustomerViewModel();

      

        public CustomerSearch()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.SearchCustomer(txt_search.Text);
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            vm.SaveChange();
        }


        private void ModernButton_Click_3(object sender, RoutedEventArgs e)
        {
            Models.KhachHang kh = dg_Customer.SelectedItem as Models.KhachHang;
            Expand ex = new Expand(kh);
            ex.Show();
        }

        private void AddGiftClicked(object sender, RoutedEventArgs e)
        {
            new Pages.GiftsManage.GiftManage_AddGift(dg_Customer.SelectedItems).ShowDialog();
        }

        private void ModernButton_Click_2(object sender, RoutedEventArgs e)
        {
            new SendNotiicationWindow(dg_Customer.SelectedItems.Cast<Models.KhachHang>()).ShowDialog();
        }
    }
}