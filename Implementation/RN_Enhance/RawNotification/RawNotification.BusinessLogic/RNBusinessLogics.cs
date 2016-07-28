using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.BusinessLogic.BLImplements;
using RawNotification.BusinessLogic.BLInterfaces;
using RawNotification.Models.ServerBusinessModels.Parameters;

namespace RawNotification.BusinessLogic
{
    public static class RNBusinessLogics
    {
        public static IDeviceBL GetDeviceBL()
        {
            return new DeviceBL();
        }

        public static INotificationBL GetNotificationBL()
        {
            return new NotificationBL();
        }

        public static IReceiverBL GetReceiverBL()
        {
            return new ReceiverBL();
        }
        public static IRawNotificationSender GetRawNotificationSender(WNSParameter WNSParams)
        {
            return new RawNotificationSender(WNSParams);
        }
    }
}
