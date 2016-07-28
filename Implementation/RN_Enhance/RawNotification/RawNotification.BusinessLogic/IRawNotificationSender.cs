using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.BusinessLogic
{
    public interface IRawNotificationSender: IDisposable
    {
        void SendAllNotificationAsync();
    }
}
