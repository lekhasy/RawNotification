using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RawNotification.SharedLibs
{
    public class JSONObjectSerializer<T>
    {
        private System.Runtime.Serialization.Json.DataContractJsonSerializer se;

        public JSONObjectSerializer()
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

        public T PathToOject(string path)
        {
            return DeSerializeFromPath(path);
        }

        public T ReadObjectFromStream(Stream stream)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            return (T)(seria.ReadObject(stream));
        }

        public void ObjectToFile(T obj, string path)
        {
            SerializeToFile(obj, path);
        }

        public void WriteObjectToStream(T obj, Stream stream)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            se.WriteObject(stream, obj);
        }

        public T StringToObject(string data)
        {
            return DeSerialize(data);
        }

        private static string Serialize(T obj)
        {
            using (MemoryStream memorystream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memorystream, Encoding.UTF8))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                seria.WriteObject(memorystream, obj);
                memorystream.Position = 0;
                return reader.ReadToEnd();
            }
        }
        private static void SerializeToFile(T obj, string filepath)
        {
            using (Stream stream = File.OpenWrite(filepath))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                seria.WriteObject(stream, obj);
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

        private static T DeSerializeFromPath(string path)
        {
            using (Stream stream = File.OpenRead(path))
            {
                stream.Position = 0;
                System.Runtime.Serialization.Json.DataContractJsonSerializer seria = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)seria.ReadObject(stream);
            }
        }
    }
}
