using RawNotification.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.DataAccess.DAInterfaces
{
    public interface IReceiverDA
    {
        IEnumerable<Receiver> GetAllReceiverByOldID(IEnumerable<string> oldReceiverIDList);
        void DeleteAllReceiverHaveNoDevice();
        void InsertReceiver(Receiver receiver);
        Receiver GetReceiverById(long ReceiverId);
        Receiver GetReceiverByOldId(string ReceiverOldId);
        void UpdateReceiver(Receiver receiver);
    }
}
