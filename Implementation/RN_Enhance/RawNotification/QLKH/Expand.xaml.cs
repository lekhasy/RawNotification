using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows.Media.Imaging;

namespace QLKH
{
    public partial class Expand : ModernWindow
    {
        ViewModels.ExpandViewModel vm;
        public Expand(Models.KhachHang kh)
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("Appico.ico", UriKind.RelativeOrAbsolute));
            vm = new ViewModels.ExpandViewModel(kh);
            this.DataContext = vm;
            ComboboxColumn.ItemsSource = vm.CbbLQHData;
            ComboboxLoaiCuocGoi.ItemsSource = vm.CbbLoaiCuocGoiData;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            vm.SaveNT();
        }

        private void RefreshCalls(object sender, System.Windows.RoutedEventArgs e)
        {
            vm.RefreshCGAsync();
        }

        private void MakeCall(object sender, System.Windows.RoutedEventArgs e)
        {
            MakeCall call = new MakeCall(vm.Customer);
            call.ShowDialog();
            vm.RefreshCGAsync();
        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            vm.RefreshNTAsync();
        }

        private void SaveCalls(object sender, System.Windows.RoutedEventArgs e)
        {
            vm.SaveCG();
        }

        private void AddNTClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            vm.AddNguoiThan(txt_HoTen_NT.Text, txt_Phone_NT.Text, (int)cbb_quanhe.SelectedValue, datetime_NT.SelectedDate);
        }
    }
}
