using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace INetApp.Models
{
    //using java.util.List;

    //using com.ineco.inetapp.domain.HoraDia;
    

    /**
     * Class that represents a user in the presentation layer.
     */
    public class WorkPartsModel : ObservableObject
    {

        private int idParte { get; set; }

        public WorkPartsModel(int idParte)
        {
            this.idParte = idParte;
        }

        private double dedicacion { get; set; }

        private string fechaFinSemana { get; set; }

        private string fechaFirma { get; set; }

        private string fechaInicioSemana { get; set; }

        private string fechaMaximaParte;

        private string fechaMinimaParte { get; set; }

        private string fechaValidado { get; set; }

        private string firmado { get; set; }

        private HoraDia horaDia { get; set; }

        private double horasSemana { get; set; }

        private int idSemana { get; set; }

        private int idSemanaAnterior { get; set; }

        private int idSemanaPosterior;

        private List<LineasDetalle> lineasDetalle { get; set; }

        private List<LineasDetalle> lineasDetalleIneco { get; set; }

        private string nombreSemana { get; set; }

        private int perEstado { get; set; }

        private int percodigo { get; set; }

        private string validado { get; set; }

        private int pdeStatus { get; set; }

        private int prpCodigo;

        private int pdelineaIDRechazo { get; set; }

        public int getIdParte()
        {
            return idParte;
        }

        public double getDedicacion()
        {
            return dedicacion;
        }

        public void setDedicacion(double dedicacion)
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

        public int getIdSemana()
        {
            return idSemana;
        }

        public void setIdSemana(int idSemana)
        {
            this.idSemana = idSemana;
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

        public int getPercodigo()
        {
            return percodigo;
        }

        public void setPercodigo(int percodigo)
        {
            this.percodigo = percodigo;
        }

        public string getValidado()
        {
            return validado;
        }

        public void setValidado(string validado)
        {
            this.validado = validado;
        }

        public HoraDia getHoraDia()
        {
            return horaDia;
        }

        public void setHoraDia(HoraDia horaDia)
        {
            this.horaDia = horaDia;
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("IdParte=" + this.getIdParte() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }


        public List<LineasDetalle> getLineasDetalle()
        {
            return lineasDetalle;
        }

        public void setLineasDetalle(List<LineasDetalle> lineasDetalle)
        {
            this.lineasDetalle = lineasDetalle;
        }

        public List<LineasDetalle> getLineasDetalleIneco()
        {
            return lineasDetalleIneco;
        }

        public void setLineasDetalleIneco(List<LineasDetalle> lineasDetalleIneco)
        {
            this.lineasDetalleIneco = lineasDetalleIneco;
        }

        public string getFechaMaximaParte()
        {
            return fechaMaximaParte;
        }

        public void setFechaMaximaParte(string fechaMaximaParte)
        {
            this.fechaMaximaParte = fechaMaximaParte;
        }

        public string getFechaMinimaParte()
        {
            return fechaMinimaParte;
        }

        public void setFechaMinimaParte(string fechaMinimaParte)
        {
            this.fechaMinimaParte = fechaMinimaParte;
        }

        public int getIdSemanaAnterior()
        {
            return idSemanaAnterior;
        }

        public void setIdSemanaAnterior(int idSemanaAnterior)
        {
            this.idSemanaAnterior = idSemanaAnterior;
        }

        public int getIdSemanaPosterior()
        {
            return idSemanaPosterior;
        }

        public void setIdSemanaPosterior(int idSemanaPosterior)
        {
            this.idSemanaPosterior = idSemanaPosterior;
        }

        public int getPdeStatus()
        {
            return pdeStatus;
        }

        public void setPdeStatus(int pdeStatus)
        {
            this.pdeStatus = pdeStatus;
        }

        public int getPrpCodigo()
        {
            return prpCodigo;
        }

        public void setPrpCodigo(int prpCodigo)
        {
            this.prpCodigo = prpCodigo;
        }

        public int getPdelineaIDRechazo()
        {
            return pdelineaIDRechazo;
        }

        public void setPdelineaIDRechazo(int pdelineaIDRechazo)
        {
            this.pdelineaIDRechazo = pdelineaIDRechazo;
        }
    }
}
