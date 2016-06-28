using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace RawNotification.DotNetCoreLibs
{
    public sealed class ToastNotificationHelper
    {
        /// <summary>
        /// Show a toast notification from local
        /// </summary>
        /// <param name="title">Title string</param>
        /// <param name="content">Content string</param>
        public static void Show(string title, string content)
        {
            ToastTemplateType type = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(type);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /// <summary>
        /// Show a toast notification with custom sound
        /// </summary>
        /// <param name="title">Title string</param>
        /// <param name="content">Content string</param>
        /// <param name="sound">Custom sound file</param>
        public static void Show(string title, string content, string sound)
        {
            ToastTemplateType type = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(type);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));
            IXmlNode toastNode = toastXml.SelectSingleNode("toast");
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-appx:///" + sound);

            toastNode.AppendChild(audio);
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public static void Show(string title, string content, string sound, string uri)
        {

        }
    }
}
