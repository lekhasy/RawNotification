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
    /// Interaction logic for GiftManage_AddGift.xaml
    /// </summary>
    public partial class GiftManage_AddGift
    {
        ViewModels.AddGiftViewModel vm;
        public GiftManage_AddGift(System.Collections.IList SelectedNt)
        {
            InitializeComponent();
            vm = new ViewModels.AddGiftViewModel(SelectedNt);
            this.DataContext = vm;
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            vm.AddGift(txtGiatri.Value,SearchEmployeeUC.SelectedItem as Models.NhanVien);
        }
    }
}
