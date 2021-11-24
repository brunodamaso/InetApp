using System.Text;
using Newtonsoft.Json;

namespace INetApp.APIWebServices.Entity
{
    public class MessageEntity
    {
        public static readonly string _ID = "_id";
        public static readonly string ID = "id";
        public static readonly string CATEGORY_ID = "category_id";
        public static readonly string USERNAME = "username"; //bd se coloco directo
        public static readonly string NAME = "name";
        public static readonly string FAVORITE = "favorite";
        public static readonly string DATE = "date";
        public static readonly string DATA = "data";

        public static readonly string DATA_HEADERS = "Cabecera";
        public static readonly string DATA_DATA = "Datos";
        public static readonly string HEADER_PREFIX_LABEL = "Nombre";
        public static readonly string DATA_PREFIX_LABEL = "Campo";

        //@DatabaseField(generatedId = true, columnName = _ID)
        //[PrimaryKey]
        public int id { get; set; }

        //@DatabaseField(columnName = ID)
        [JsonProperty(PropertyName = "Codigo")]
        public int messageId { get; set; }

        //[JsonProperty(PropertyName = USERNAME)]
        //@DatabaseField(columnName = USERNAME)
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        //[JsonProperty(PropertyName = "CATEGORY_ID")]
        //DatabaseField(columnName = CATEGORY_ID)
        [JsonProperty(PropertyName = "category_id")]
        public int categoryId { get; set; }

        [JsonProperty(PropertyName = "Descripcion")]
        //@DatabaseField(columnName = NAME)
        public string name { get; set; }

        //[JsonProperty(PropertyName = FAVORITE)]
        //@DatabaseField(columnName = FAVORITE)
        [JsonProperty(PropertyName = "favorite")]
        public bool favorite { get; set; }

        //@DatabaseField(columnName = DATE)
        [JsonProperty(PropertyName = "Fecha")]
        public string date { get; set; }

        //@DatabaseField(columnName = DATA)
        public string data { get; set; }

        public MessageEntity()
        {
            //empty
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getMessageId()
        {
            return this.messageId;
        }

        public void setMessageId(int messageId)
        {
            this.messageId = messageId;
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }


        public bool isFavorite()
        {
            return this.favorite;
        }

        public void setFavorite(bool favorite)
        {
            this.favorite = favorite;
        }

        public int getCategoryId()
        {
            return this.categoryId;
        }

        public void setCategoryId(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public string getDate()
        {
            return this.date;
        }

        public void setDate(string date)
        {
            this.date = date;
        }

        public string getData()
        {
            return this.data;
        }

        public void setData(string data)
        {
            this.data = data;
        }

        public string getUsername()
        {
            return this.username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** User Entity Details *****\n");
            stringBuilder.Append("id=" + getMessageId() + "\n");
            stringBuilder.Append("username=" + getUsername() + "\n");
            stringBuilder.Append("category id=" + getCategoryId() + "\n");
            stringBuilder.Append("name=" + getName() + "\n");
            stringBuilder.Append("favorite=" + isFavorite() + "\n");
            stringBuilder.Append("date=" + getDate() + "\n");
            stringBuilder.Append("data=" + getData() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}