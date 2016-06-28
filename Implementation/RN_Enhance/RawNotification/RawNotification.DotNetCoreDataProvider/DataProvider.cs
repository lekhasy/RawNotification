using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.DotNetCoreDataProviders.Implements;
using RawNotification.DotNetCoreDataProviders.Interfaces;

namespace RawNotification.DotNetCoreDataProviders
{
    public class DataProvider : IDataProvider
    {
        INotificationInfoDataProvider _NotificationInfoDataProvider;
        IRNServiceProvider _ServiceProvider;

        public DataProvider(INotificationInfoDataProvider notificationInfoDataProvider, IRNServiceProvider serviceProvider)
        {
            _NotificationInfoDataProvider = notificationInfoDataProvider;
            _ServiceProvider = serviceProvider;
        }

        public DataProvider()
        {

        }

        public async Task<INotificationInfoDataProvider> GetNotifiInfoDataProviderAsync()
        {
            return _NotificationInfoDataProvider ?? (_NotificationInfoDataProvider = await NotificationInfoDataProvider.CreateNew());
        }

        public IRNServiceProvider GetServiceProviderAsync()
        {
            return _ServiceProvider ?? (_ServiceProvider = new RNServiceProvider());
        }
    }
}
