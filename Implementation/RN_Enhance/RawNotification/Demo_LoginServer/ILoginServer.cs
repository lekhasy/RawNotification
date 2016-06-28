using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RawNotification.Models;

namespace Demo_LoginServer
{
    [ServiceContract]
    public interface ILoginServer
    {
        [OperationContract]
        BaseServiceResult<string, long> Login(string username, string password);
    }
}
