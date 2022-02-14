using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.Services
{
    public interface IPushService 
    {
        bool RegistrarToken(string token);
        bool OnPushAction(IDictionary<string, string> token);
    }
}
