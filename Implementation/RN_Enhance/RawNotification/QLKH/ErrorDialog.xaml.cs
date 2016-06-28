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
using System.ComponentModel;
using System.Diagnostics;

namespace QLKH
{
    /// <summary>
    /// Interaction logic for ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : ModernDialog, INotifyPropertyChanged
    {
        private string _ErrorTitle;

        private string _ErrorDescription;
        private string _ErrorDetailException;

        public string ErrorTitle
        {
            get { return _ErrorTitle; }
            set
            {
                _ErrorTitle = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorTitle"));
                }
            }
        }

        public string ErrorDescription
        {
            get
            {
                return _ErrorDescription;
            }
            set
            {
                _ErrorDescription = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorDescription"));
                }
            }
        }

        public string ErrorDetailException
        {
            get { return _ErrorDetailException; }
            set
            {
                _ErrorDetailException = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorDetailException"));
                }
            }
        }
        public bool CanSeeLog
        {
            get
            {
                return App.Log != null;
            }
        }
        public ErrorDialog(string title, string description, string detail)
        {
            InitializeComponent();
            this.DataContext = this;
            ErrorTitle = title;
            ErrorDetailException = detail;
            ErrorDescription = description;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        

        private void OpenLLog_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(App.Log.CurrentFile);
        }
    }
}
