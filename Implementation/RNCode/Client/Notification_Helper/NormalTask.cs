using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace Notification_Helper_Client
{
    public class Normal_Task
    {
        public async static Task<PushNotificationChannel> getChanel()
        {
            PushNotificationChannel channel = null;
            try
            {
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                return channel;
            }
            catch (Exception ex)
            {
                // Không thể tạo một kênh thông báo.
                return null;
            }
        }
        public class MyOandBConverter
        {
            public static System.IO.MemoryStream ObjectToBytes(object Input)
            {
                System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                System.Runtime.Serialization.Json.DataContractJsonSerializer se = new System.Runtime.Serialization.Json.DataContractJsonSerializer(Input.GetType());
                se.WriteObject(mstream, Input);
                return mstream;
            }

            public static object B2O(byte[] data, Type OutputType)
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer se = new System.Runtime.Serialization.Json.DataContractJsonSerializer(OutputType);
                return se.ReadObject(new System.IO.MemoryStream(data));
            }

        }
    }
}
