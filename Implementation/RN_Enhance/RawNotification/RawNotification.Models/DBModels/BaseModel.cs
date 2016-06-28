using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
namespace RawNotification.Models.DBModels
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public long Id { get; set; }
    }
}