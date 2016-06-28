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

namespace QLKH.Pages.UserManages
{
    /// <summary>
    /// Interaction logic for ExpandEmployee.xaml
    /// </summary>
    public partial class ExpandEmployee : ModernWindow
    {
        ViewModels.ExpandEmployeeViewModel vm;
        public ExpandEmployee(Models.NhanVien nv)
        {
            InitializeComponent();
            vm = new ViewModels.ExpandEmployeeViewModel(nv);
            this.DataContext = vm;
        }

        private void AddChucVuClick(object sender, RoutedEventArgs e)
        {
            vm.AddChucVu((Models.ChucVu)cbb_chucvu.SelectedItem);
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            vm.RefreshAsync();
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            vm.Save();
        }
    }
}
