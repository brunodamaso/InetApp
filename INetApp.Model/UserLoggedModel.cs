using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{
    /**
     * Class that represents a user logged in the presentation layer.
     */
    public class UserLoggedModel : BindableObject
    {
        public UserLoggedModel() { }
        public UserLoggedModel(string username)
        {
            this.username = username;
        }

        public string username { get; set; }
        public string fullName { get; set; }
        public string nameInitial { get; set; }
        public string lastNameInitial { get; set; }
        //BIGAALCA
        public string version { get; set; }
        public string url { get; set; }
        public bool requerido { get; set; }
        public bool permission { get; set; }

        public string getUrl()
        {
            return url;
        }

        public void setUrl(string url)
        {
            this.url = url;
        }

        public string getUsername()
        {
            return username;
        }

        public string getFullName()
        {
            return fullName;
        }

        public void setFullName(string fullName)
        {
            this.fullName = fullName;
        }

        public string getNameInitial()
        {
            return nameInitial;
        }

        public void setNameInitial(string nameInitial)
        {
            this.nameInitial = nameInitial;
        }

        public string getLastNameInitial()
        {
            return lastNameInitial;
        }

        public void setLastNameInitial(string lastNameInitial)
        {
            this.lastNameInitial = lastNameInitial;
        }

        public bool isPermission()
        {
            return permission;
        }

        public void setPermission(bool permission)
        {
            this.permission = permission;
        }

        //BIGAALCA
        public string getVersion()
        {
            return version;
        }

        public void setVersion(string version)
        {
            this.version = version;
        }

        public bool isRequerido()
        {
            return requerido;
        }

        public void setRequerido(bool requerido)
        {
            this.requerido = requerido;
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** User Model Details *****\n");
            stringBuilder.Append("username=" + this.getUsername() + "\n");
            stringBuilder.Append("fullname=" + this.getFullName() + "\n");
            stringBuilder.Append("nameInitial=" + this.getNameInitial() + "\n");
            stringBuilder.Append("lastNameInitial=" + this.getLastNameInitial() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}