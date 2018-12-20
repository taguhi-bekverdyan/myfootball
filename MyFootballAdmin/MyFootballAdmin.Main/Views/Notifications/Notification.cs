using MyFootballAdmin.Main.Views.Helpers;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Notifications
{
    public class Notification
    {
        public  NotificationType NotificationType { get; set; }
        public  string Message { get; set; }
        public  string Colour { get; set; }
    }

    public enum NotificationType
    {
        Warning,
        Info,
        Error,
        Alert
    }

}

