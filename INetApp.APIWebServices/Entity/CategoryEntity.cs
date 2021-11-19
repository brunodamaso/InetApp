﻿using System.Text;
using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;

namespace INetApp.APIWebServices.Entity
{
    public class CategoryEntity
    {
        public CategoryEntity() { }

        [JsonProperty(PropertyName = "IdAplicacion")]
        private int categoryId;

        [JsonProperty(PropertyName = "NombreBandeja")]
        private string name;

        [JsonProperty(PropertyName = "Pendientes")]
        private int pendingMessages;

        [JsonProperty(PropertyName = "UrlIcono")]
        private string UrlIcono;

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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Category Entity Details *****\n");
            stringBuilder.Append("id=" + getCategoryId() + "\n");
            stringBuilder.Append("name=" + getName() + "\n");
            stringBuilder.Append("pending messages=" + getPendingMessages() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}