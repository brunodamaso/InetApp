using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Entity
{
    public class PeriodoActivoEntity : Response
    {

        [JsonProperty(PropertyName = "PeriodoActivo")]
        public int periodoActivo { get; set; }

        public PeriodoActivoEntity()
        {
            //empty
        }

        public int getPeriodoActivo()
        {
            return periodoActivo;
        }

        public void setPeriodoActivo(int periodoActivo)
        {
            this.periodoActivo = periodoActivo;
        }


        //override public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** Category Entity Details *****\n");
        //    stringBuilder.Append("id=" + this.getPeriodoActivo() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}