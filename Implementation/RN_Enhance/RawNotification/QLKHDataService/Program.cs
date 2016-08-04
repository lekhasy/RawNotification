using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace QLKHDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Getting Adresses
                var address = "net.tcp://{0}:{1}/QLKHDataService";
                string hostName = ConfigurationManager.AppSettings["HostNameOrIP"];
                string portNumber = ConfigurationManager.AppSettings["PortNumber"];

                // Define the base address for the service
                var baseAddress = new Uri(string.Format(address, hostName, portNumber));

                var httpaddress = "http://{0}:{1}/QLKHDataService";
                string httpportNumber = ConfigurationManager.AppSettings["HTTPPortNumber"];

                // Define the base http address for the service
                var httpbaseAddress = new Uri(string.Format(httpaddress, hostName, httpportNumber));
                #endregion

                // Create service host for the CalculatorService type and privide the base address
                var serviceHost = new ServiceHost(typeof(QLKHDataService),baseAddress ,httpbaseAddress);

                #region Add Service Behaviors
                var smb = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();

                if (smb == null)
                {
                    smb = new ServiceMetadataBehavior
                    {
                        HttpGetEnabled = true,
                        HttpsGetEnabled = true,
                        MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                    };

                    ServiceThrottlingBehavior throttle = serviceHost.Description.Behaviors.Find<ServiceThrottlingBehavior>();
                    if (throttle == null)
                    {
                        throttle = new ServiceThrottlingBehavior
                        {
                            MaxConcurrentCalls = 1000,
                            MaxConcurrentSessions = 1000,
                            MaxConcurrentInstances = 1000
                        };
                        serviceHost.Description.Behaviors.Add(throttle);
                    }

                    serviceHost.Description.Behaviors.Add(smb);
                }
                #endregion

                #region Create Bindings
                var binding = new NetTcpBinding
                {
                    OpenTimeout = new TimeSpan(1, 30, 0),
                    CloseTimeout = new TimeSpan(1, 30, 0),
                    SendTimeout = new TimeSpan(1, 30, 0),
                    ReceiveTimeout = new TimeSpan(1, 30, 0),
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue,
                    MaxBufferPoolSize = int.MaxValue
                };
                binding.Security.Mode = SecurityMode.None;

                var httpBinding = new BasicHttpBinding
                {
                    OpenTimeout = new TimeSpan(1, 30, 0),
                    CloseTimeout = new TimeSpan(1, 30, 0),
                    SendTimeout = new TimeSpan(1, 30, 0),
                    ReceiveTimeout = new TimeSpan(1, 30, 0),
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue,
                    MaxBufferPoolSize = int.MaxValue
                };
                httpBinding.Security.Mode = BasicHttpSecurityMode.None;
                #endregion


                serviceHost.AddServiceEndpoint(typeof(IQLKHDataService), binding, "");

                serviceHost.AddServiceEndpoint(typeof(IQLKHDataService), httpBinding, "");

                serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("The QLKH Data Service is ready. PORT : {0}", portNumber);
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
