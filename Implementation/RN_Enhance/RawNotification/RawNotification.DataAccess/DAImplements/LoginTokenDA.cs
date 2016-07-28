using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DataAccess.DAInterfaces;
using RawNotification.Models.DBModels;
using Dapper;
using DapperExtensions;

namespace RawNotification.DataAccess.DAImplements
{
    public class LoginTokenDA : BaseDA, ILoginTokenDA
    {
        public LoginTokenDA(IDbTransaction trans) : base(trans)
        {

        }

        public IEnumerable<LoginToken> GetTokensByReceiverId(long ReceiverId)
        {
            var p = Predicates.Field<LoginToken>(t => t.ReceiverId, Operator.Eq, ReceiverId);
            return connection.GetList<LoginToken>(predicate: p, transaction: transaction);
        }

        public void InsertToken(LoginToken token)
        {
            connection.Insert(entity: token, transaction: transaction);
        }

        public void RemoveLoginTokenById(long Id)
        {
            var p = Predicates.Field<LoginToken>(t => t.Id, Operator.Eq, Id);
            connection.Delete<LoginToken>(predicate: p, transaction: transaction);
        }
    }
}
