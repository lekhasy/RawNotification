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

namespace QLKH.Pages
{
    /// <summary>
    /// Interaction logic for AdminSettings.xaml
    /// </summary>
    public partial class AdminSettings : UserControl
    {
        ViewModels.AdminSettingsViewModel vm;
        public AdminSettings()
        {
            InitializeComponent();
            vm = new ViewModels.AdminSettingsViewModel();
            this.DataContext = vm;
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            if (TimePicker.Value !=null)
            {
                if (!switch_Repeat.IsChecked||(switch_Repeat.IsChecked && cbx_cn.IsChecked.Value||cbx_t2.IsChecked.Value||cbx_t3.IsChecked.Value||cbx_t4.IsChecked.Value||cbx_t5.IsChecked.Value||cbx_t6.IsChecked.Value||cbx_t7.IsChecked.Value))
                {
                    vm.AddBackupSchedule(TimePicker.Value.Value, switch_Repeat.IsChecked, cbx_cn.IsChecked.Value, cbx_t2.IsChecked.Value, cbx_t3.IsChecked.Value, cbx_t4.IsChecked.Value, cbx_t5.IsChecked.Value, cbx_t6.IsChecked.Value, cbx_t7.IsChecked.Value);
                }
                else
                {
                    MessageBox.Show("Hãy chọn tối thiểu một ngày");
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn một thời điểm");
            }
        }

        private void RemoveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedItem!=null)
            {
                vm.RemoveBackupSchedule(listbox.SelectedItem as Models.BackupSchedule);
            }
            else
            {
                MessageBox.Show("Hãy lựa chọn một mục để xóa");
            }
        }
    }
}
