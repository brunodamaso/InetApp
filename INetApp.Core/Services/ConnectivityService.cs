using Xamarin.Essentials;

namespace INetApp.Services
{
    public class ConnectivityService
    {
        public enum ConnectivityStatusType
        {
            Undefined,
            Online,
            Offline,
            Error,
            Loading
        }

        public ConnectivityStatusType ConnectivityStatus { get; set; }

        public ConnectivityService()
        {
        }

        public bool CheckConnectivity()
        {
            var current = Connectivity.NetworkAccess;

            return (current == NetworkAccess.Internet) || (current == NetworkAccess.Local);
        }
    }
}
