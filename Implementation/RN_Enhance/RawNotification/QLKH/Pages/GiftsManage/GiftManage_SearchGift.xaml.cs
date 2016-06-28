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

namespace QLKH.Pages.GiftsManage
{
    /// <summary>
    /// Interaction logic for GiftManage_SearhGift.xaml
    /// </summary>
    public partial class GiftManage_SearchGift : UserControl
    {
        ViewModels.SearchGiftViewModel vm = new ViewModels.SearchGiftViewModel();
        
        public GiftManage_SearchGift()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void SearchClicked(object sender, RoutedEventArgs e)
        {
            if (datetime_from.SelectedDate == null || datetime_to.SelectedDate == null)
            {
                new FirstFloor.ModernUI.Windows.Controls.ModernDialog { Title = "Lỗi", Content = "Vui lòng chọn ngày để tìm kiếm" }.ShowDialog();
                return;
                
            }
            vm.Search(cbx_NotSend.IsChecked.Value,datetime_from.SelectedDate.Value, datetime_to.SelectedDate.Value);
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            vm.Save();
        }
        
    }
}
