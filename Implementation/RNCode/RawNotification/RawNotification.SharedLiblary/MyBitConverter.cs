﻿using System;
using System.Text;
using System.IO;

namespace RawNotification.SharedLiblary
{
    internal class MyBitConverter<T>
    {
        private System.Runtime.Serialization.Json.DataContractJsonSerializer se;

        internal MyBitConverter()
        {
            se = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
        }

        public byte[] ObjectToBytes(T Input)
        {
            return Encoding.UTF8.GetBytes(Serialize(Input));
        }

        public T BytesToObject(byte[] data)
        {
            return DeSerialize(Encoding.UTF8.GetString(data));
        }

        private static string Serialize(T obj)
        {
            using (MemoryStream memorystream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memorystream,Encoding.UTF8))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                seria.WriteObject(memorystream, obj);
                memorystream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        private static T DeSerialize(string json)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)seria.ReadObject(stream);
            }
        }

    }
}
