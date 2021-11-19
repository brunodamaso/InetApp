using Xamarin.Forms;

namespace INetApp.Models
{

    /**
     * Class that represents a user in the presentation layer.
     */
    public class SendLineasDetalleModel : BindableObject
    {

        private string fechaImputacion;
        private int pdLineaId;
        private int perParteId;
        private double procuenta;
        private string pronumero;


        public string getFechaImputacion()
        {
            return fechaImputacion;
        }

        public void setFechaImputacion(string fechaImputacion)
        {
            this.fechaImputacion = fechaImputacion;
        }

        public int getPdLineaId()
        {
            return pdLineaId;
        }

        public void setPdLineaId(int pdLineaId)
        {
            this.pdLineaId = pdLineaId;
        }

        public int getPerParteId()
        {
            return perParteId;
        }

        public void setPerParteId(int perParteId)
        {
            this.perParteId = perParteId;
        }

        public double getProcuenta()
        {
            return procuenta;
        }

        public void setProcuenta(double procuenta)
        {
            this.procuenta = procuenta;
        }

        public string getPronumero()
        {
            return pronumero;
        }

        public void setPronumero(string pronumero)
        {
            this.pronumero = pronumero;
        }
    }
}