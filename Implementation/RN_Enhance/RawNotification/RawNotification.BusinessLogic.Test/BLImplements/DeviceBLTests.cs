using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawNotification.BusinessLogic.BLImplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.Models.DBModels;

namespace RawNotification.BusinessLogic.BLImplements.Tests
{
    [TestClass()]
    public class DeviceBLTests
    {
        [TestMethod()]
        public void AddDeviceTest()
        {
            BLImplements.DeviceBL dbl = new DeviceBL();
            dbl.AddDevice(new Device { IMEI = "", URI = "", OSId  = 1, ReceiverNewID = 15232});
        }
    }
}