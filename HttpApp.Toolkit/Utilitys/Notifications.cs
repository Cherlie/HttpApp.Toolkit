using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace HttpApp.Toolkit.Utilitys
{
    public class Notifications
    {
        /// <summary>
        /// 本地推送通知
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public static void Toast(string title, string content)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;//WindowsPhone上只支持这一种
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));
            ToastNotification toast = new ToastNotification(toastXml);
            var guid = Guid.NewGuid().ToString("N").Substring(0, 16);
            toast.Tag = guid;
            toast.Dismissed += (sender, args) => ToastNotificationManager.History.Remove(guid);
            toast.Activated += (sender, args) => ToastNotificationManager.History.Remove(guid);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
