using log4net;
using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Main
{
  public class MainViewModel : BindableBase 
  {

    private readonly IShellService _shellService;
    private readonly IEventAggregator _eventAggregator;
    private readonly INotificationService _notificationService;
    private readonly IRegionManager _regionManager;
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService, IRegionManager regionManager)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
            _regionManager = regionManager;
            log4net.Config.XmlConfigurator.Configure();
            log.Info("In MainView.xaml");
        }


  }
}
