using INetApp.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync (string route, IDictionary<string, string> routeParameters = null);
    }
}