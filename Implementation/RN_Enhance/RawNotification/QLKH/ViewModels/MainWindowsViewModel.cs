using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public class MainWindowsViewModel : SuperViewModel
    {
        #region Attibutes
        private LinkGroupCollection groups = new LinkGroupCollection();
        private Uri _ContentSource;
        String _WindowTitle;
        #endregion

        #region Properties
        public Uri ContentSource
        {
            get
            {
                return _ContentSource;
            }
            set
            {
                _ContentSource = value;
                OnPropertyChanged("ContentSource");
            }
        }

        public String WindowTitle
        {
            get { return _WindowTitle; }
            set
            {
                _WindowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        public LinkGroupCollection Groups
        {
            get { return this.groups; }
        }

        #endregion

        public MainWindowsViewModel()
        {
            WindowTitle = "Hello " + App.CurentHuman.HoTen;
            // fill groups

            if (App.CurrentPermissions.HoTroGiaoTiepKH)
            {
                var Home = new LinkGroup { DisplayName = "Welcome" };
                Home.Links.Add(new Link { DisplayName = "Home", Source = new Uri("/Pages/Home.xaml", UriKind.Relative) });
                this.groups.Add(Home);
                var Customer = new LinkGroup { DisplayName = "Customer" };
                Customer.Links.Add(new Link { DisplayName = "Customer info", Source = new Uri("/Pages/Customer.xaml", UriKind.Relative) });
                Customer.Links.Add(new Link { DisplayName = "Birthday", Source = new Uri("/Pages/BirthDay.xaml", UriKind.Relative) });
                this.groups.Add(Customer);
                this.ContentSource = new Uri("/Pages/Home.xaml", UriKind.Relative);
            }

            

            if (App.CurrentPermissions.BaoCaoThongKe)
            {
                var StatisticalAndReport = new LinkGroup { DisplayName = "Statistical & Report" };
                StatisticalAndReport.Links.Add(new Link { DisplayName = "Statistical", Source = new Uri("/pages/Statistical.xaml", UriKind.Relative) });
                StatisticalAndReport.Links.Add(new Link { DisplayName = "Report", Source = new Uri("/Pages/Reports.xaml", UriKind.Relative) });
                groups.Add(StatisticalAndReport);
                if (ContentSource == null)
                {
                    this.ContentSource = new Uri("/Pages/Statisticals.xaml", UriKind.Relative);
                }
            }

            if (App.CurrentPermissions.QLTTNV) // có toàn quyền quản lí nhân viên có nghĩa là Admin
            {
                var Administrator = new LinkGroup { DisplayName = "Admin Work Space" };
                Administrator.Links.Add(new Link { DisplayName = "Backup", Source = new Uri("/Pages/Backup.xaml", UriKind.Relative) });
                Administrator.Links.Add(new Link { DisplayName = "Setting", Source = new Uri("/Pages/AdminSettings.xaml", UriKind.Relative) });
                Administrator.Links.Add(new Link { DisplayName = "User Manage", Source = new Uri("/Pages/Usermanage.xaml", UriKind.Relative) });
                groups.Add(Administrator);
                if (ContentSource == null)
                {
                    this.ContentSource = new Uri("/Pages/Backup.xaml", UriKind.Relative);
                }
            }

            if (App.CurrentPermissions.XemTTQuaTang)
            {
                var Gift = new LinkGroup { DisplayName = "Gift" };
                Gift.Links.Add(new Link { DisplayName = "Gift View", Source= new Uri("/Pages/GiftView.xaml",UriKind.Relative) });

                if (App.CurrentPermissions.QLTTQuaTang)
                {
                    Gift.Links.Add(new Link { DisplayName = "Gift Manage", Source = new Uri("/Pages/GiftManage.xaml", UriKind.Relative) });
                }
                groups.Add(Gift);
            }

        }
    }
}
