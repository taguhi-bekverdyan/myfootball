using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Unity;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Common.Views;
using MyFootballAdmin.Main;
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
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            var regionManager = RegionManager.GetRegionManager((Shell));
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);
            App.Current.MainWindow.Show();
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
