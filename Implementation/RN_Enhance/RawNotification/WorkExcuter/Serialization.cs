using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WorkExcuter
{
    public static class ObjectSerization
    {
        static BinaryFormatter formatter = new BinaryFormatter();
        public static void Serization(object ser_obj, String FilePath)
        {
            FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite);
            formatter.Serialize(file, ser_obj);
            file.Close();
            file.Dispose();
        }

        public static object Deserization(string FilePath)
        {
            FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            object rtobj = formatter.Deserialize(file);
            file.Close();
            file.Dispose();
            return rtobj;
        }
    }
}
