using MyFootballAdmin.Main.Views.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Helpers
{
   public  interface INotificationService
    {
       void  ShowNotification(NotificationType notificationType, string notificaitonMessage);
    }
}
