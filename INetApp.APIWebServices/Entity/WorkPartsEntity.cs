using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace INetApp.APIWebServices.Entity
{
    public class WorkPartsEntity : Response
    {
        [JsonProperty(PropertyName = "Dedicacion")]
        public double dedicacion{ get; set; }

        [JsonProperty(PropertyName = "FechaFinSemana")]
        public string fechaFinSemana { get; set; }

        [JsonProperty(PropertyName = "FechaFirma")]
        public string fechaFirma { get; set; }

        [JsonProperty(PropertyName = "FechaInicioSemana")]
        public string fechaInicioSemana { get; set; }

        [JsonProperty(PropertyName = "FechaMaximaParte")]
        public string fechaMaximaParte { get; set; }

        [JsonProperty(PropertyName = "FechaMinimaParte")]
        public string fechaMinimaParte { get; set; }

        [JsonProperty(PropertyName = "FechaValidado")]
        public string fechaValidado { get; set; }

        [JsonProperty(PropertyName = "Firmado")]
        public string firmado { get; set; }

        [JsonProperty(PropertyName = "HoraDia")]
        public HoraDiaEntity horaDia { get; set; }

        [JsonProperty(PropertyName = "HorasSemana")]
        public double horasSemana { get; set; }

        [JsonProperty(PropertyName = "IdParte")]
        public int idParte { get; set; }

        [JsonProperty(PropertyName = "IdSemana")]
        public int idSemana { get; set; }

        [JsonProperty(PropertyName = "IdSemanaAnterior")]
        public int idSemanaAnterior { get; set; }

        [JsonProperty(PropertyName = "IdSemanaPosterior")]
        public int idSemanaPosterior { get; set; }

        [JsonProperty(PropertyName = "LineasDetalle")]
        public List<LineasDetalleEntity> lineasDetalleEntity { get; set; }

        [JsonProperty(PropertyName = "LineasDetalleIneco")]
        public List<LineasDetalleEntity> lineasDetalleInecoEntity { get; set; }

        [JsonProperty(PropertyName = "NombreSemana")]
        public string nombreSemana { get; set; }

        [JsonProperty(PropertyName = "PerEstado")]
        public int perEstado { get; set; }

        [JsonProperty(PropertyName = "Percodigo")]
        public int percodigo { get; set; }

        [JsonProperty(PropertyName = "Validado")]
        public string validado { get; set; }



        public WorkPartsEntity()
        {
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

        public HoraDiaEntity getHoraDia()
        {
            return horaDia;
        }

        public void setHoraDia(HoraDiaEntity horaDia)
        {
            this.horaDia = horaDia;
        }


        //public override string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** Message Options *****\n");
        //    stringBuilder.Append("dedicacion=" + this.getDedicacion() + "\n");
        //    stringBuilder.Append("name=" + this.getFechaFinSemana() + "\n");
        //    stringBuilder.Append("name=" + this.getFechaFirma() + "\n");
        //    stringBuilder.Append("name=" + this.getFechaInicioSemana() + "\n");
        //    stringBuilder.Append("name=" + this.getFechaValidado() + "\n");
        //    stringBuilder.Append("name=" + this.getFirmado() + "\n");
        //    stringBuilder.Append("name=" + this.getHorasSemana() + "\n");
        //    stringBuilder.Append("name=" + this.getIdParte() + "\n");
        //    stringBuilder.Append("name=" + this.getIdSemana() + "\n");
        //    stringBuilder.Append("name=" + this.getLineasDetalleEntity() + "\n");
        //    stringBuilder.Append("name=" + this.getLineasDetalleInecoEntity() + "\n");
        //    stringBuilder.Append("name=" + this.getNombreSemana() + "\n");
        //    stringBuilder.Append("name=" + this.getPerEstado() + "\n");
        //    stringBuilder.Append("name=" + this.getPercodigo() + "\n");
        //    stringBuilder.Append("name=" + this.getValidado() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}

        public List<LineasDetalleEntity> getLineasDetalleEntity()
        {
            return lineasDetalleEntity;
        }

        public void setLineasDetalleEntity(List<LineasDetalleEntity> lineasDetalleEntity)
        {
            this.lineasDetalleEntity = lineasDetalleEntity;
        }

        public List<LineasDetalleEntity> getLineasDetalleInecoEntity()
        {
            return lineasDetalleInecoEntity;
        }

        public void setLineasDetalleInecoEntity(List<LineasDetalleEntity> lineasDetalleInecoEntity)
        {
            this.lineasDetalleInecoEntity = lineasDetalleInecoEntity;
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

    }
}