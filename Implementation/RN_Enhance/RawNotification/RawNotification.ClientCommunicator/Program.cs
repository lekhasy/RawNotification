using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace RawNotification.ClientCommunicator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var address = "net.tcp://{0}:{1}/RNClientService";
                string hostName = ConfigurationManager.AppSettings["HostNameOrIP"];
                string portNumber = ConfigurationManager.AppSettings["PortNumber"];

                // Define the base address for the service
                var baseAddress = new Uri(string.Format(address, hostName, portNumber));

                // Create service host for the CalculatorService type and privide the base address
                using (var serviceHost = new ServiceHost(typeof(RNClientCommunicator), baseAddress))
                {
                    var smb = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();

                    if (smb == null)
                    {
                        smb = new ServiceMetadataBehavior
                        {
                            HttpGetEnabled = false,
                            HttpsGetEnabled = false,
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

                    serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

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

                    serviceHost.AddServiceEndpoint(typeof(IRNClientCommunicator), binding, "");

                    serviceHost.Open();

                    // The service can now be accessed.
                    Console.WriteLine("The RN Client Service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
