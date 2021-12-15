using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace INetApp.Services.Interfaces
{
    public interface IDBService
    {
        SQLiteAsyncConnection GetConnection();

        #region SQLite
        Task<T> Get<T>(object pk, bool withChildren = true) where T : new();
        Task<List<T>> GetAll<T>(bool withChildren = true) where T : new();
        Task<List<T>> GetItemsWhere<T>(Expression<Func<T, bool>> whereClause, bool withChildren = true) where T : new();

        Task<T> GetItemWhere<T>(Expression<Func<T, bool>> whereClause) where T : new();
        Task<T> GetItemWhereId<T>(int id, bool withChildren = true) where T : new();
        Task<List<T>> GetQuery<T>(string Query) where T : new();

        Task<bool> AddItem<T>(T item, bool withChildren = true);
        Task<bool> AddAll<T>(IEnumerable<T> items, bool withChildren = true);
        Task<bool> InsertOrReplace<T>(T item, bool withChildren = true);
        Task<bool> InsertOrReplaceAll<T>(IEnumerable<T> items, bool withChildren = true);

        Task<bool> DeleteItem<T>(T item);
        Task<bool> DeleteItem<T>(object pk) where T : new();
        //Task<bool> DeleteItemWhereId<T>(int id);
        Task<bool> DeleteAll<T>();

        #endregion
    }
}