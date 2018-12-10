using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth0.OidcClient;
using Prism.Mvvm;
using Prism.Regions;

namespace MyFootballAdmin.Common.Controls
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        public LoginViewModel()
        {

        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {


        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
