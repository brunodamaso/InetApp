using Xamarin.Forms;

namespace INetApp.Models
{
    /**
     * Created by BIZAALOI on 14/03/2018.
     */

    public class ItemTableProjectModel : BindableObject
    {
        public string protitulo { get; set; }
        public string pronumero { get; set; }
        public string fechaFirma { get; set; }
        public int pdeStatus { get; set; }
        public int prpCodigo { get; set; }
        public int pdelineaIDRechazo { get; set; }
        public int pdLineaIdLunes { get; set; }
        public int pdLineaIdMartes { get; set; }
        public int pdLineaIdMiercoles { get; set; }
        public int pdLineaIdJueves { get; set; }
        public int pdLineaIdViernes { get; set; }
        public int perParteId { get; set; }
        public bool editable { get; set; }
        public double procuentaLunes { get; set; }
        public double procuentaMartes { get; set; }
        public double procuentaMiercoles { get; set; }
        public double procuentaJueves { get; set; }
        public double procuentaViernes { get; set; }

        public string getProtitulo()
        {
            return protitulo;
        }

        public void setProtitulo(string protitulo)
        {
            this.protitulo = protitulo;
        }

        public string getPronumero()
        {
            return pronumero;
        }

        public void setPronumero(string pronumero)
        {
            this.pronumero = pronumero;
        }

        public string getFechaFirma()
        {
            return fechaFirma;
        }

        public void setFechaFirma(string fechaFirma)
        {
            this.fechaFirma = fechaFirma;
        }

        public double getProcuenta(int diaSemana)
        {
            switch (diaSemana)
            {
                case 2:
                    return procuentaLunes;
                case 3:
                    return procuentaMartes;
                case 4:
                    return procuentaMiercoles;
                case 5:
                    return procuentaJueves;
                case 6:
                    return procuentaViernes;
                default:
                    return 0;
            }
        }
        public int getPdlineaid(int diaSemana)
        {
            switch (diaSemana)
            {
                case 2:
                    return pdLineaIdLunes;
                case 3:
                    return pdLineaIdMartes;
                case 4:
                    return pdLineaIdMiercoles;
                case 5:
                    return pdLineaIdJueves;
                case 6:
                    return pdLineaIdViernes;
                default:
                    return 0;
            }
        }

        public double getProcuentaLunes()
        {
            return procuentaLunes;
        }

        public void setProcuentaLunes(double procuentaLunes)
        {
            this.procuentaLunes = procuentaLunes;
        }

        public double getProcuentaMartes()
        {
            return procuentaMartes;
        }

        public void setProcuentaMartes(double procuentaMartes)
        {
            this.procuentaMartes = procuentaMartes;
        }

        public double getProcuentaMiercoles()
        {
            return procuentaMiercoles;
        }

        public void setProcuentaMiercoles(double procuentaMiercoles)
        {
            this.procuentaMiercoles = procuentaMiercoles;
        }

        public double getProcuentaJueves()
        {
            return procuentaJueves;
        }

        public void setProcuentaJueves(double procuentaJueves)
        {
            this.procuentaJueves = procuentaJueves;
        }

        public double getProcuentaViernes()
        {
            return procuentaViernes;
        }

        public void setProcuentaViernes(double procuentaViernes)
        {
            this.procuentaViernes = procuentaViernes;
        }

        public bool getEditable()
        {
            return editable;
        }

        public void setEditable(bool editable)
        {
            this.editable = editable;
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

        public int getPdlineaIdLunes()
        {
            return pdLineaIdLunes;
        }

        public void setPdlineaIdLunes(int pdLineaIdLunes)
        {
            this.pdLineaIdLunes = pdLineaIdLunes;
        }

        public int getPdlineaIdMartes()
        {
            return pdLineaIdMartes;
        }

        public void setPdlineaIdMartes(int pdLineaIdMartes)
        {
            this.pdLineaIdMartes = pdLineaIdMartes;
        }

        public int getPdlineaIdMiercoles()
        {
            return pdLineaIdMiercoles;
        }

        public void setPdlineaIdMiercoles(int pdLineaIdMiercoles)
        {
            this.pdLineaIdMiercoles = pdLineaIdMiercoles;
        }

        public int getPdlineaIdJueves()
        {
            return pdLineaIdJueves;
        }

        public void setPdlineaIdJueves(int pdLineaIdJueves)
        {
            this.pdLineaIdJueves = pdLineaIdJueves;
        }

        public int getPdlineaIdViernes()
        {
            return pdLineaIdViernes;
        }

        public void setPdlineaIdViernes(int pdLineaIdViernes)
        {
            this.pdLineaIdViernes = pdLineaIdViernes;
        }

        public int getPerParteId()
        {
            return perParteId;
        }

        public void setPerParteId(int perParteId)
        {
            this.perParteId = perParteId;
        }
    }
}
