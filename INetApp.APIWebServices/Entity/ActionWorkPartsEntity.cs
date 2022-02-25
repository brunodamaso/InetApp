using System.Text;
using Newtonsoft.Json;

namespace com.ineco.inetapp.data.entity
{

    //using com.google.gson.annotations.SerializedName;

    //using java.util.ArrayList;
    //using java.util.List;

    
    /**
     * Category Entity used in the data layer.
     */
    public class ActionWorkPartsEntity {

        [JsonProperty(PropertyName = "accion")]
        private int accion;

        [JsonProperty(PropertyName = "parte")]
        private WorkPartsEntity parte;


        public int getAccion() {
            return accion;
        }

        public void setAccion(int accion) {
            this.accion = accion;
        }

        public WorkPartsEntity getParte() {
            return parte;
        }

        public void setParte(WorkPartsEntity parte) {
            this.parte = parte;
        }

        override public string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("***** Message Options *****\n");
            stringBuilder.Append("accion=" + this.getAccion() + "\n");
            stringBuilder.Append("parte=" + this.getParte() + "\n");
            stringBuilder.Append("*******************************");
            return stringBuilder.ToString();
        }

    }
}