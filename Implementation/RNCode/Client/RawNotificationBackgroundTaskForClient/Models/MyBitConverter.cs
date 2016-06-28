using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotificationBackgroundTaskForClient.Models
{
    internal class MyBitConverter<T>
    {
        private System.Runtime.Serialization.Json.DataContractJsonSerializer se;
        private System.IO.MemoryStream mstream;

        /// <summary>
        /// tạo một thể hiện mới của class MyBitConverter
        /// </summary>
        /// <param name="KnowList">Danh sách các kiểu refference có thể xuất hiện trong class T</param>
        internal MyBitConverter(IEnumerable<Type> KnowList)
        {
            se = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T), KnowList);
            mstream = new System.IO.MemoryStream();
        }

        internal MyBitConverter()
        {
            se = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

            mstream = new System.IO.MemoryStream();
        }

        public byte[] ObjectToBytes(T Input)
        {
            mstream = new System.IO.MemoryStream();
            se.WriteObject(mstream, Input);
            return mstream.ToArray();
        }

        public T BytesToObject(byte[] data)
        {
            return (T)se.ReadObject(new System.IO.MemoryStream(data));
        }
    }
}
