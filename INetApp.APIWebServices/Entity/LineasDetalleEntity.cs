using INetApp.APIWebServices.Responses;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class LineasDetalleEntity : Response
    {

        [JsonPropertyName( "FechaImputacion")]
        public string fechaImputacion { get; set; }

        [JsonPropertyName( "PdLineaId")]
        public int pdLineaId { get; set; }

        [JsonPropertyName( "PerParteId")]
        public int perParteId { get; set; }

        [JsonPropertyName( "Procuenta")]
        public double procuenta { get; set; }


        [JsonPropertyName( "Pronumero")]
        public string pronumero { get; set; }


        [JsonPropertyName( "Protitulo")]
        public string protitulo { get; set; }

        //BIGAALCA 13-05-2020
        [JsonPropertyName( "PdeStatus")]
        public int pdeStatus { get; set; }

        [JsonPropertyName( "PrpCodigo")]
        public int prpCodigo { get; set; }

        [JsonPropertyName( "PdelineaIDRechazo")]
        public int pdelineaIDRechazo { get; set; }

        [JsonPropertyName( "FechaFirma")]
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