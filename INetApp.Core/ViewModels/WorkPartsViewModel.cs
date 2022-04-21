using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class WorkPartsViewModel : ViewModelBase
    {
        private readonly IWorkPartsService WorkPartsService;

        private WorkPartsModel _WorkPartsModel;
        private ObservableCollection<ItemTableProjectModel> _ItemTableProject;
        private ObservableCollection<ItemTableProjectModel> _ItemTableProjectIneco;

        private bool _HasNextWeek;
        private bool _HasPreviewWeek;
        private bool _HasCopy;
        private bool _Editable;
        private bool _HasDatos;
        private string _tv_date_2;
        private string _Dedicacion;
        private string _HorasSemana;
        private int _HeightProject;
        private int _HeightProjectGestion;

        private int periodoActivo;
        //todo peridoactivo
        //todo mostrar combo1
        //todo mostrar combo2
        //todo combo 1
        //todo combo 2
        //todo entry selectall probar
        //todo totales
        //todo revisar editando
        //todo firmar
        //todo grabar
        //todo eliminar
        #region Properties
        public WorkPartsModel WorkParts
        {
            get => _WorkPartsModel;
            set
            {
                _WorkPartsModel = value;
                OnPropertyChanged(nameof(WorkParts));
            }
        }
        public ObservableCollection<ItemTableProjectModel> ItemTableProject
        {
            get => _ItemTableProject;
            set
            {
                _ItemTableProject = value;
                OnPropertyChanged(nameof(ItemTableProject));
            }
        }
        public ObservableCollection<ItemTableProjectModel> ItemTableProjectIneco
        {
            get => _ItemTableProjectIneco;
            set
            {
                _ItemTableProjectIneco = value;
                OnPropertyChanged(nameof(ItemTableProjectIneco));
            }
        }
        public bool HasDatos
        {
            get => _HasDatos;
            set
            {
                _HasDatos = value;
                OnPropertyChanged(nameof(HasDatos));
            }
        }

        public bool HasNextWeek
        {
            get => _HasNextWeek;
            set
            {
                _HasNextWeek = value;
                OnPropertyChanged(nameof(HasNextWeek));
            }
        }
        public bool HasPreviewWeek
        {
            get => _HasPreviewWeek;
            set
            {
                _HasPreviewWeek = value;
                OnPropertyChanged(nameof(HasPreviewWeek));
            }
        }
        public bool HasCopy
        {
            get => _HasCopy;
            set
            {
                _HasCopy = value;
                OnPropertyChanged(nameof(HasCopy));
            }
        }
        public bool Editable
        {
            get => _Editable;
            set
            {
                _Editable = value;
                OnPropertyChanged(nameof(Editable));
            }
        }
        public string Dedicacion
        {
            get => _Dedicacion;
            set
            {
                _Dedicacion = value;
                OnPropertyChanged(nameof(Dedicacion));
            }
        }
        public string HorasSemana
        {
            get => _HorasSemana;
            set
            {
                _HorasSemana = value;
                OnPropertyChanged(nameof(HorasSemana));
            }
        }

        public string Tv_date_2
        {
            get => _tv_date_2;
            set
            {
                _tv_date_2 = value;
                OnPropertyChanged(nameof(Tv_date_2));
            }
        }

        public int HeightProject
        {
            get => _HeightProject;
            set
            {
                _HeightProject = value;
                OnPropertyChanged(nameof(HeightProject));
            }
        }
        public int HeightProjectGestion
        {
            get => _HeightProjectGestion;
            set
            {
                _HeightProjectGestion = value;
                OnPropertyChanged(nameof(HeightProjectGestion));
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
            HasDatos = false;
            WorkPartsDto workPartsDto = await WorkPartsService.GetWorkPartsAsync(FechaIni, FechaFin, IdSemana);
            if (workPartsDto.IsOk)
            {
                _WorkPartsModel = workPartsDto.WorkPartsModel;

                InecoProjectsDto inecoProjectsDto = await WorkPartsService.GetInecoProjectsAsync(true, null, null);
                if (inecoProjectsDto.IsOk)
                {
                    PeriodoActivoDto periodoActivoDto = await WorkPartsService.GetPeriodoActivoAsync();
                    if (periodoActivoDto.IsOk)
                    {
                        this.periodoActivo = periodoActivoDto.PeriodoActivoModel.periodoActivo;
                    }
                    HasPreviewWeek = _WorkPartsModel.idSemanaAnterior != 0;
                    HasNextWeek = _WorkPartsModel.idSemanaPosterior != 0;
                    Editable = _WorkPartsModel.perEstado == 0 || _WorkPartsModel.perEstado == 1 || _WorkPartsModel.perEstado == 5 || _WorkPartsModel.perEstado == 6;
                    HasCopy = HasPreviewWeek && Editable;
                    Dedicacion = string.Format(Literales.text_dedication, _WorkPartsModel.dedicacion) + " %";
                    HorasSemana = string.Format(Literales.total_week_hours, _WorkPartsModel.horasSemana) + " h";
                    string fechaIni = _WorkPartsModel.fechaInicioSemana.Replace("/", "-");
                    string fechaFin = _WorkPartsModel.fechaFinSemana.Replace("/", "-");
                    Tv_date_2 = "(" + fechaIni + " / " + fechaFin + ")";
                    HeightProject = 55;
                    HeightProjectGestion = 55;

                    if (_WorkPartsModel.lineasDetalle != null)
                    {
                        ItemTableProject = new ObservableCollection<ItemTableProjectModel>(WorkPartsService.GetItemTableProjects(_WorkPartsModel.lineasDetalle, Editable, periodoActivo));
                        HeightProject += ItemTableProject.Count * 56;
                    }
                    if (WorkParts.lineasDetalleIneco != null)
                    {
                        ItemTableProjectIneco = new ObservableCollection<ItemTableProjectModel>(WorkPartsService.GetItemTableProjects(_WorkPartsModel.lineasDetalleIneco, Editable, periodoActivo));
                        HeightProjectGestion += ItemTableProjectIneco.Count * 56;
                    }
                    WorkParts = _WorkPartsModel;
                    HasDatos = true;
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