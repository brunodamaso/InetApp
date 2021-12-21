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
        public async Task<List<T>> GetAll<T>(bool withChildren = true) where T : new()
        {
            return await dbService.GetAll<T>();
        }
        public async Task<List<T>> GetItemsWhere<T>(Expression<Func<T, bool>> whereClause, bool withChildren = true) where T : new()
        {
            return await dbService.GetItemsWhere<T>(whereClause);
        }

        public async Task<bool> MarkMessageFavoriteAsync(MessageModel messageModel, bool IsFavorite)
        {
            bool retorno;
            if (IsFavorite)
            {
                messageModel.favorite = IsFavorite;
                retorno = await dbService.InsertOrReplace(messageModel);
            }
            else
            {
                retorno = !await dbService.DeleteItem(messageModel);
            }

            return retorno;
        }

        #endregion
    }
}
