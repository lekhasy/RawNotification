using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RawNotification.DataAccess
{
    public abstract class BaseDA
    {
        protected IDbTransaction transaction { get; private set; }
        protected IDbConnection connection { get { return transaction.Connection; } }

        public BaseDA(IDbTransaction trans)
        {
            transaction = trans;
        }

        public int getLastestIdentity()
        {
            var identity = connection.Query<int>("SELECT @@IDENTITY AS Id", transaction: transaction).Single();
            return identity;
        }
    }
}
