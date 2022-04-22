using System.Text;
using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class CategoryEntity : Response
    {
        public CategoryEntity() { }

        [JsonProperty(PropertyName = "IdAplicacion")]
        public int categoryId;

        [JsonProperty(PropertyName = "NombreBandeja")]
        public string name;

        [JsonProperty(PropertyName = "Pendientes")]
        public int pendingMessages;

        [JsonProperty(PropertyName = "UrlIcono")]
        public string UrlIcono;

        public int getCategoryId()
        {
            return categoryId;
        }

        public void setCategoryId(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getPendingMessages()
        {
            return pendingMessages;
        }

        public void setPendingMessages(int pendingMessages)
        {
            this.pendingMessages = pendingMessages;
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
        //    stringBuilder.Append("id=" + getCategoryId() + "\n");
        //    stringBuilder.Append("name=" + getName() + "\n");
        //    stringBuilder.Append("pending messages=" + getPendingMessages() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}