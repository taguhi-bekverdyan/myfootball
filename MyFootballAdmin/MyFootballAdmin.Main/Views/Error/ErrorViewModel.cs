using MyFootballAdmin.Common.Prism;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Error
{
    public class ErrorViewModel:BindableBase
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public ErrorViewModel(IShellService shellService)
        {
            _shellService = shellService;
        }
    }
}
