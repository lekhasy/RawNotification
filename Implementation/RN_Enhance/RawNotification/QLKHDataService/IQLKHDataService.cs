using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models;

namespace QLKHDataService
{
    [ServiceContract]
    public interface IQLKHDataService
    {
        [OperationContract]
       BaseServiceResult<IEnumerable<KhachHangInfo>> GetAllKhachHangs();
    }
}
