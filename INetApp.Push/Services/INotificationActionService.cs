using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.Services.Push
{
    public interface INotificationActionService
    {
        void TriggerAction(string action);
    }
}
