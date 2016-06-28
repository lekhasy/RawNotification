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
    /// Interaction logic for MostSupportedCustomer.xaml
    /// </summary>
    public partial class MostSupportedCustomer : UserControl
    {
        ViewModels.MostSupportedCustomerViewModel vm = new ViewModels.MostSupportedCustomerViewModel();
        public MostSupportedCustomer()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Update();
        }
    }
}
