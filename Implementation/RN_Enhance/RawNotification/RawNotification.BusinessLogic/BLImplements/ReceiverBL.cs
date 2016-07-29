using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.DataAccess;
using RawNotification.Models;
using RawNotification.Models.DBModels;

namespace RawNotification.BusinessLogic.BLImplements
{
    internal class ReceiverBL : BaseServiceBL, BLInterfaces.IReceiverBL
    {
        public ReceiverBL() : base()
        {
        }

        public ReceiverBL(IRawNotificationDB db) : base(db)
        {
        }

        public IEnumerable<Receiver> GetAllReceiverByOldID(IEnumerable<string> oldReceiverIDList)
        {
            return DB.ReceiverDA.GetAllReceiverByOldID(oldReceiverIDList);
        }

        public BaseServiceResult<string, long> RegisterOrRenewToken(string ReceiverOldID, TimeSpan NewTokenPeriod)
        {
            try
            {
                string Token = Guid.NewGuid().ToString();

                var receiver = DB.ReceiverDA.GetReceiverByOldId(ReceiverOldID);

                if (receiver == null)
                {
                    // receiver does not exists, insert new receiver and a token for that receiver
                    receiver = new Receiver { OldId = ReceiverOldID, DeviceCount = 0};
                    DB.ReceiverDA.InsertReceiver(receiver);    
                }
                LoginToken logintoken = new LoginToken { TokenExpiredTime = DateTime.Now.Add(NewTokenPeriod), AccessToken = Token, ReceiverId = receiver.Id };
                DB.LoginTokenDA.InsertToken(logintoken);

                DB.commit();
                return new BaseServiceResult<string, long>(ResultStatusCodes.OK, Token, receiver.Id);
            } catch (Exception ex)
            {
                _Logger.Error("RegisterOrRenewToken", ex);
                return BaseServiceResult<string, long>.InternalErrorResult;
            }
        }

        public bool CheckLoginTokenValid(string receiverToken, long ReceiverId)
        {
            if (receiverToken == null)
                return false;

            var ReceiverTokenList = DB.LoginTokenDA.GetTokensByReceiverId(ReceiverId);
            var token = ReceiverTokenList.FirstOrDefault(tk => tk.AccessToken == receiverToken);

            if (token != null)
            {
                DB.LoginTokenDA.RemoveLoginTokenById(token.Id);
                DB.commit();
                return true;
            }
            return false;
        }
    }
}