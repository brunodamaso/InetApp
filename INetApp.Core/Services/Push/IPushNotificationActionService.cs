using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.Services.Push
{
    public interface IPushNotificationActionService : INotificationActionService
    {
        event EventHandler<PushAction> ActionTriggered;
    }
}
