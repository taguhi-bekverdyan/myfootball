using MyFootballAdmin.Main.Views.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Helpers
{
    public class NotificationService : INotificationService
    {
        private readonly IEventAggregator _eventAggregator;

        public NotificationService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public static Notification Notification { get; set; }

        public void ShowNotification(NotificationType notificationType, string notificationMessage)
        {
           Notification.Message = notificationType.ToString() + "! " + notificationMessage;
            Notification.NotificationType = notificationType;
            if (notificationType.Equals(NotificationType.Alert))
            {
                Notification.Colour = "Yellow";
            }
            else if (notificationType.Equals(NotificationType.Error))
            {
                Notification.Colour = "#FF960931";
            }
            else if (notificationType.Equals(NotificationType.Info))
            {
                Notification.Colour = "Blue";
            }
            else if (notificationType.Equals(NotificationType.Warning))
            {
                Notification.Colour = "Red";

            }
            _eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArgs(Notification));
        }
    }
}
