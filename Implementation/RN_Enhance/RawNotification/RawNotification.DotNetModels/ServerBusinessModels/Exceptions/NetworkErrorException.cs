using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.ServerBusinessModels.Exceptions
{
    public class NetworkErrorException : Exception
    {
        public NetworkErrorException(Exception innerException, string message = null) : base(message, innerException)
        {

        }
    }
}
