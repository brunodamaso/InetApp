using Xamarin.Forms;

namespace INetApp.Models
{

    /**
     * Class that represents a LinesDetail in the domain layer.
     */

    public class LineasDetalle : BindableObject
    {

        public string fechaImputacion { get; set; }

        public int pdLineaId { get; set; }

        public int perParteId { get; set; }

        public double procuenta { get; set; }

        public string pronumero { get; set; }

        public string protitulo { get; set; }

        public int pdeStatus { get; set; }

        public int prpCodigo { get; set; }

        public int pdelineaIDRechazo { get; set; }

        public string fechaFirma { get; set; }

        public LineasDetalle(int perParteId)
        {

        }

        public string getFechaFirma()
        {
            return fechaFirma;
        }

        public void setFechaFirma(string fechaFirma)
        {
            this.fechaFirma = fechaFirma;
        }

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

        public string getProtitulo()
        {
            return protitulo;
        }

        public void setProtitulo(string protitulo)
        {
            this.protitulo = protitulo;
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