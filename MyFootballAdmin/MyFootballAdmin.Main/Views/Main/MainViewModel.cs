using System.Configuration;
using System.Text;
using Auth0.OidcClient;
using log4net;
using MyFootballAdmin.Common.Prism;
using Prism.Commands;
using Prism.Mvvm;

namespace MyFootballAdmin.Main.Views.Main
{
    public class MainViewModel : BindableBase
    {
        private readonly IShellService _shellService;

        private readonly string _domain = ConfigurationManager.AppSettings["Auth0:Domain"];
        private readonly string _clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainViewModel(IShellService shellService)
        {
            _shellService = shellService;
            log4net.Config.XmlConfigurator.Configure();
            log.Info("In MainView.xaml");
        }


        #region Properties

        private string _loginResult;
        public string LoginResult
        {
            get => _loginResult;
            set => SetProperty(ref _loginResult, value);
        }

        #endregion

        private DelegateCommand _loginCommand;

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginCommandAction));

        public async void LoginCommandAction()
        {
            var client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = _domain,
                ClientId = _clientId
            });

            //login window
            var loginResult=await client.LoginAsync();

            if (loginResult.IsError)
            {
                LoginResult = loginResult.Error;
                log.Error("error");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("Tokens");
            sb.AppendLine("------");
            sb.AppendLine($"id_token: {loginResult.IdentityToken}");
            sb.AppendLine($"access_token: {loginResult.AccessToken}");
            sb.AppendLine($"refresh_token: {loginResult.RefreshToken}");
            sb.AppendLine();

            sb.AppendLine("Claims");
            sb.AppendLine("------");
            foreach (var claim in loginResult.User.Claims)
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }

            LoginResult = sb.ToString();
            log.Info("Login");


        }
    }
}
