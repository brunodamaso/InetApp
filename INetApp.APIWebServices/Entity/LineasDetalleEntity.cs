using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Entity
{
    public class LineasDetalleEntity : Response
    {

        [JsonProperty(PropertyName = "FechaImputacion")]
        public string fechaImputacion { get; set; }

        [JsonProperty(PropertyName = "PdLineaId")]
        public int pdLineaId { get; set; }

        [JsonProperty(PropertyName = "PerParteId")]
        public int perParteId { get; set; }

        [JsonProperty(PropertyName = "Procuenta")]
        public double procuenta { get; set; }


        [JsonProperty(PropertyName = "Pronumero")]
        public string pronumero { get; set; }


        [JsonProperty(PropertyName = "Protitulo")]
        public string protitulo { get; set; }

        //BIGAALCA 13-05-2020
        [JsonProperty(PropertyName = "PdeStatus")]
        public int pdeStatus { get; set; }

        [JsonProperty(PropertyName = "PrpCodigo")]
        public int prpCodigo { get; set; }

        [JsonProperty(PropertyName = "PdelineaIDRechazo")]
        public int pdelineaIDRechazo { get; set; }

        [JsonProperty(PropertyName = "FechaFirma")]
        public string fechaFirma { get; set; }

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