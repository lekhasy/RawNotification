using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models.DBModels;

namespace RawNotification.DataAccess.DAInterfaces
{
    public interface ILoginTokenDA
    {
        IEnumerable<LoginToken> GetTokensByReceiverId(long ReceiverId);
        void InsertToken(LoginToken token);
        void RemoveLoginTokenById(long Id);
    }
}
