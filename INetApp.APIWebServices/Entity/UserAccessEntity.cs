using System;
using System.Collections.Generic;
using System.Text;
using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;

namespace INetApp.APIWebServices.Entity
{
    public class UserAccessEntity : Response
    {
        public UserAccessEntity() { }

        [JsonProperty(PropertyName = "mensaje")]
        public string mensaje;

        [JsonProperty(PropertyName = "respuesta")]
        public bool respuesta;


        
        public string getMensaje()
        {
            return mensaje;
        }

        public void setMensaje(string mensaje)
        {
            this.mensaje = mensaje;
        }



        public bool isRespuesta()
        {
            return respuesta;
        }

        public void setRespuesta(bool respuesta)
        {
            this.respuesta = respuesta;
        }



        //override public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** User Entity Details *****\n");
        //    stringBuilder.Append("username=" + this.getMensaje() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}