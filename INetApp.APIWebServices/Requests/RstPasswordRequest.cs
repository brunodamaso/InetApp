using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;

namespace INetApp.APIWebServices.Requests
{
    public class RstPasswordRequest
    {
        public RstPasswordRequest() { }

        public RstPasswordRequest(string Mail, string Usuario, string Password)
        {
            this.mail = Mail;
            this.user = Usuario;
            this.pass = Password;
        }
        public string mail { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
    }
}
