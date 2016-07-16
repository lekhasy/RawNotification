using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models;

namespace QLKHDataService
{
    [ServiceBehavior(MaxItemsInObjectGraph = 2147483646, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class QLKHDataService : IQLKHDataService
    {
        public BaseServiceResult<IEnumerable<KhachHangInfo>> GetAllKhachHangs()
        {
            try
            {
                List<KhachHangInfo> khachhangList = new List<KhachHangInfo>();
                QLKHDataContext db = new QLKHDataContext();
                foreach (var item in db.KhachHangs)
                {
                    khachhangList.Add(new KhachHangInfo { Id = item.KhachHangID, Name = item.ConNguoi.HoTen });
                }
                return new BaseServiceResult<IEnumerable<KhachHangInfo>>(ResultStatusCodes.OK, khachhangList);
            }
            catch
            {
                return BaseServiceResult<IEnumerable<KhachHangInfo>>.InternalErrorResult;
            }
        }
    }
}
