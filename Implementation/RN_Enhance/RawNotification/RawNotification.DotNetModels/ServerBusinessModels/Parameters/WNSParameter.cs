using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.Models.ServerBusinessModels.Parameters
{
    public class WNSParameter
    {
        public readonly string WNSPackageSID;
        public readonly string WNSSecretKey;
        public readonly Func<string> GetToken;
        public readonly Action<string> SetToken;
        public WNSParameter(string WNSPackageSID, string WNSSecretKey, Func<string> GetToken, Action<string> SetToken)
        {
            this.WNSPackageSID = WNSPackageSID;
            this.WNSSecretKey = WNSSecretKey;
            this.GetToken = GetToken;
            this.SetToken = SetToken;
        }
    }
}
