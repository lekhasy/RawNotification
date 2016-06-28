using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.Models.ServerBusinessModels;
using RawNotification.Models.ClientCommunicatorModels;

namespace RawNotification.BusinessLogic.RawNotificationSenders
{
    public interface IRNSender
    {
        EnumWNSSendResult SendNotificationData(string DeviceURI, byte[] NotifiInfo);
    }
}