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
using MyFootballAdmin.Main.Views.TournamentEdit;

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

            _regionManager.RegisterViewWithRegion(RegionNames.NotificationRegion, typeof(NotificationView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
            _regionManager.RegisterViewWithRegion(RegionNames.BesidesToolBarRegion, typeof(BesidesToolBarView));
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ToolBarView));
            _regionManager.RegisterViewWithRegion(RegionNames.AddTournamentRegion, typeof(AddTournamentView));

            //register views
            _unityContainer.RegisterType<object, MainView>(typeof(MainView).FullName);
            _unityContainer.RegisterType<object, NotificationView>(typeof(NotificationView).FullName);
            _unityContainer.RegisterType<object, AddTournamentView>(typeof(AddTournamentView).FullName);
            _unityContainer.RegisterType<object, ToolBarView>(typeof(ToolBarView).FullName);
            _unityContainer.RegisterType<object, BesidesToolBarView>(typeof(BesidesToolBarView).FullName);
            _unityContainer.RegisterType<object, PausesView>(typeof(PausesView).FullName);
            _unityContainer.RegisterType<object, AddPauseView>(typeof(AddPauseView).FullName);
            _unityContainer.RegisterType<object, MatchEditView>(typeof(MatchEditView).FullName);
            _unityContainer.RegisterType<object, TournamentsView>(typeof(TournamentsView).FullName);
            _unityContainer.RegisterType<object, TournamentEditView>(typeof(TournamentEditView).FullName);
        }
    }
}
