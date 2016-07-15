using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QLKHDataService
{
    [ServiceBehavior(MaxItemsInObjectGraph = 2147483646, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class QLKHDataService : IQLKHDataService
    {
        public IEnumerable<KhachHangInfo> GetAllKhachHangs()
        {
            throw new NotImplementedException();
        }
    }
}
