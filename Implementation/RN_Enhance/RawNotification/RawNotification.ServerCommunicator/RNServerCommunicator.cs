using System;
using System.Collections.Generic;
using System.ServiceModel;
using RawNotification.BusinessLogic.BLImplements;
using RawNotification.BusinessLogic;
using System.Threading.Tasks;
using RawNotification.Models;
using RawNotification.Models.ServerBusinessModels.Parameters;

namespace RawNotification.ServerCommunicator
{
    [ServiceBehavior(MaxItemsInObjectGraph = 2147483646, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RNServerCommunicator : IRNServerCommunicator
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        public BaseServiceResult AddNotification(byte[] NotificationContent, byte[] NotificationPreviewContent, IEnumerable<string> oldReceiverIdList)
        {
            try
            {
                using (var notificationBL = new NotificationBL())
                {
                    notificationBL.AddNotification(NotificationContent, NotificationPreviewContent, oldReceiverIdList);
                    return new BaseServiceResult(ResultStatusCodes.OK, null);
                }
            } catch(Exception ex)
            {
                Logger.Error("AddNotification", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }

        public BaseServiceResult SendAllNotification()
        {
            try
            {
                var wnsParam = new WNSParameter(Settings.WNSPackageSID.Value, Settings.WNSSecretKey.Value, Settings.GetWNSToken, Settings.SetWNSToken);
                using (var sender = new RawNotificationSender(wnsparameter: wnsParam))
                {
                    sender.SendAllNotificationAsync();
                    return new BaseServiceResult( ResultStatusCodes.OK, null);
                }
            } catch (Exception ex)
            {
                Logger.Error("SendAllNotification", ex);
                return BaseServiceResult.InternalErrorResult;
            }
        }

        public BaseServiceResult<string, long> AddReceiver(string oldReceiverId)
        {
            try
            {
                using (var receiverBL = new ReceiverBL())
                {
                    return receiverBL.RegisterOrRenewToken(oldReceiverId, Settings.NewTokenPeriod.Value);
                }
            } catch(Exception ex)
            {
                Logger.Error("Login", ex);
                return BaseServiceResult<string, long>.InternalErrorResult;
            }
        }
    }
}