﻿//------------------------------------------------------------------------------
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
namespace RawNotification.DotNetCoreDataProviders.RNClientService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RNClientService.IRNClientCommunicator")]
    public interface IRNClientCommunicator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNClientCommunicator/AddDevice", ReplyAction="http://tempuri.org/IRNClientCommunicator/AddDeviceResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<byte[]>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<bool>))]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> AddDeviceAsync(long ReceiverId, RawNotification.Models.DBModels.Device deviceInfo, string ReceiverToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNClientCommunicator/GetNotificationContent", ReplyAction="http://tempuri.org/IRNClientCommunicator/GetNotificationContentResponse")]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult<byte[]>> GetNotificationContentAsync(long NotificationId, string NotificationAccessKey, string ReceiverToken, long ReceiverId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNClientCommunicator/CheckIfNotificationRead", ReplyAction="http://tempuri.org/IRNClientCommunicator/CheckIfNotificationReadResponse")]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult<bool>> CheckIfNotificationReadAsync(long ReceiverNotificationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRNClientCommunicator/RemoveDevice", ReplyAction="http://tempuri.org/IRNClientCommunicator/RemoveDeviceResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<byte[]>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RawNotification.Models.BaseServiceResult<bool>))]
        System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> RemoveDeviceAsync(string DeviceIMEI, long ReceiverId, string ReceiverToken);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRNClientCommunicatorChannel : RawNotification.DotNetCoreDataProviders.RNClientService.IRNClientCommunicator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RNClientCommunicatorClient : System.ServiceModel.ClientBase<RawNotification.DotNetCoreDataProviders.RNClientService.IRNClientCommunicator>, RawNotification.DotNetCoreDataProviders.RNClientService.IRNClientCommunicator {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public RNClientCommunicatorClient() : 
                base(RNClientCommunicatorClient.GetDefaultBinding(), RNClientCommunicatorClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.NetTcpBinding_IRNClientCommunicator.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNClientCommunicatorClient(EndpointConfiguration endpointConfiguration) : 
                base(RNClientCommunicatorClient.GetBindingForEndpoint(endpointConfiguration), RNClientCommunicatorClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNClientCommunicatorClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(RNClientCommunicatorClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNClientCommunicatorClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(RNClientCommunicatorClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RNClientCommunicatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> AddDeviceAsync(long ReceiverId, RawNotification.Models.DBModels.Device deviceInfo, string ReceiverToken) {
            return base.Channel.AddDeviceAsync(ReceiverId, deviceInfo, ReceiverToken);
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult<byte[]>> GetNotificationContentAsync(long NotificationId, string NotificationAccessKey, string ReceiverToken, long ReceiverId) {
            return base.Channel.GetNotificationContentAsync(NotificationId, NotificationAccessKey, ReceiverToken, ReceiverId);
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult<bool>> CheckIfNotificationReadAsync(long ReceiverNotificationID) {
            return base.Channel.CheckIfNotificationReadAsync(ReceiverNotificationID);
        }
        
        public System.Threading.Tasks.Task<RawNotification.Models.BaseServiceResult> RemoveDeviceAsync(string DeviceIMEI, long ReceiverId, string ReceiverToken) {
            return base.Channel.RemoveDeviceAsync(DeviceIMEI, ReceiverId, ReceiverToken);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IRNClientCommunicator)) {
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
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IRNClientCommunicator)) {
                return new System.ServiceModel.EndpointAddress("net.tcp://lekhasy.ddns.net:2695/RNClientService");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return RNClientCommunicatorClient.GetBindingForEndpoint(EndpointConfiguration.NetTcpBinding_IRNClientCommunicator);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return RNClientCommunicatorClient.GetEndpointAddress(EndpointConfiguration.NetTcpBinding_IRNClientCommunicator);
        }
        
        public enum EndpointConfiguration {
            
            NetTcpBinding_IRNClientCommunicator,
        }
    }
}
