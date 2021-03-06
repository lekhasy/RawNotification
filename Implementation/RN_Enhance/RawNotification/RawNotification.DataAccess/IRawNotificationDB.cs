﻿using RawNotification.DataAccess.DAInterfaces;
using System;

namespace RawNotification.DataAccess
{
    public interface IRawNotificationDB : IDisposable
    {
        IDeviceDA DeviceDA { get; }
        INotificationDA NotificationDA { get; }
        IReceiverDA ReceiverDA { get; }
        IReceiverNotificationDA ReceiverNotificationDA { get; }
        IDeviceNotificationDA DeviceNotificationDA { get;}

        System.Data.IDbTransaction gettransaction();

        void commit();
    }
}