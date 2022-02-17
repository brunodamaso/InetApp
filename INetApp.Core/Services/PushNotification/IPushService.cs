using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IPushService 
    {
        bool RegistrarToken(string token);
        Task OnPushAction(IDictionary<string, string> token);
    }
}
