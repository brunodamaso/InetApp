////using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class ActionWorkPartsEntity
    {
        [JsonPropertyName("accion")]
        public int accion { get; set; }

        [JsonPropertyName( "parte")]
        public WorkPartsEntity parte { get; set; }


        public int getAccion()
        {
            return accion;
        }

        public void setAccion(int accion)
        {
            this.accion = accion;
        }

        public WorkPartsEntity getParte()
        {
            return parte;
        }

        public void setParte(WorkPartsEntity parte)
        {
            this.parte = parte;
        }

        //override public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.Append("***** Message Options *****\n");
        //    stringBuilder.Append("accion=" + this.getAccion() + "\n");
        //    stringBuilder.Append("parte=" + this.getParte() + "\n");
        //    stringBuilder.Append("*******************************");
        //    return stringBuilder.ToString();
        //}

    }
}