using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.Models.ServerBusinessModels
{
    public interface IInterNetworkConnectionChecker
    {
        bool IsInternetAvailable();
    }
}