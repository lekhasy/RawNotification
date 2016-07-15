using RawNotification.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.Models;

namespace RawNotification.BusinessLogic.BLInterfaces
{
    public interface IReceiverBL : IBaseBL
    {
        IEnumerable<Receiver> GetAllReceiverByOldID(IEnumerable<string> oldReceiverIDList);
        BaseServiceResult<string, long> RegisterOrRenewToken(string ReceiverOldID, TimeSpan NewTokenPeriod);
        bool CheckTokenValid(string receiverToken, long ReceiverId);
    }
}
