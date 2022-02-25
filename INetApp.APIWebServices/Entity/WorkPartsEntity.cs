using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Entity
{
    class WorkPartsEntity
    {
    }
}
namespace com.ineco.inetapp.data.entity
{
    using System.Collections.Generic;
    using System.Text;
    //using com.google.gson.annotations.SerializedName;

    //using java.util.ArrayList;

    using com.ineco.inetapp.domain;
    using Newtonsoft.Json;

    //using com.ineco.inetapp.domain.LineasDetalle;

    /**
     * Category Entity used in the data layer.
     */
    public class WorkPartsEntity
    {

        [JsonProperty(PropertyName = "Dedicacion")]
        //[JsonProperty(PropertyName = "Dedicacion")
        private double dedicacion;

        [JsonProperty(PropertyName = "FechaFinSemana")]
        private string fechaFinSemana;

        [JsonProperty(PropertyName = "FechaFirma")]
        private string fechaFirma;

        [JsonProperty(PropertyName = "FechaInicioSemana")]
        private string fechaInicioSemana;

        [JsonProperty(PropertyName = "FechaMaximaParte")]
        private string fechaMaximaParte;

        [JsonProperty(PropertyName = "FechaMinimaParte")]
        private string fechaMinimaParte;

        [JsonProperty(PropertyName = "FechaValidado")]
        private string fechaValidado;

        [JsonProperty(PropertyName = "Firmado")]
        private string firmado;

        [JsonProperty(PropertyName = "HoraDia")]
        private HoraDiaEntity horaDia;

        [JsonProperty(PropertyName = "HorasSemana")]
        private double horasSemana;

        [JsonProperty(PropertyName = "IdParte")]
        private int idParte;

        [JsonProperty(PropertyName = "IdSemana")]
        private int idSemana;

        [JsonProperty(PropertyName = "IdSemanaAnterior")]
        private int idSemanaAnterior;

        [JsonProperty(PropertyName = "IdSemanaPosterior")]
        private int idSemanaPosterior;

        [JsonProperty(PropertyName = "LineasDetalle")]
        private List<LineasDetalleEntity> lineasDetalleEntity;

        [JsonProperty(PropertyName = "LineasDetalleIneco")]
        private List<LineasDetalleEntity> lineasDetalleInecoEntity;

        [JsonProperty(PropertyName = "NombreSemana")]
        private string nombreSemana;

        [JsonProperty(PropertyName = "PerEstado")]
        private int perEstado;

        [JsonProperty(PropertyName = "Percodigo")]
        private int percodigo;

        [JsonProperty(PropertyName = "Validado")]
        private string validado;



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


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("***** Message Options *****\n");
            stringBuilder.Append("dedicacion=" + this.getDedicacion() + "\n");
            stringBuilder.Append("name=" + this.getFechaFinSemana() + "\n");
            stringBuilder.Append("name=" + this.getFechaFirma() + "\n");
            stringBuilder.Append("name=" + this.getFechaInicioSemana() + "\n");
            stringBuilder.Append("name=" + this.getFechaValidado() + "\n");
            stringBuilder.Append("name=" + this.getFirmado() + "\n");
            stringBuilder.Append("name=" + this.getHorasSemana() + "\n");
            stringBuilder.Append("name=" + this.getIdParte() + "\n");
            stringBuilder.Append("name=" + this.getIdSemana() + "\n");
            stringBuilder.Append("name=" + this.getLineasDetalleEntity() + "\n");
            stringBuilder.Append("name=" + this.getLineasDetalleInecoEntity() + "\n");
            stringBuilder.Append("name=" + this.getNombreSemana() + "\n");
            stringBuilder.Append("name=" + this.getPerEstado() + "\n");
            stringBuilder.Append("name=" + this.getPercodigo() + "\n");
            stringBuilder.Append("name=" + this.getValidado() + "\n");
            stringBuilder.Append("*******************************");

            return stringBuilder.ToString();
        }

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