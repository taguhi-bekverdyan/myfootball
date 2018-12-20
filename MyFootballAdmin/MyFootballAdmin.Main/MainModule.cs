using Microsoft.Practices.Unity;
using MyFootballAdmin.Common;
using MyFootballAdmin.Main.Views.Main;
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
            _regionManager.RegisterViewWithRegion(RegionNames.WindowContentRegion, typeof(MainView));

            //register views
            _unityContainer.RegisterType(typeof(object), typeof(MainView), nameof(MainView));
        }
    }
}
