using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.Models.ServerBusinessModels.Exceptions
{
    public class MessageException : Exception
    {
        public MessageException(string message) : base(message)
        {
        }
    }
}
