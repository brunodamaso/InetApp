using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{
    /**
     * Class that represents a user in the presentation layer.
     */
    public class PeriodoActivoModel : BindableObject
    {

        public PeriodoActivoModel()
        {
        }
        public PeriodoActivoModel(int _periodoActivo)
        {
            periodoActivo = _periodoActivo;
        }

        public int periodoActivo { get; set; }

        public int getPeriodoActivo()
        {
            return periodoActivo;
        }

        public void setPeriodoActivo(int periodoActivo)
        {
            this.periodoActivo = periodoActivo;
        }

        //override
        //public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.Append("***** Message Model Options *****\n");
        //    stringBuilder.Append("id=" + this.getPeriodoActivo() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}
