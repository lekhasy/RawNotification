using System;
using System.Collections.Generic;
using System.Text;

namespace TestInterface
{
    public class TestReceiver : RawNotification.ServerClient.ServerInterface.IReceiver
    {
        private string _OldID;
        public string RNReceiverOldID
        {
            get
            {
                return _OldID;
            }
        }

        public TestReceiver(string id)
        {
            _OldID = id;
        }
    }
}
