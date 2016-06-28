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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : UserControl
    {
        ViewModels.AddEmployeeViewModel vm = new ViewModels.AddEmployeeViewModel();
        public AddEmployee()
        {
            InitializeComponent();
            this.DataContext = vm;                
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.AddEmployee(txt_Name.Text, txt_DiaChi.Text, rdb_Nam.IsChecked.Value, datepicker1.SelectedDate, txt_Phone.Text, txt_Phone2.Text, txt_CMND.Text, txt_Email.Text, (Models.ChucVu)cbb_chucvu.SelectedItem, passwordbox.Password);
            txt_Name.Text = txt_DiaChi.Text = txt_Phone.Text = txt_CMND.Text = txt_Email.Text = txt_Phone2.Text = "";
        }
    }
}