using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{

    /**
     * Class that represents a user logged in the presentation layer.
     */

    public class InecoProjectsModel : BindableObject
    {

        private string pronumero { get; set; }

        public InecoProjectsModel(string pronumero)
        {
            this.pronumero = pronumero;
        }

        private string titulo { get; set; }
        private string tipo { get; set; }


        override public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** User Model Details *****\n");
            stringBuilder.Append("pronumero=" + pronumero + "\n");
            stringBuilder.Append("titulo=" + titulo + "\n");
            stringBuilder.Append("fullname=" + tipo + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}