using System.Windows;
using Prism.Regions;

namespace MyFootballAdmin.Common.Prism
{
    public static class RegionManagerAware
    {
        public static void SetRegionManagerAware(object item, IRegionManager regionManager)
        {
            switch (item)
            {
                case IRegionManagerAware rmAware:
                    rmAware.RegionManager = regionManager;
                    break;
                case FrameworkElement rmAwareFrameworkElement:
                    if (rmAwareFrameworkElement.DataContext is IRegionManagerAware rmAwareDataContext)
                        rmAwareDataContext.RegionManager = regionManager;
                    break;
            }
        }
    }
}
