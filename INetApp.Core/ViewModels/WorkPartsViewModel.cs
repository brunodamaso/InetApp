using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace INetApp.ViewModels
{
    public class WorkPartsViewModel : ViewModelBase
    {
        private WorkPartsModel _WorkPartsModel;
        private readonly IWorkPartsService WorkPartsService;
        private bool _HasNextWeek;
        private bool _HasPreviewWeek;
        private bool _HasCopy;
        private bool _Editable;
        private string _tv_date_2;
        private string _Dedicacion;
        private string _HorasSemana;
        private int _HeightProject;
        private int _HeightProjectGestion;
        #region Properties

        public WorkPartsModel WorkParts
        {
            get => _WorkPartsModel;
            set
            {
                _WorkPartsModel = value;
                RaisePropertyChanged(() => WorkParts);
            }
        }
        public bool HasNextWeek
        {
            get => _HasNextWeek;
            set
            {
                _HasNextWeek = value;
                RaisePropertyChanged(() => HasNextWeek);
            }
        }
        public bool HasPreviewWeek
        {
            get => _HasPreviewWeek;
            set
            {
                _HasPreviewWeek = value;
                RaisePropertyChanged(() => HasPreviewWeek);
            }
        }
        public bool HasCopy
        {
            get => _HasCopy;
            set
            {
                _HasCopy = value;
                RaisePropertyChanged(() => HasCopy);
            }
        }
        public bool Editable
        {
            get => _Editable;
            set
            {
                _Editable = value;
                RaisePropertyChanged(() => Editable);
            }
        }
        public string Dedicacion
        {
            get => _Dedicacion;
            set
            {
                _Dedicacion = value;
                RaisePropertyChanged(() => Dedicacion);
            }
        }
        public string HorasSemana
        {
            get => _HorasSemana;
            set
            {
                _HorasSemana = value;
                RaisePropertyChanged(() => HorasSemana);
            }
        }
        
        public string Tv_date_2
        {
            get => _tv_date_2;
            set
            {
                _tv_date_2 = value;
                RaisePropertyChanged(() => Tv_date_2);
            }
        }

        public int HeightProject
        {
            get => _HeightProject;
            set
            {
                _HeightProject = value;
                RaisePropertyChanged(() => HeightProject);
            }
        }
        public int HeightProjectGestion
        {
            get => _HeightProjectGestion;
            set
            {
                _HeightProjectGestion = value;
                RaisePropertyChanged(() => HeightProjectGestion);
            }
        }
        #endregion

        //public ICommand SelectFavoriteCommand => new Command<bool>(OnSelectFavorite);
        public ICommand GrabarCommand => new Command(OnGrabar);
        public ICommand FirmarCommand => new Command(OnFirmar);
        public ICommand CopiaSemanaAnteriorCommand => new Command(OnCopiaSemanaAnterior);
        public ICommand IrSemanaAnteriorCommand => new Command(OnIrSemanaAnterior);
        public ICommand IrSemanaSiguienteCommand => new Command(OnIrSemanaSiguiente);
        public ICommand CalendarioCommand => new Command(OnCalendario);

        public WorkPartsViewModel()
        {
            WorkPartsService = DependencyService.Get<IWorkPartsService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            await Sincroniza();
            //await base.InitializeAsync(query);
        }
        private async Task Sincroniza(string FechaIni = null, string FechaFin = null, int? IdSemana = null)
        {
            IsBusy = true;

            WorkPartsDto workPartsDto = await WorkPartsService.GetWorkPartsAsync(FechaIni, FechaFin, IdSemana);
            if (workPartsDto.IsOk)
            {
                WorkParts = workPartsDto.WorkPartsModel;
                HasPreviewWeek = WorkParts.idSemanaAnterior != 0;
                HasNextWeek = WorkParts.idSemanaPosterior != 0;
                Editable = WorkParts.perEstado == 0 || WorkParts.perEstado == 1 || WorkParts.perEstado == 5 || WorkParts.perEstado == 6;
                HasCopy = HasNextWeek && Editable;
                Dedicacion = string.Format(Literales.text_dedication, WorkParts.dedicacion) + " %";
                HorasSemana = string.Format(Literales.total_week_hours, WorkParts.horasSemana) + " h";
                string fechaIni = WorkParts.fechaInicioSemana.Replace("/", "-");
                string fechaFin = WorkParts.fechaFinSemana.Replace("/", "-");
                Tv_date_2 = "(" + fechaIni + " / " + fechaFin + ")";
                HeightProject = 45;
                if (WorkParts.lineasDetalle != null)
                {
                    HeightProject += WorkParts.lineasDetalle.Count * 44;
                }
                HeightProjectGestion = 45;
                if (WorkParts.lineasDetalleIneco != null)
                {
                    HeightProjectGestion += WorkParts.lineasDetalleIneco.Count * 44;
                }
            }

            IsBusy = false;
        }

        private async void OnCopiaSemanaAnterior()
        {
            WorkPartsDto workPartsDto = await WorkPartsService.GetWorkPartsAsync(IdSemana: WorkParts.idSemanaAnterior);
            //todo copiar
        }
        private async void OnIrSemanaAnterior()
        {
            await Sincroniza(IdSemana: WorkParts.idSemanaAnterior);
        }

        private async void OnIrSemanaSiguiente()
        {
            await Sincroniza(IdSemana: WorkParts.idSemanaPosterior);
        }
        private async void OnCalendario()
        {
        }

        private void OnGrabar()
        {
            throw new NotImplementedException();
        }


        private void OnFirmar()
        {
            throw new NotImplementedException();
        }
    }
}