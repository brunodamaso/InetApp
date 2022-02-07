using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.Services.Push
{
    public static class PushBootstrap
    {
        public static void Begin(Func<IDeviceInstallationService> deviceInstallationService)
        {
            ServiceContainer.Register(deviceInstallationService);

            ServiceContainer.Register<IPushNotificationActionService>(()
                => new PushNotificationActionService());

            ServiceContainer.Register<INotificationRegistrationService>(()
                => new NotificationRegistrationService(
                    PushConfig.BackendServiceEndpoint,
                    PushConfig.ApiKey));
        }
    }
}
