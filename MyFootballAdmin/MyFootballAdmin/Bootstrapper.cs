using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Windows;
using Auth0.OidcClient;
using Microsoft.Practices.Unity;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Common.Views;
using MyFootballAdmin.Data.Services.CupService;
using MyFootballAdmin.Data.Services.Helpers;
using MyFootballAdmin.Data.Services.LeagueService;
using MyFootballAdmin.Data.Services.MatchService;
using MyFootballAdmin.Data.Services.TeamsService;
using MyFootballAdmin.Data.Services.TournamentService;
using MyFootballAdmin.Main;
using MyFootballAdmin.Main.Views.Error;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;

namespace MyFootballAdmin
{
    public class Bootstrapper : UnityBootstrapper
    {
        public Bootstrapper()
        {
            //AutoWireViewModel logic
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, viewName.EndsWith("View") ? "{0}Model, {1}" : "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => Container.Resolve(type));

            Container.RegisterType<IShellService, ShellService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INotificationService, NotificationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILeagueService, LeagueService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITournamentService, TournamentService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITeamsService, TeamsService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMatchService, MatchService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICupService, CupService>(new ContainerControlledLifetimeManager());
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            var regionManager = RegionManager.GetRegionManager((Shell));
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);
            Auth0Async();
        }

        private readonly string _domain = ConfigurationManager.AppSettings["Auth0:Domain"];
        private readonly string _clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];

        private string _loginResult;

        public string LoginResult
        {
            get => _loginResult;
            set => _loginResult = value;
        }


        public async void  Auth0Async()
        {
            var client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = _domain,
                ClientId = _clientId
            });

            //login window
            var loginResult = await client.LoginAsync();

            if (loginResult.IsError)
            {
                LoginResult = loginResult.Error;

                var errorView = new ErrorView();
                errorView.Show();
            }

            else
            {
                App.Current.MainWindow.Show();
                var accessToken = loginResult.AccessToken;
                var accessTokenExpiresAt = loginResult.AccessTokenExpiration;
                AccessToken.Token = accessToken;
                AccessToken.ExpiresAt = accessTokenExpiresAt;
            }
        }


        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(MainModule));
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }


    }
}
