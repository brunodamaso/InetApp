using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{ 
    /**
     * Class that represents a user in the presentation layer.
     */
    public class WorkPartsModel : BindableObject
    {

        public WorkPartsModel() { }
    
        public int idParte { get; set; }

        public WorkPartsModel(int idParte)
        {
            this.idParte = idParte;
        }

        public double dedicacion { get; set; }

        public string fechaFinSemana { get; set; }

        public string fechaFirma { get; set; }

        public string fechaInicioSemana { get; set; }

        public string fechaMaximaParte;

        public string fechaMinimaParte { get; set; }

        public string fechaValidado { get; set; }

        public string firmado { get; set; }

        public HoraDia horaDia { get; set; }

        public double horasSemana { get; set; }

        public int idSemana { get; set; }

        public int idSemanaAnterior { get; set; }

        public int idSemanaPosterior;

        public List<LineasDetalle> lineasDetalle { get; set; }

        public List<LineasDetalle> lineasDetalleIneco { get; set; }

        public string nombreSemana { get; set; }

        public int perEstado { get; set; }

        public int percodigo { get; set; }

        public string validado { get; set; }

        public int pdeStatus { get; set; }

        public int prpCodigo;

        public int pdelineaIDRechazo { get; set; }

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

        //override
        //public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("IdParte=" + this.getIdParte() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}


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
