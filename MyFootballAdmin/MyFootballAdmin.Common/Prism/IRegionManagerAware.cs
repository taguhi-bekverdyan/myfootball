using Prism.Regions;

namespace MyFootballAdmin.Common.Prism
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}
