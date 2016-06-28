using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawNotification.DataAccess.DAImplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.DataAccess.DAImplements.Tests
{
    [TestClass()]
    public class NotificationDATests
    {
        [TestMethod()]
        public void InsertNotificationTest()
        {

        }

        [TestMethod()]
        public void GetNotificationByIdTest()
        {
            RawNotificationDB db = new RawNotificationDB();
            var noti = db.NotificationDA.GetNotificationById(10048);
            Assert.IsNotNull(noti);
        }
    }
}