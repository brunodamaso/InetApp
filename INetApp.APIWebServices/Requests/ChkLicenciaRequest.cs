using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;
using INetApp.APIWebServices.Requests;

namespace INetApp.APIWebServices.Requests
{
    public class ChkLicenciaRequest 
    {
        public ChkLicenciaRequest() { }

        public ChkLicenciaRequest(string Empresa, string Vendedor, string Licencia, string DeviceId)
        {
            this.emp = Empresa;
            this.usr = Vendedor;
            this.lic = Licencia;
            this.cod = DeviceId;

        }

        public string emp { get; set; }
        public string usr { get; set; }
        public string lic { get; set; }
        public string cod { get; set; }

    }
}
