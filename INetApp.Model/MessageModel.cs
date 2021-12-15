using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{
    //using java.util.Date;
    //using java.util.HashMap;

    /**
     * Class that represents a user in the presentation layer.
     */
    public class MessageModel : BindableObject
    {
        public MessageModel() { }
        public MessageModel(int messageId)
        {
            this.messageId = messageId;
            this.checkeado = false;
        }

        public static readonly string URL_LABEL = "URL";

        public int messageId { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public bool favorite { get; set; }
        public MessageDetails fields { get; set; }
        public bool checkeado { get; set; }

        public int getMessageId()
        {
            return messageId;
        }


        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }


        public bool isFavorite()
        {
            return favorite;
        }

        public void setFavorite(bool favorite)
        {
            this.favorite = favorite;
        }

        public int getCategoryId()
        {
            return categoryId;
        }

        public void setCategoryId(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public DateTime getDate()
        {
            return date;
        }

        public void setDate(DateTime date)
        {
            this.date = date;
        }

        //public Dictionary<string, string> getFields()
        //{
        //    return fields;
        //}

        //public void setFields(Dictionary<string, string> fields)
        //{
        //    this.fields = fields;
        //}

        public bool isChecked()
        {
            return checkeado;
        }

        public void setChecked(bool checkeado)
        {
            this.checkeado = checkeado;
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Message Model Details *****\n");
            stringBuilder.Append("id=" + this.getMessageId() + "\n");
            stringBuilder.Append("category id=" + this.getCategoryId() + "\n");
            stringBuilder.Append("name=" + this.getName() + "\n");
            stringBuilder.Append("date=" + this.getDate() + "\n");
            stringBuilder.Append("favorite=" + this.isFavorite() + "\n");
            //stringBuilder.Append("fields size=" + this.getFields() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}
