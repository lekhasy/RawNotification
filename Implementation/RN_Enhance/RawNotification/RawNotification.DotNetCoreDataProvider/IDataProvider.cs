using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreDataProviders.Interfaces;
//using RawNotification.DotNetCoreDataProviders.RNClientService;
using RawNotification.Models.DBModels;

namespace RawNotification.DotNetCoreDataProviders
{
    public interface IDataProvider
    {
        IRNServiceProvider GetServiceProviderAsync();
        Task<INotificationInfoDataProvider> GetNotifiInfoDataProviderAsync();
    }
}