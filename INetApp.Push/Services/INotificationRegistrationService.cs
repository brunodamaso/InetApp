using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace INetApp.Services.Push
{
    public interface INotificationRegistrationService
    {
        Task DeregisterDeviceAsync();
        Task RegisterDeviceAsync(params string[] tags);
        Task RefreshRegistrationAsync();
    }
}
