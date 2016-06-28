using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawNotification.DataAccess.DAImplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.DataAccess.DAImplements.Tests
{
    [TestClass()]
    public class ReceiverDATests
    {
        [TestMethod()]
        public void GetReceiverByIdTest()
        {
            using (RawNotificationDB db = new RawNotificationDB())
            {
                var reveiver = db.ReceiverDA.GetReceiverById(9999);
            }
        }
    }
}