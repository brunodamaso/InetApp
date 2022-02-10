using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.Services
{
    public interface IPushNotification
    {
        string GetToken();

        //bool RegistrarToken(string token);
    }
}
