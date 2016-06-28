using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.DataAccess;
using RawNotification.Models;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.BusinessLogic.BLImplements
{
    public class ReceiverBL : BaseServiceBL, BLInterfaces.IReceiverBL
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
                    receiver = new Receiver { OldId = ReceiverOldID, TokenExpiredTime = DateTime.Now.Add(NewTokenPeriod), DeviceCount = 0, AccessToken = Token };
                    DB.ReceiverDA.InsertReceiver(receiver);
                }
                else
                {
                    receiver.AccessToken = Token;
                    receiver.TokenExpiredTime = DateTime.Now.Add(NewTokenPeriod);
                    DB.ReceiverDA.UpdateReceiver(receiver);
                }

                DB.commit();
                return new BaseServiceResult<string, long>(ResultStatusCodes.OK, Token, receiver.Id);
            } catch (Exception ex)
            {
                _Logger.Error("RegisterOrRenewToken", ex);
                return BaseServiceResult<string, long>.InternalErrorResult;
            }
        }

        public bool CheckTokenValid(string receiverToken, long ReceiverId)
        {
            if (receiverToken == null)
                return false;
            Receiver receiver = DB.ReceiverDA.GetReceiverById(ReceiverId);

            if (receiver != null && receiver.AccessToken != null && receiver.TokenExpiredTime != null &&
                receiver.AccessToken == receiverToken && receiver.TokenExpiredTime >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
