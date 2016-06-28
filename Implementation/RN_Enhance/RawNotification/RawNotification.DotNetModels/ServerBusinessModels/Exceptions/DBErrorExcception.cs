using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.ServerBusinessModels.Exceptions
{
    public class DBErrorExcception : Exception
    {
        public DBErrorExcception(Exception innerException, string message = null) : base(message, innerException)
        {

        }
    }
}
