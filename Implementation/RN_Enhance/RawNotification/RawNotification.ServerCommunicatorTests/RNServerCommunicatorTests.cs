using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawNotification.ServerCommunicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.ServerCommunicator.Tests
{
    [TestClass()]
    public class RNServerCommunicatorTests
    {
        [TestMethod()]
        public void AddNotificationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendAllNotificationTest()
        {
            RNServerCommunicator com = new RNServerCommunicator();
            var result = com.SendAllNotification();
        }

        [TestMethod()]
        public void AddReceiverTest()
        {
            Assert.Fail();
        }
    }
}