using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{
    /**
     * Class that represents a user in the presentation layer.
     */
    public class OptionsModel : BindableObject
    {

        //private readonly string LABEL_TRAVEL = "VIAJES";
        //private readonly string LABEL_INFO = "VENTANILLA";
        //private readonly string LABEL_PROFILE = "PERFIL";
        //private readonly string LABEL_EPI = "EPIs";
        //private readonly string LABEL_EMPLOYEE = "PortalEmpleado";

        public string name { get; set; }
        public int optionsId { get; set; }
        public bool checkeado { get; set; }

        public int getOptionsId()
        {
            return optionsId;
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

        public void setChecked(bool checkeado)
        {
            this.checkeado = checkeado;
        }

        public
        override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Message Model Options *****\n");
            stringBuilder.Append("id=" + getOptionsId() + "\n");
            stringBuilder.Append("name=" + getName() + "\n");
            stringBuilder.Append("selected=" + ((isChecked()) ? "true" : "false") + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}
