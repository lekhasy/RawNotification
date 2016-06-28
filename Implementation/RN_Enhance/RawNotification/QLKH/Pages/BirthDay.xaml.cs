using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BirthDay.xaml
    /// </summary>
    public partial class BirthDay : UserControl
    {
        ViewModels.BirthdayViewModel vm = new ViewModels.BirthdayViewModel();
        public BirthDay()
        {
            InitializeComponent();

            this.DataContext = vm;
        }
        
        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Refresh();
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            vm.Save();
        }
    }
}
