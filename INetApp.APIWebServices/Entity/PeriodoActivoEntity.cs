using System.Text;
using Newtonsoft.Json;

namespace com.ineco.inetapp.data.entity
{

    //using com.google.gson.annotations.SerializedName;

    /**
     * Category Entity used in the data layer.
     */
    public class PeriodoActivoEntity
    {

        [JsonProperty(PropertyName = "PeriodoActivo")]
        private int periodoActivo;

        public PeriodoActivoEntity()
        {
            //empty
        }

        public int getPeriodoActivo()
        {
            return periodoActivo;
        }

        public void setPeriodoActivo(int periodoActivo)
        {
            this.periodoActivo = periodoActivo;
        }


        override public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Category Entity Details *****\n");
            stringBuilder.Append("id=" + this.getPeriodoActivo() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }
    }
}