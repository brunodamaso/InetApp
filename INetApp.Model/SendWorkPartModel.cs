using System.Collections.Generic;
using Xamarin.Forms;

namespace INetApp.Models
{
    //using java.util.List;

    /**
     * Class that represents a user in the presentation layer.
     */
    public class SendWorkPartModel : BindableObject
    {

        private int dedicacion { get; set; }
        private string fechaFinSemana { get; set; }
        private string fechaFirma { get; set; }
        private string fechaInicioSemana { get; set; }
        private string fechaValidado { get; set; }
        private string firmado { get; set; }
        private double horasSemana { get; set; }
        private int idParte { get; set; }
        private int idSemana { get; set; }
        private List<SendLineasDetalleModel> sendLineasDetalleModel { get; set; }
        private string nombreSemana { get; set; }
        private int perEstado { get; set; }
        private string validado { get; set; }


        public int getDedicacion()
        {
            return dedicacion;
        }

        public void setDedicacion(int dedicacion)
        {
            this.dedicacion = dedicacion;
        }

        public string getFechaFinSemana()
        {
            return fechaFinSemana;
        }

        public void setFechaFinSemana(string fechaFinSemana)
        {
            this.fechaFinSemana = fechaFinSemana;
        }

        public string getFechaFirma()
        {
            return fechaFirma;
        }

        public void setFechaFirma(string fechaFirma)
        {
            this.fechaFirma = fechaFirma;
        }

        public string getFechaInicioSemana()
        {
            return fechaInicioSemana;
        }

        public void setFechaInicioSemana(string fechaInicioSemana)
        {
            this.fechaInicioSemana = fechaInicioSemana;
        }

        public string getFechaValidado()
        {
            return fechaValidado;
        }

        public void setFechaValidado(string fechaValidado)
        {
            this.fechaValidado = fechaValidado;
        }

        public string getFirmado()
        {
            return firmado;
        }

        public void setFirmado(string firmado)
        {
            this.firmado = firmado;
        }

        public double getHorasSemana()
        {
            return horasSemana;
        }

        public void setHorasSemana(double horasSemana)
        {
            this.horasSemana = horasSemana;
        }

        public int getIdParte()
        {
            return idParte;
        }

        public void setIdParte(int idParte)
        {
            this.idParte = idParte;
        }

        public int getIdSemana()
        {
            return idSemana;
        }

        public void setIdSemana(int idSemana)
        {
            this.idSemana = idSemana;
        }

        public List<SendLineasDetalleModel> getSendLineasDetalleModel()
        {
            return sendLineasDetalleModel;
        }

        public void setSendLineasDetalleModel(List<SendLineasDetalleModel> sendLineasDetalleModel)
        {
            this.sendLineasDetalleModel = sendLineasDetalleModel;
        }

        public string getNombreSemana()
        {
            return nombreSemana;
        }

        public void setNombreSemana(string nombreSemana)
        {
            this.nombreSemana = nombreSemana;
        }

        public int getPerEstado()
        {
            return perEstado;
        }

        public void setPerEstado(int perEstado)
        {
            this.perEstado = perEstado;
        }

        public string getValidado()
        {
            return validado;
        }

        public void setValidado(string validado)
        {
            this.validado = validado;
        }
    }
}