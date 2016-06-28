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

namespace QLKH.Pages.Statisticals
{
    /// <summary>
    /// Interaction logic for MostGiftTakerCustomer.xaml
    /// </summary>
    public partial class MostGiftTakerCustomer : UserControl
    {
        ViewModels.MostGiftTakerCustomerViewModel vm = new ViewModels.MostGiftTakerCustomerViewModel();
        public MostGiftTakerCustomer()
        {
            InitializeComponent();
            this.DataContext = vm;
            dg_Customer.ItemsSource = vm.SearchResult;
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Update();
        }
    }
}
