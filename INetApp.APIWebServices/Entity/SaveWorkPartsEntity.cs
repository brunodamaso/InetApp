using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Entity
{
    public class SaveWorkPartsEntity : Response
    {

        [JsonProperty(PropertyName = "GuardarParteResult")]
        public WorkPartsEntity guardarParteResult { get; set; }

        public WorkPartsEntity getGuardarParteResult() {
            return guardarParteResult;
        }

        public void setGuardarParteResult(WorkPartsEntity guardarParteResult) {
            this.guardarParteResult = guardarParteResult;
        }

        //override public string ToString() {
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.Append("***** SaveWorkPartsEntity *****\n");
        //    stringBuilder.Append("guardarParteResult=" + this.getGuardarParteResult() + "\n");
        //    stringBuilder.Append("*******************************");
        //    return stringBuilder.ToString();
        //}

    }
}