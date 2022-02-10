using System.Threading.Tasks;

namespace INetApp.Services.Settings
{
    public interface ISettingsService
    {
        string UserName { get; set; }
        string UserPass { get; set; }
        string AuthAccessToken { get; set; }
        string NameInitial { get; set; }
        string NameFull { get; set; }
        bool Permission { get; set; }
        string Version { get; set; }
        bool Requerido { get; set; }
        string Url { get; set; }
        string PushToken { get; set; }        
    }
}
