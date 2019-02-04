using MyFootballAdmin.Main.Views.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Notifications
{
  public class NotificationService : INotificationService
  {
    private readonly IEventAggregator _eventAggregator;
    public static Notification Notification { get; set; }

    public NotificationService(IEventAggregator eventAggregator)
    {
      _eventAggregator = eventAggregator;
    }

    public void ShowNotification(NotificationType notificationType, string notificationMessage)
    {
      var message = notificationType.ToString() + "! " + notificationMessage;
      Notification = new Notification(message, notificationType);
      if (notificationType.Equals(NotificationType.Alert))
      {
        Notification.Colour = "#FFCAC647";
      }
      else if (notificationType.Equals(NotificationType.Error))
      {
        Notification.Colour = "#FF960931";
      }
      else if (notificationType.Equals(NotificationType.Info))
      {
        Notification.Colour = "#0060FC";
      }
      else if (notificationType.Equals(NotificationType.Warning))
      {
        Notification.Colour = "#D81E1E";
      }
      _eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArgs(Notification));
    }
  }
}
