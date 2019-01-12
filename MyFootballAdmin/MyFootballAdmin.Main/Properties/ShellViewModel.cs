using MyFootballAdmin.Common.Prism;
using Prism.Mvvm;
using Prism.Regions;

namespace MyFootballAdmin.Common.Views
{
    public class ShellViewModel : BindableBase, IRegionManagerAware
    {
        private readonly IShellService _service;


        private string _title = "MyFootball Admin Panel";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ShellViewModel(IShellService service)
        {
            _service = service;
        }

        public IRegionManager RegionManager { get; set; }
    }
}
