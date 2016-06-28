using System.Windows;
using System.Windows.Controls;

namespace QLKH.Contents
{
    /// <summary>
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : UserControl
    {
        ViewModels.CustomerAddViewModel vm = new ViewModels.CustomerAddViewModel();
        public CustomerAdd()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.AddCustomer(txt_Name.Text, 
                txt_DiaChi.Text, (bool?)rdb_Nam.IsChecked, datepicker1.SelectedDate, 
                txt_Phone.Text, txt_Phone2.Text, txt_CMND.Text, txt_Email.Text);
            CleanInputData();
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.AddNguoiThan(txt_HoTen_NT.Text, txt_Phone_NT.Text, (int)cbb_quanhe.SelectedValue, datetime_NT.SelectedDate);
            txt_HoTen_NT.Text = txt_Phone_NT.Text = string.Empty;
        }

        private void CleanInputData()
        {
            txt_CMND.Text = txt_DiaChi.Text = txt_Email.Text = txt_GTHopDong.textBox.Text = txt_HoTen_NT.Text = txt_Name.Text = txt_Phone.Text = txt_Phone2.Text = txt_Phone_NT.Text = string.Empty;
        }
    }
}