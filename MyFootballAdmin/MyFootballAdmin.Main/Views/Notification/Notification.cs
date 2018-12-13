using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Notification
{
    public class Notification
    {
        NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public string Colour { get; set; }

        public Notification(NotificationType notificationType, string message)
        {
            Message = message;
            NotificationType = notificationType;
            if (NotificationType.Equals(NotificationType.Alert))
            {
                Colour = "Grey";
            }
            else if (NotificationType.Equals(NotificationType.Error))
            {
                Colour = "Red";
            }
            else if (NotificationType.Equals(NotificationType.Info))
            {
                Colour = "Black";
            }
            else if (NotificationType.Equals(NotificationType.Warning))
            {
                Colour = "Red";
            }
        }
    }

    public enum NotificationType
    {
        Warning,
        Info,
        Error,
        Alert
    }

}

