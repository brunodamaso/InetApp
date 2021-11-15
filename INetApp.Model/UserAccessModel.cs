using System.Text;
using GalaSoft.MvvmLight;

namespace INetApp.Models
{

    /**
     * Class that represents a user logged in the presentation layer.
     */
    public class UserAccessModel : ObservableObject
    {


        public UserAccessModel()
        {

        }

        public string mensaje { get; set; }
        public bool respuesta { get; set; }

        public string getMensaje()
        {
            return mensaje;
        }

        public void setMensaje(string mensaje)
        {
            this.mensaje = mensaje;
        }



        public bool isRespuesta()
        {
            return respuesta;
        }

        public void setRespuesta(bool respuesta)
        {
            this.respuesta = respuesta;
        }



        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** User Entity Details *****\n");
            stringBuilder.Append("username=" + this.getMensaje() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}
