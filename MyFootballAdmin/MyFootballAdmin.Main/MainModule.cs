using Microsoft.Practices.Unity;
using MyFootballAdmin.Common;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Error;
using MyFootballAdmin.Main.Views.Tournaments;
using MyFootballAdmin.Main.Views.Main;
using MyFootballAdmin.Main.Views.MatchEdit;
using MyFootballAdmin.Main.Views.Notifications;
using MyFootballAdmin.Main.Views.Pauses;
using Prism.Modularity;
using Prism.Regions;

namespace MyFootballAdmin.Main
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public MainModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _unityContainer = container;
        }

        public void Initialize()
        {
            //register first view
            _regionManager.RegisterViewWithRegion(RegionNames.NotificationRegion, typeof(NotificationView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
            _regionManager.RegisterViewWithRegion(RegionNames.BesidesToolBarRegion, typeof(BesidesToolBarView));
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ToolBarView));


            //register views
            _unityContainer.RegisterType(typeof(object), typeof(MainView), nameof(MainView));
            _unityContainer.RegisterType(typeof(object), typeof(NotificationView), nameof(NotificationView));
            _unityContainer.RegisterType(typeof(object), typeof(AddTournamentView), nameof(AddTournamentView));
            _unityContainer.RegisterType(typeof(object), typeof(ToolBarView), nameof(ToolBarView));
            _unityContainer.RegisterType(typeof(object), typeof(BesidesToolBarView), nameof(BesidesToolBarView));
            _unityContainer.RegisterType(typeof(object), typeof(PausesView), nameof(PausesView));
            _unityContainer.RegisterType(typeof(object), typeof(MatchEditView), nameof(MatchEditView));
            _unityContainer.RegisterType(typeof(object), typeof(TournamentsView), nameof(TournamentsView));
        }
    }
}
