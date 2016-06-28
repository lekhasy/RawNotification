using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using RawNotification.Models.ServerBusinessModels;

namespace RawNotification.ServiceLib
{
    public class InterNetworkConnectionChecker : IInterNetworkConnectionChecker
    {
        log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        public bool IsInternetAvailable()
        {
            int trytime = 5;
            string hostname = "Google.com";
            try
            {
                Ping pinger = new Ping();
                for (int i = 0; i < trytime; i++)
                {
                    var respond = pinger.Send(hostname);
                    if (respond.Status == IPStatus.Success)
                        return true;
                }
                _Logger.Error("Host " + hostname + " not respond after " + trytime + " ping requests.");
                return false;
            } catch(Exception ex)
            {
                _Logger.Error("an error occurred while pinging host name " + hostname, ex);
                return false;
            }
        }
    }
}
