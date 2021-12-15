using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using INetApp.Services.Interfaces;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public partial class RepositoryService : IRepositoryService
    {
        #region Variables


        //protected readonly AppNavigationService Navigation;
        //protected readonly AppSettingsService settings;
        protected readonly IDBService dbService;
        //protected readonly ConnectivityService connectivityService;
        //protected readonly IDeviceService deviceService;
        //protected readonly IRepositoryWebService repositoryWebService;

        #endregion

        public RepositoryService(IDBService dbService)
            //AppSettingsService settings,
            //AppNavigationService navigation,
            //                    IDBService dbService,
            //                    ConnectivityService connectivityService,
            //                    IDeviceService deviceService,
            //                    IRepositoryWebService repositoryWebService)
        {
            //this.Navigation = navigation;
            //this.settings = settings;
            this.dbService = dbService;
            //this.connectivityService = connectivityService;
            //this.deviceService = deviceService;
            //this.repositoryWebService = repositoryWebService;

        }

        #region Generico
        
        public async Task<T> Get<T>(object codigo, bool withChildren = true) where T : new()
        {
            return await dbService.Get<T>(codigo);
        }
        
        #endregion
    }
}
