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

namespace QLKH.Pages.UserManages
{
    /// <summary>
    /// Interaction logic for SearhEmployee.xaml
    /// </summary>
    public partial class SearchEmployee : UserControl
    {
        ViewModels.SearchEmployeeViewModel vm = new ViewModels.SearchEmployeeViewModel();
        public Object SelectedItem
        {
            get { return dg_Customer.SelectedItem; }
        }
        public SearchEmployee()
        {
            InitializeComponent();
            this.DataContext = vm;
            
        }

        private void Search_clicked(object sender, RoutedEventArgs e)
        {
            vm.Search(txt_search.Text);
        }

        private void Save_clicked(object sender, RoutedEventArgs e)
        {
            vm.Save();
        }

        private void Expand_clicked(object sender, RoutedEventArgs e)
        {
            new ExpandEmployee((Models.NhanVien)dg_Customer.SelectedItem).Show();
        }
    }
}
