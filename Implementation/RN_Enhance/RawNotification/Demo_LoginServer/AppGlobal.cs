using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Demo_LoginServer
{
    public class AppGlobal
    {
        public static RNServerService.RNServerCommunicatorClient getRNServerService()
        {
            // Check if host is running
            var address = ConfigurationManager.AppSettings["IRNServerService"];
            var myBinding = new NetTcpBinding();
            myBinding.Security.Mode = SecurityMode.None;
            RNServerService.RNServerCommunicatorClient svc = null;
            try
            {
                //Set property for binding
                myBinding.OpenTimeout = new TimeSpan(1, 30, 0);
                myBinding.CloseTimeout = new TimeSpan(1, 30, 0);
                myBinding.SendTimeout = new TimeSpan(16, 30, 0);
                myBinding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                myBinding.MaxReceivedMessageSize = int.MaxValue;
                myBinding.MaxBufferSize = int.MaxValue;
                myBinding.MaxBufferPoolSize = int.MaxValue;
                myBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;

                //Initalize Service Client
                svc = new RNServerService.RNServerCommunicatorClient(myBinding, new EndpointAddress(address));
                foreach (var operation in svc.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }
                svc.Open();
                return svc;
            }
            catch (Exception)
            {
                if (svc != null && svc.State != CommunicationState.Faulted)
                {
                    svc.Close();
                }
                throw;
            }
        }
    }
}
