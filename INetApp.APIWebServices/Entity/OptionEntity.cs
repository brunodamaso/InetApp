using System.Text;
using INetApp.APIWebServices.Responses;
//using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class OptionEntity : Response
    {
        public OptionEntity() { }

        [JsonPropertyName( "IdAplicacion")]
        public int optionsId;

        [JsonPropertyName( "NombreBandeja")]
        public string name;

        [JsonPropertyName( "marcada")]
        public bool checkeado; //bd

        [JsonPropertyName( "UrlIcono")]
        public string UrlIcono;

        public int getOptionsId()
        {
            return optionsId;
        }

        public void setOptionsId(int optionsId)
        {
            this.optionsId = optionsId;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public bool isChecked()
        {
            return checkeado;
        }

        public void setChecked(bool checked_)
        {
            this.checkeado = checked_;
        }

        public string getUrlIcono()
        {
            return UrlIcono;
        }

        public void setUrlIcono(string urlIcono)
        {
            UrlIcono = urlIcono;
        }

        //public override string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** Category Entity Details *****\n");
        //    stringBuilder.Append("id=" + this.getOptionsId() + "\n");
        //    stringBuilder.Append("name=" + this.getName() + "\n");
        //    stringBuilder.Append("selected=" + ((this.isChecked()) ? "true" : "false") + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}