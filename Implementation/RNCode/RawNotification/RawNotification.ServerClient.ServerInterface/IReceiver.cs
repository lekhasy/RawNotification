using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.ServerClient.ServerInterface
{
    public interface IReceiver
    {
        string RNReceiverOldID
        {
            get;
        }
    }
}
