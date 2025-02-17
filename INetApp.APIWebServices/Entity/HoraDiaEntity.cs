using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class HoraDiaEntity 
    {
        [JsonProperty(PropertyName = "Lunes")]
        private string lunes;

        [JsonProperty(PropertyName = "Martes")]
        private string martes;

        [JsonProperty(PropertyName = "Miercoles")]
        private string miercoles;

        [JsonProperty(PropertyName = "Jueves")]
        private string jueves;

        [JsonProperty(PropertyName = "Viernes")]
        private string viernes;


        public HoraDiaEntity() {
        }


        public string getLunes() {
            return lunes;
        }

        public void setLunes(string lunes) {
            this.lunes = lunes;
        }

        public string getMartes() {
            return martes;
        }

        public void setMartes(string martes) {
            this.martes = martes;
        }

        public string getMiercoles() {
            return miercoles;
        }

        public void setMiercoles(string miercoles) {
            this.miercoles = miercoles;
        }

        public string getJueves() {
            return jueves;
        }

        public void setJueves(string jueves) {
            this.jueves = jueves;
        }

        public string getViernes() {
            return viernes;
        }

        public void setViernes(string viernes) {
            this.viernes = viernes;
        }
    }
}