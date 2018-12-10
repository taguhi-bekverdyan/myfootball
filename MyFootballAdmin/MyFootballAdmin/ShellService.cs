using Microsoft.Practices.Unity;
using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Common.Views;
using Prism.Regions;

namespace MyFootballAdmin
{
    public class ShellService : IShellService
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ShellService(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public ShellView ShowShell(string uri)
        {
            var shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri);

            shell.Show();
            return shell;
        }

        public ShellView ShowShell(string uri, int w, int h)
        {
            var shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri);
            shell.Width = w;
            shell.Height = h;
            shell.Show();
            return shell;
        }

        public ShellView ShowShell(string uri, int w, int h, NavigationParameters navigationParameters)
        {
            var shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri, navigationParameters);
            shell.Width = w;
            shell.Height = h;
            shell.Show();
            return shell;
        }

        public ShellView ShowShell(string uri, NavigationParameters navigationParameters)
        {
            var shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();

            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri, navigationParameters);
            shell.Show();
            return shell;
        }
    }
}
