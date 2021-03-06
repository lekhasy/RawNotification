//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 14.0.23107.0
// 
namespace Demo_UWP.RNServerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RNServerService.IRNServerCommunicator")]
    public interface IRNServerCommunicator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNServerCommunicator/SendAllNotification", ReplyAction="http://tempuri.org/IRNServerCommunicator/SendAllNotificationResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<string, long>))]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> SendAllNotificationAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNServerCommunicator/AddNotification", ReplyAction="http://tempuri.org/IRNServerCommunicator/AddNotificationResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<string, long>))]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> AddNotificationAsync(byte[] Notification, byte[] NotificationPreviewContent, System.Collections.ObjectModel.ObservableCollection<string> oldReceiverIdList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNServerCommunicator/AddReceiver", ReplyAction="http://tempuri.org/IRNServerCommunicator/AddReceiverResponse")]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult<string, long>> AddReceiverAsync(string oldReceiverId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRNServerCommunicatorChannel : Demo_UWP.RNServerService.IRNServerCommunicator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RNServerCommunicatorClient : System.ServiceModel.ClientBase<Demo_UWP.RNServerService.IRNServerCommunicator>, Demo_UWP.RNServerService.IRNServerCommunicator {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public RNServerCommunicatorClient() : 
                base(RNServerCommunicatorClient.GetDefaultBinding(), RNServerCommunicatorClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.NetTcpBinding_IRNServerCommunicator.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNServerCommunicatorClient(EndpointConfiguration endpointConfiguration) : 
                base(RNServerCommunicatorClient.GetBindingForEndpoint(endpointConfiguration), RNServerCommunicatorClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNServerCommunicatorClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(RNServerCommunicatorClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNServerCommunicatorClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(RNServerCommunicatorClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNServerCommunicatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> SendAllNotificationAsync() {
            return base.Channel.SendAllNotificationAsync();
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> AddNotificationAsync(byte[] Notification, byte[] NotificationPreviewContent, System.Collections.ObjectModel.ObservableCollection<string> oldReceiverIdList) {
            return base.Channel.AddNotificationAsync(Notification, NotificationPreviewContent, oldReceiverIdList);
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult<string, long>> AddReceiverAsync(string oldReceiverId) {
            return base.Channel.AddReceiverAsync(oldReceiverId);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IRNServerCommunicator)) {
                System.ServiceModel.NetTcpBinding result = new System.ServiceModel.NetTcpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.Security.Mode = System.ServiceModel.SecurityMode.None;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IRNServerCommunicator)) {
                return new System.ServiceModel.EndpointAddress("net.tcp://192.168.220.1:22694/RNServerService");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return RNServerCommunicatorClient.GetBindingForEndpoint(EndpointConfiguration.NetTcpBinding_IRNServerCommunicator);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return RNServerCommunicatorClient.GetEndpointAddress(EndpointConfiguration.NetTcpBinding_IRNServerCommunicator);
        }
        
        public enum EndpointConfiguration {
            
            NetTcpBinding_IRNServerCommunicator,
        }
    }
}
