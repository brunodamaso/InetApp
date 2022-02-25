using System.Text;
using Newtonsoft.Json;

namespace com.ineco.inetapp.data.entity
{

    //using com.google.gson.annotations.SerializedName;

    /**
     * Category Entity used in the data layer.
     */
    public class SaveWorkPartsEntity {

        [JsonProperty(PropertyName = "GuardarParteResult")]
        private WorkPartsEntity guardarParteResult;

        public WorkPartsEntity getGuardarParteResult() {
            return guardarParteResult;
        }

        public void setGuardarParteResult(WorkPartsEntity guardarParteResult) {
            this.guardarParteResult = guardarParteResult;
        }

        override public string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("***** SaveWorkPartsEntity *****\n");
            stringBuilder.Append("guardarParteResult=" + this.getGuardarParteResult() + "\n");
            stringBuilder.Append("*******************************");
            return stringBuilder.ToString();
        }

    }
}