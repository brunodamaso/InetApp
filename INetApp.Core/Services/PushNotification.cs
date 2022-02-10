using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;

namespace INetApp.Services
{
    public class PushNotification
    {
        private IPushService pushService;
        public PushNotification()
        {
            pushService = ViewModelLocator.Resolve<IPushService>();

        }
        public virtual bool RegistrarToken1(string token)
        {
            return pushService.RegistrarToken(token);
        }

    }
}
