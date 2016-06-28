using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DapperExtensions;
using RawNotification.Models.DBModels;
using Dapper;

namespace RawNotification.DataAccess.DAImplements
{
    public class ReceiverNotificationDA : BaseDA, DAInterfaces.IReceiverNotificationDA
    {
        public ReceiverNotificationDA(IDbTransaction trans) : base(trans)
        {
        }

        public void InsertReceiverNotification(ReceiverNotification enitty)
        {
            connection.Insert( enitty, transaction: transaction);
            enitty.Id = getLastestIdentity();
        }
    }
}