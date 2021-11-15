using System.Text;
using GalaSoft.MvvmLight;

namespace INetApp.Models
{

    //using java.util.Date;
    //using java.util.HashMap;

    /**
     * Class that represents a user in the presentation layer.
     */
    public class ResponseModel : ObservableObject
    {
        public static readonly string URL_LABEL = "URL";


        public ResponseModel()
        {
        }

        private int resultado { get; set; }

        public int getResultado()
        {
            return resultado;
        }

        public void setResultado(int resultado)
        {
            this.resultado = resultado;
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Response Model Details *****\n");
            stringBuilder.Append("id=" + this.getResultado() + "\n");
            stringBuilder.Append("*******************************");
            return stringBuilder.ToString();
        }

    }
}
