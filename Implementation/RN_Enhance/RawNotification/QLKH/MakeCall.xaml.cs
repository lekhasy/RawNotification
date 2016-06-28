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

namespace QLKH
{
    /// <summary>
    /// Interaction logic for MakeCall.xaml
    /// </summary>
    public partial class MakeCall : ModernDialog
    {
        ViewModels.MakeCallViewModel vm;
        public MakeCall(Models.KhachHang kh)
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("Appico.ico", UriKind.RelativeOrAbsolute));
            vm = new ViewModels.MakeCallViewModel(kh);
            this.DataContext = vm;

            
            // define the dialog buttons
            this.Buttons = new Button[] { this.CancelButton };
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.AddCall((int)CbbLoaiCuocGoi.SelectedValue,(int)SliderStatus.Value,txt.Text);
            this.Close();
        }
    }
}
