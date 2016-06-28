using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKH.Models
{
    public partial class KhachHang
    {
        public string RNReceiverOldID
        {
            get
            {
                return KhachHangID.ToString();
            }
        }
    }
}