using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace INetApp.Models
{
    public class MessageDetails : BindableObject
    {
        public IList<Cabecera> Cabecera { get; set; }
        public IList<Datos> Datos { get; set; }
        public IList<Detail> Details { get; set; }
    }


    public class Detail
    {
        public string Nombre { get; set; }
        public string Campo { get; set; }
        public bool IsURL{ get; set; }

    }
    public class Cabecera
    {
        public string Nombre { get; set; }
    }
    public class Datos
    {
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }
        public string Campo4 { get; set; }
        public string Campo5 { get; set; }
        public string Campo6 { get; set; }
        public string Campo7 { get; set; }
        public string Campo8 { get; set; }
        public string Campo9 { get; set; }
        public string Campo10 { get; set; }
    }
}
//"{\\\"Cabecera\\\":[{\\\"Nombre\\\":\\\"Empleado\\\"},{\\\"Nombre\\\":\\\"Proyecto\\\"},{\\\"Nombre\\\":\\\"Fecha semana\\\"},{\\\"Nombre\\\":\\\"Horas\\\"}],\\\"Datos\\\":[{\\\"Campo1\\\":\\\"Urruticoechea Olloquiegui, Pablo\\\",\\\"Campo2\\\":\\\"CNRC - 194572. CYA REDACCIÓN PC FASE 2 (ESTACIÓN PASANTE, MARQUESINA HISTÓRICA Y EDIF. SERVICIOS) Y DE PB Y DE CONSTRUCCIÓN PARA DESARROLLAR POR COMPLETO EL ESTUDIO INFORMATIVO DEL NUEVO COMPLEJO FERROVIARIO DE ATOCHA. PROYECTO DE CONSTRUCCIÓN DE LA ESTACIÓN PASANTE    \\\",\\\"Campo3\\\":\\\"15\\/11\\/2021 - 19\\/11\\/2021\\\",\\\"Campo4\\\":\\\"9.00 h\\\"}]}\"}"