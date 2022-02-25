using Newtonsoft.Json;

namespace com.ineco.inetapp.data.entity
{

    //using com.google.gson.annotations.SerializedName;

    /**
     * Created by BISAMOER on 09/03/2018.
     */

    public class LineasDetalleEntity
    {

        [JsonProperty(PropertyName = "FechaImputacion")]
        private string fechaImputacion;

        [JsonProperty(PropertyName = "PdLineaId")]
        private int pdLineaId;

        [JsonProperty(PropertyName = "PerParteId")]
        private int perParteId;

        [JsonProperty(PropertyName = "Procuenta")]
        private double procuenta;


        [JsonProperty(PropertyName = "Pronumero")]
        private string pronumero;


        [JsonProperty(PropertyName = "Protitulo")]
        private string protitulo;

        //BIGAALCA 13-05-2020
        [JsonProperty(PropertyName = "PdeStatus")]
        private int pdeStatus;

        [JsonProperty(PropertyName = "PrpCodigo")]
        private int prpCodigo;

        [JsonProperty(PropertyName = "PdelineaIDRechazo")]
        private int pdelineaIDRechazo;

        [JsonProperty(PropertyName = "FechaFirma")]
        private string fechaFirma;

        public LineasDetalleEntity()
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