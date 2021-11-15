using System.Text;
using GalaSoft.MvvmLight;

namespace INetApp.Models
{
    /**
     * Class that represents a user in the presentation layer.
     */
    public class OptionsModel : ObservableObject
    {

        private readonly string LABEL_TRAVEL = "VIAJES";
        private readonly string LABEL_INFO = "VENTANILLA";
        private readonly string LABEL_PROFILE = "PERFIL";
        private readonly string LABEL_EPI = "EPIs";
        private readonly string LABEL_EMPLOYEE = "PortalEmpleado";

        public OptionsModel(int optionsId)
        {
            this.checkeado = false;
            this.optionsId = optionsId;
        }

        private string name { get; set; }
        private int optionsId { get; set; }
        private bool checkeado { get; set; }

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

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Message Model Options *****\n");
            stringBuilder.Append("id=" + this.getOptionsId() + "\n");
            stringBuilder.Append("name=" + this.getName() + "\n");
            stringBuilder.Append("selected=" + ((this.isChecked()) ? "true" : "false") + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}
