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

    public static class FireMessageException
    {
        public static void FireMessageExceptionForServiceResult(this BaseServiceResult result)
        {
            string resultContent = string.Empty;
            switch (result.StatusCode)
            {
                case ResultStatusCodes.OK: return;
                case ResultStatusCodes.NotFound:
                    resultContent = "Login info not exists.";
                    break;
                case ResultStatusCodes.InternalServerError:
                    resultContent = "An internal error occurred on server, try again later.";
                    break;
                case ResultStatusCodes.ServiceUnavailable:
                    resultContent = result.Message;
                    break;
                case ResultStatusCodes.UnknownError:
                    resultContent = "An unknow error occurred on server";
                    break;
                case ResultStatusCodes.UnAuthorised:
                    resultContent = "Your login info has been out of date!";
                    break;
                default:
                    resultContent = "this service result has not been implemented yet";
                    break;
            }
            throw new MessageException(resultContent);
        }
    }

}
