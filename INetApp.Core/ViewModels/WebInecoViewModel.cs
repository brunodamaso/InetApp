using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.ViewModels
{
    public class WebInecoViewModel : ViewModelBase
    {
        private string _ruta;

        #region Properties
        public string Ruta
        {
            get => _ruta;
            set
            {
                _ruta = value;
                RaisePropertyChanged(() => Ruta);
            }
        }

        #endregion


        public WebInecoViewModel()
        {
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            Ruta = Uri.UnescapeDataString(query["Ruta"]);
        }
    }
}

