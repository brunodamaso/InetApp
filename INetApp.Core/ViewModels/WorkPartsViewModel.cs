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
        private ObservableCollection<ItemTableProjectModel> _ItemTableProjectModel;

        private bool _HasNextWeek;
        private bool _HasPreviewWeek;
        private bool _HasCopy;
        private bool _Editable;
        private string _tv_date_2;
        private string _Dedicacion;
        private string _HorasSemana;
        private int _HeightProject;
        private int _HeightProjectGestion;

        private int periodoActivo;
        //todo combo 1
        //todo combo 2
        //todo entry solo numerico
        //todo entry selectall probar
        //todo convertir lineas en ItemTableProjectModel
        //todo convertir lineasineco en ItemTableProjectModel
        //todo totales
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
        public ObservableCollection<ItemTableProjectModel> ItemTableProject
        {
            get => _ItemTableProjectModel;
            set
            {
                _ItemTableProjectModel = value;
                RaisePropertyChanged(() => ItemTableProject);
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
                InecoProjectsDto inecoProjectsDto = await WorkPartsService.GetInecoProjectsAsync(true, null, null);
                if (inecoProjectsDto.IsOk)
                {
                }
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
                    List<ItemTableProjectModel> TableProjects= new List<ItemTableProjectModel>();
                    string pronum = "";
                    string fechaFirma = "";
                    foreach (LineasDetalle item in WorkParts.lineasDetalle)
                    {
                        DayOfWeek dia = DateTime.ParseExact(item.fechaImputacion ,"dd/MM/yyyy" , System.Globalization.CultureInfo.InvariantCulture).DayOfWeek;
                        if (pronum != item.pronumero)
                        {
                            ItemTableProjectModel TableProject = new ItemTableProjectModel();
                            pronum = item.pronumero;
                            fechaFirma = item.fechaFirma;
                            TableProject.pronumero = pronum;
                            TableProject.fechaFirma = fechaFirma;
                            TableProject.protitulo = item.protitulo;
                            TableProject.pdeStatus = item.pdeStatus;
                            TableProject.prpCodigo = item.prpCodigo;
                            TableProject.pdelineaIDRechazo = item.pdelineaIDRechazo;
                            TableProject.perParteId = item.perParteId;
                            TableProject.editable = CheckEditable(item);

                            TableProjects.Add(TableProject);
                        }
                        else
                        {
                            if (fechaFirma != item.fechaFirma)
                            {
                                ItemTableProjectModel TableProject = new ItemTableProjectModel();
                                pronum = item.pronumero;
                                fechaFirma = item.fechaFirma;
                                TableProject.pronumero = pronum;
                                TableProject.fechaFirma = fechaFirma;
                                TableProject.protitulo = item.protitulo;
                                TableProject.pdeStatus = item.pdeStatus;
                                TableProject.prpCodigo = item.prpCodigo;
                                TableProject.pdelineaIDRechazo = item.pdelineaIDRechazo;
                                TableProject.perParteId = item.perParteId;
                                TableProject.editable = CheckEditable(item);

                                TableProjects.Add(TableProject);
                            }
                        }
                        switch (dia)
                        {
                            case DayOfWeek.Monday:
                                TableProjects[TableProjects.Count - 1].procuentaLunes = item.procuenta;
                                TableProjects[TableProjects.Count - 1].pdLineaIdLunes = item.pdLineaId;
                                break;
                            case DayOfWeek.Tuesday:
                                TableProjects[TableProjects.Count - 1].procuentaMartes = item.procuenta;
                                TableProjects[TableProjects.Count - 1].pdLineaIdMartes = item.pdLineaId;
                                break;
                            case DayOfWeek.Wednesday:
                                TableProjects[TableProjects.Count - 1].procuentaMiercoles = item.procuenta;
                                TableProjects[TableProjects.Count - 1].pdLineaIdMiercoles = item.pdLineaId;
                                break;
                            case DayOfWeek.Thursday:
                                TableProjects[TableProjects.Count - 1].procuentaJueves = item.procuenta;
                                TableProjects[TableProjects.Count - 1].pdLineaIdJueves = item.pdLineaId;
                                break;
                            case DayOfWeek.Friday:
                                TableProjects[TableProjects.Count - 1].procuentaViernes = item.procuenta;
                                TableProjects[TableProjects.Count - 1].pdLineaIdViernes = item.pdLineaId;
                                break;
                            default:
                                break;
                        }
                    }
                    this.ItemTableProject = new ObservableCollection<ItemTableProjectModel>(TableProjects);
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

        private bool CheckEditable(LineasDetalle lineasDetalle)
        {
            bool editLine = false;
            if (this.Editable)
            {
                editLine = lineasDetalle.pdeStatus == 0 || (lineasDetalle.pdeStatus == 2 && lineasDetalle.prpCodigo == periodoActivo) || (lineasDetalle.pdeStatus == 3 && lineasDetalle.prpCodigo == periodoActivo && lineasDetalle.pdelineaIDRechazo == null);
            }
            return editLine;
        }
    }
}