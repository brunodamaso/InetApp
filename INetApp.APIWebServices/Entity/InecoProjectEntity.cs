using INetApp.APIWebServices.Responses;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class InecoProjectEntity 
    {
        [JsonPropertyName( "Pronumero")]
        public string pronumero { get; set; }

        [JsonPropertyName( "Titulo")]
        public string titulo { get; set; }

        [JsonPropertyName( "Tipo")]
        public string tipo { get; set; }

        public InecoProjectEntity() { }

        public string getPronumero() {
            return pronumero;
        }

        public void setPronumero(string pronumero) {
            this.pronumero = pronumero;
        }

        public string getTitulo() {
            return titulo;
        }

        public void setTitulo(string titulo) {
            this.titulo = titulo;
        }

        public string getTipo() {
            return tipo;
        }

        public void setTipo(string tipo) {
            this.tipo = tipo;
        }

        //override public string ToString() {
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** Category Entity Details *****\n");
        //    stringBuilder.Append("pronumero=" + this.getPronumero() + "\n");
        //    stringBuilder.Append("titulo=" + this.getTitulo() + "\n");
        //    stringBuilder.Append("tipo=" + this.getTipo() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}