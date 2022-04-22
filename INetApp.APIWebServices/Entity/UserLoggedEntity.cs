using INetApp.Models;
using INetApp.APIWebServices.Responses;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class UserLoggedEntity : Response
    {
        [JsonProperty(PropertyName = "usuarioIneco")]
        public string username;

        [JsonProperty(PropertyName = "nombre")]
        public string name;

        [JsonProperty(PropertyName = "apellidos")]
        public string lastname;

        [JsonProperty(PropertyName = "permiso")]
        public bool permission;

        [JsonProperty(PropertyName = "VersionApp")]
        public string version;

        [JsonProperty(PropertyName = "URLDescarga")]
        public string url;

        [JsonProperty(PropertyName = "Obligatorio")]
        public bool requerido;       

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getLastname()
        {
            return lastname;
        }

        public void setLastname(string lastname)
        {
            this.lastname = lastname;
        }

        public bool isPermission()
        {
            return permission;
        }

        public void setPermission(bool permission)
        {
            this.permission = permission;
        }

        public string getVersion()
        {
            return version;
        }

        public void setVersion(string versión)
        {
            this.version = versión;
        }

        public string getUrl()
        {
            return url;
        }

        public void setUrl(string url)
        {
            this.url = url;
        }

        public bool isRequerido()
        {
            return requerido;
        }

        public void setRequerido(bool requerido)
        {
            this.requerido = requerido;
        }

        //override public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** User Entity Details *****\n");
        //    stringBuilder.Append("username=" + this.getUsername() + "\n");
        //    stringBuilder.Append("name=" + this.getName() + "\n");
        //    stringBuilder.Append("lastname=" + this.getLastname() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}

        //public string usuarioIneco { get; set; }
        //public string nombre { get; set; }
        //public string apellidos { get; set; }
    }
}

