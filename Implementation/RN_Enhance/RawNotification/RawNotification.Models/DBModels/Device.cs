using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RawNotification.Models.DBModels
{
    [DataContract]
    public class Device : BaseModel
    {
        [DataMember]
        public long ReceiverNewID { get; set; }
        [DataMember]
        public string IMEI { get; set; }
        [DataMember]
        public string URI { get; set; }
        [DataMember]
        public int OSId { get; set; }
    }
}