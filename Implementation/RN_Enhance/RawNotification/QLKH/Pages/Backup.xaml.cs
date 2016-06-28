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
using System.IO;

namespace QLKH.Pages
{
    /// <summary>
    /// Interaction logic for Backup.xaml
    /// </summary>
    public partial class Backup : UserControl
    {
        ViewModels.BackupViewModel vm = new ViewModels.BackupViewModel();
        public Backup()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.RefreshList();
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedItem==null)
            {
                MessageBox.Show("Không có file nào được chọn");
                return;
            }
            if (MessageBox.Show("bạn thực sự muốn xóa file backup này?","Xác nhận xóa",MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if(vm.DeleteBackUp(listbox.SelectedItem.ToString()))
            {
                MessageBox.Show("Đã xóa backup");
            }
            else
            {
                MessageBox.Show("File không tồn tại");
            }
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.BackUp();
        }
    }
}
