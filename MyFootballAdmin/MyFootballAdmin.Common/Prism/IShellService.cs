using MyFootballAdmin.Common.Views;
using Prism.Regions;

namespace MyFootballAdmin.Common.Prism
{
    public interface IShellService
    {
        ShellView ShowShell(string uri);
        ShellView ShowShell(string uri, int w, int h);
        ShellView ShowShell(string uri, int w, int h, NavigationParameters navigationParameters);
        ShellView ShowShell(string uri, NavigationParameters navigationParameters);
    }
}
