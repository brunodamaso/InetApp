using GalaSoft.MvvmLight;

namespace INetApp.Models
{

    /**
     * Class that represents a user in the presentation layer.
     */
    public class SendSaveDataModel : ObservableObject
    {

        private int accion { get; set; }
        private SendWorkPartModel sendWorkPartModel { get; set; }

        public int getAccion()
        {
            return accion;
        }

        public void setAccion(int accion)
        {
            this.accion = accion;
        }


        public SendWorkPartModel getSendWorkPartModel()
        {
            return sendWorkPartModel;
        }

        public void setSendWorkPartModel(SendWorkPartModel sendWorkPartModel)
        {
            this.sendWorkPartModel = sendWorkPartModel;
        }
    }
}