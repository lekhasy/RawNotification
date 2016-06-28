using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.RawNotificationServer.Params
{
    public class RawNotificationSenderParamForWindows10
    {
        /// <summary>
        /// PackageSID được cấp bởi việc đăng ký app trên Windows Store
        /// </summary>
        internal string PackageSID { get; private set; }

        /// <summary>
        /// SecretKey được cấp bởi việc đăng ký app trên Windows Store
        /// </summary>
        internal string SecretKey { get; private set; }

        /// <summary>
        /// Tạo một lớp chứa các tham số cho việc khởi tạo Windows10RawNotificationSender
        /// </summary>
        /// <param name="packageSID">PackageSID được cấp bởi việc đăng ký app trên Windows Store</param>
        /// <param name="secretKey">SecretKey được cấp bởi việc đăng ký app trên Windows Store</param>
        public RawNotificationSenderParamForWindows10(string packageSID, string secretKey)
        {
            PackageSID = packageSID;
            SecretKey = secretKey;
        }
    }
}
