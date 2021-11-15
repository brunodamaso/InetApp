using GalaSoft.MvvmLight;

namespace INetApp.Models
{

/**
 * Class that represents a HourDay in the domain layer.
 */

    public class HoraDia : ObservableObject
    {

        private double lunes;
        private double martes;
        private double miercoles;
        private double jueves;
        private double viernes;


        public double getLunes()
        {
            return lunes;
        }

        public void setLunes(double lunes)
        {
            this.lunes = lunes;
        }

        public double getMartes()
        {
            return martes;
        }

        public void setMartes(double martes)
        {
            this.martes = martes;
        }

        public double getMiercoles()
        {
            return miercoles;
        }

        public void setMiercoles(double miercoles)
        {
            this.miercoles = miercoles;
        }

        public double getJueves()
        {
            return jueves;
        }

        public void setJueves(double jueves)
        {
            this.jueves = jueves;
        }

        public double getViernes()
        {
            return viernes;
        }

        public void setViernes(double viernes)
        {
            this.viernes = viernes;
        }
    }
}