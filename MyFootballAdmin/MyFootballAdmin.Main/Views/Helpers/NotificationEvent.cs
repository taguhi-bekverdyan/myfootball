using MyFootballAdmin.Main.Views.Notifications;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Helpers
{
        public class NotificationEvent : PubSubEvent<NotificationEventArgs> { }

        public class NotificationEventArgs
        {
        public Notification Notification { get; set; }
        public NotificationEventArgs( Notification notification)
        {
            Notification = notification;
        }
    }

}
