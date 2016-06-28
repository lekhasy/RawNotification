using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.ServerBusinessModels.Exceptions
{
    public class SendingErrorException : Exception
    {
        public SendingErrorException(Exception innerException, string message = null) : base(message, innerException)
        {

        }
    }
}
