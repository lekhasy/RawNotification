using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RawNotification.Models;

namespace Demo_LoginServer
{
    [ServiceBehavior(MaxItemsInObjectGraph = int.MaxValue, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

    public class LoginServer : ILoginServer
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseServiceResult<string, long> Login(string username, string password)
        {
            try
            {
                
                LoginDBDataContext db = new LoginDBDataContext();
                var user = db.KhachHangs.FirstOrDefault(k => k.KhachHangID.ToString() == username && k.ConNguoi.MatKhau == password);

                if (user == null) return new BaseServiceResult<string, long>(ResultStatusCodes.NotFound, null, 0);

                using (var service = AppGlobal.getRNServerService())
                {
                    var result = service.AddReceiver(user.KhachHangID.ToString());
                    Logger.Info("Login " + username);
                    return result;
                }
            } catch (Exception ex)
            {
                Logger.Error("Login", ex);
                return BaseServiceResult<string, long>.InternalErrorResult;
            }
        }
    }
}