using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace TestInterface.SharedModels
{
    [DataContract(Namespace = "NotificationModels", Name = "NotificationDataPacket")]
    public class NotificationDataPacket
    {
        [DataMember]
        public string Line1 { get; private set; }

        [DataMember]
        public string Line2 { get; private set; }

        public NotificationDataPacket(string line1, string line2)
        {
            Line1 = line1;
            Line2 = line2;
        }
    }
}
