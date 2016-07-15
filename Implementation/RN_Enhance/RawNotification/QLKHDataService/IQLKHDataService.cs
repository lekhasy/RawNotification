using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QLKHDataService
{
    [ServiceContract]
    public interface IQLKHDataService
    {
        [OperationContract]
        IEnumerable<KhachHangInfo> GetAllKhachHangs();
    }
}
