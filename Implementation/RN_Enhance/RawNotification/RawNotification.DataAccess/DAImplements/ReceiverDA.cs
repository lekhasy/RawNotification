using System.Collections.Generic;
using System.Data;
using System.Linq;
using RawNotification.Models.DBModels;
using DapperExtensions;
using Dapper;
using System;

namespace RawNotification.DataAccess.DAImplements
{
    public class ReceiverDA : BaseDA, DAInterfaces.IReceiverDA
    {
        public ReceiverDA(IDbTransaction trans) : base(trans)
        {
        }

        public void DeleteAllReceiverHaveNoDevice()
        {
            connection.Execute(RawNotificationDB.StoreProcedureList.RemoveAllReceiverHaveNoDeviceProcName, null, commandType: CommandType.StoredProcedure, transaction: transaction);
        }

        public IEnumerable<Receiver> GetAllReceiverByOldID(IEnumerable<string> oldReceiverIDList)
        {
            return connection.Query<Receiver>("SELECT * FROM Receiver where OldId in @lst", new { lst = oldReceiverIDList }, transaction: transaction);
        }

        public Receiver GetReceiverById(long ReceiverId)
        {
            return connection.Get<Receiver>(ReceiverId, transaction);
        }

        public Receiver GetReceiverByOldId(string ReceiverOldId)
        {
            IFieldPredicate equalpre = Predicates.Field<Receiver>(r => r.OldId, Operator.Eq, ReceiverOldId);
            return connection.GetList<Receiver>(equalpre, transaction: transaction).FirstOrDefault();
        }

        public void InsertReceiver(Receiver receiver)
        {
            connection.Insert(receiver, transaction: transaction);
            receiver.Id = getLastestIdentity();
        }

        public void UpdateReceiver(Receiver receiver)
        {
            connection.Update(receiver, transaction: transaction);
        }
    }
}
