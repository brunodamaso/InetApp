using System.Threading.Tasks;

namespace INetApp.Services.Settings
{
    public interface ISettingsService
    {
        string UserName { get; set; }
        string UserPass { get; set; }
        string AuthAccessToken { get; set; }
        string AuthIdToken { get; set; }
        string IdentityEndpointBase { get; set; }
        string GatewayShoppingEndpointBase { get; set; }
        string GatewayMarketingEndpointBase { get; set; }
    }
}
