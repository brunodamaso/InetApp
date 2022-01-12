using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using INetApp.Models;

namespace INetApp.Services
{
    public interface IRepositoryService
    {
        Task<T> Get<T>(object pk, bool withChildren = true) where T : new();
        Task<List<T>> GetAll<T>(bool withChildren = true) where T : new();
        Task<List<T>> GetItemsWhere<T>(Expression<Func<T, bool>> whereClause, bool withChildren = true) where T : new();
        Task<bool> DeleteItemsWhere<T>(Expression<Func<T, bool>> whereClause, bool withChildren = true) where T : new();
        Task<bool> MarkMessageFavoriteAsync(MessageModel messageModel, bool IsFavorite);

    }
}
