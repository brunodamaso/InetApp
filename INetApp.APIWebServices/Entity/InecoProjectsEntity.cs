using System.Text;
using Newtonsoft.Json;

namespace com.ineco.inetapp.data.entity
{

    //using com.google.gson.annotations.SerializedName;

    /**
     * Category Entity used in the data layer.
     */
    public class InecoProjectsEntity {

        [JsonProperty(PropertyName = "Pronumero")]
        private string pronumero;

        [JsonProperty(PropertyName = "Titulo")]
        private string titulo;

        [JsonProperty(PropertyName = "Tipo")]
        private string tipo;

        public InecoProjectsEntity() {

        }


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

        override public string ToString() {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Category Entity Details *****\n");
            stringBuilder.Append("pronumero=" + this.getPronumero() + "\n");
            stringBuilder.Append("titulo=" + this.getTitulo() + "\n");
            stringBuilder.Append("tipo=" + this.getTipo() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}