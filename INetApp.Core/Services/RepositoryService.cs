using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using INetApp.Models;

namespace INetApp.Services
{
    public partial class RepositoryService : IRepositoryService
    {
        #region Variables

        protected readonly IDBService dbService;

        #endregion

        public RepositoryService(IDBService dbService)
        {
            this.dbService = dbService;
        }

        #region Generico
        
        public async Task<T> Get<T>(object codigo, bool withChildren = true) where T : new()
        {
            return await dbService.Get<T>(codigo);
        }

        public async Task<bool> MarkMessageFavoriteAsync(int categoryId, int messageId, bool IsFavorite)
        {
            bool retorno = IsFavorite;
            //todo si es true IsFavorite lo debo insertarORupdate, marcar isfavorite, si no puedo retorno false
            //todo si es false IsFavorite lo debo borrar 
            if (await Get<MessageModel>(messageId) is MessageModel messageModel)
            {
                // lo encontre y debo cambiar favorite
            }
            else
            {
                //no lo encontre insert si isfavorite es false
            }

            return retorno;
        }

        #endregion
    }
}
