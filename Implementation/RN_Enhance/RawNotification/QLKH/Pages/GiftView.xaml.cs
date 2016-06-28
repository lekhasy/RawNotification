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
    /// Interaction logic for GiftView.xaml
    /// </summary>
    public partial class GiftView : UserControl
    {
        ViewModels.GiftViewViewModel vm = new ViewModels.GiftViewViewModel();
        public GiftView()
        {
            InitializeComponent();
            this.DataContext = vm;
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
