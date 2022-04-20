using INetApp.Resources;
using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.ViewModels
{
    public class WebInecoViewModel : ViewModelBase
    {
        private string _ruta;
        private string _Title;

        #region Properties
        public string Ruta
        {
            get => _ruta;
            set
            {
                _ruta = value;
                OnPropertyChanged(nameof(Ruta));
            }
        }
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        #endregion


        public WebInecoViewModel()
        {
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            Ruta = Uri.UnescapeDataString(query["Ruta"]);
            if (query.TryGetValue("Titulo", out string titulo))
            {
                this.Title = Uri.UnescapeDataString(titulo);
            }
            else
            {
                this.Title = Literales.app_name;
            }

        }
    }
}

