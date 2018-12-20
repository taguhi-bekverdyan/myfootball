using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Notifications
{
    public class Notification:BindableBase
    {
        NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public string Colour { get; set; }

        public Notification()
        {

        }

        public Notification(NotificationType notificationType, string message)
        {
            Message = notificationType.ToString()+ "! " + message;
            NotificationType = notificationType;
            if (NotificationType.Equals(NotificationType.Alert))
            {
                Colour = "Green";
            }
            else if (NotificationType.Equals(NotificationType.Error))
            {
                Colour = "#FF960931";
            }
            else if (NotificationType.Equals(NotificationType.Info))
            {
                Colour = "Blue";
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

