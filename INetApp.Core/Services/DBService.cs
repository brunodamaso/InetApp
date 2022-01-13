using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using INetApp.Models;
using SQLite;
using Xamarin.Essentials;

namespace INetApp.Services
{
    public class DBService : IDBService
    {
        protected SQLiteAsyncConnection db;
        private static readonly string DATABASE_NAME = "data_ineco.db";
        private static readonly int DATABASE_VERSION = 1;

        public DBService()
        {
            db = GetConnection();
            CrearTablaAsync();
        }

        private async void CrearTablaAsync()
        {
            await db.CreateTableAsync<MessageModel>();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            try
            {
                string dbname = GetPath();
                return new SQLiteAsyncConnection(dbname, SQLiteOpenFlags.Create |
                        SQLiteOpenFlags.FullMutex |
                        SQLiteOpenFlags.ReadWrite);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("error  " + e.Message);
                throw;
            }

        }

        private string GetPath()
        {
            string path;
            try
            {
                string dbName = DATABASE_NAME;
                if (DeviceInfo.Platform == DevicePlatform.UWP)
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
                }
                else
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("error  " + e.Message);
                throw;
            }
            return path;
        }

        #region SQLite
        /// <summary>
        /// Get One Item where PK match
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="withChildren"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(object pk, bool withChildren = true) where T : new()
        {
            try
            {
                //if (withChildren)
                //    return db.GetWithChildren<T>(pk);
                //else
                return await db.GetAsync<T>(pk);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error  " + ex.Message);
                return default;
            }
        }

        public async Task<List<T>> GetAll<T>(bool withChildren = true) where T : new()
        {
            //if (withChildren)
            //    return db.GetAllWithChildren<T>();
            //else
            try
            {
                return await db.Table<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get All Data From Table T filtered by clause
        /// For Example (o => o.Id == projectId)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereClause"></param>
        /// <param name="withChildren"></param>
        /// <returns></returns>
        public async Task<List<T>> GetItemsWhere<T>(Expression<Func<T, bool>> whereClause, bool withChildren = true) where T : new()
        {
            //if (withChildren)
            //    return db.GetAllWithChildren<T>(whereClause);
            //else
            return await db.Table<T>().Where(whereClause).ToListAsync();
        }

        public async Task<T> GetItemWhere<T>(Expression<Func<T, bool>> whereClause) where T : new()
        {
            T datos = await db.Table<T>().Where(whereClause).FirstOrDefaultAsync();//.ToListAsync();
            return datos;
        }

        /// <summary>
        /// Get One Item where Id match
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="withChildren"></param>
        /// <returns></returns>
        public async Task<T> GetItemWhereId<T>(int id, bool withChildren = true) where T : new()
        {
            //if (withChildren)
            //    return db.GetWithChildren<T>(id);
            //else
            T datos = await db.GetAsync<T>(id);
            return datos;
        }

        public async Task<List<T>> GetQuery<T>(string Query) where T : new()
        {
            try
            {
                //if (withChildren)
                //    return db.GetWithChildren<T>(pk);
                //else
                List<T> datos = await db.QueryAsync<T>(Query);
                return datos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error  " + ex.Message);
                return default;
            }
        }
        #endregion

        #region ADD
        /// <summary>
        /// Add Item To Table T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">Object T</param>
        /// <param name="withChildren"></param>
        public async Task<bool> AddItem<T>(T item, bool withChildren = true)
        {
            try
            {
                //if (withChildren)
                //    db.InsertWithChildren(item);
                //else
                return await db.InsertAsync(item) == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("error  " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Add List of items to Table T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="withChildren"></param>
        public async Task<bool> AddAll<T>(IEnumerable<T> items, bool withChildren = true)
        {
            //if (withChildren)
            //    db.InsertAllWithChildren(items);
            //else
            return await db.InsertAllAsync(items) > 0;
        }

        /// <summary>
        /// Add Item To Table T or replace if exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="withChildren"></param>
        public async Task<bool> InsertOrReplace<T>(T item, bool withChildren = true)
        {
            //if (withChildren)
            //    db.InsertOrReplaceWithChildren(item);
            //else
            return await db.InsertOrReplaceAsync(item) >= 0;
        }

        /// <summary>
        /// Add List of Items To Table T or replace if exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="withChildren"></param>
        public async Task<bool> InsertOrReplaceAll<T>(IEnumerable<T> items, bool withChildren = true)
        {
            //if (withChildren)
            //    db.InsertOrReplaceAllWithChildren(items);
            //else
            //{
            try
            {
                foreach (T item in items)
                {
                    await db.InsertOrReplaceAsync(item);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
            //}
        }

        #endregion

        #region DELETE

        public async Task<bool> DeleteItem<T>(T item)
        {
            return await db.DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteItem<T>(object pk) where T : new()
        {
            bool retorno = true;
            T item = await db.GetAsync<T>(pk);

            if (item != null)
            {
                retorno = await db.DeleteAsync(item) > 0;
            }
            return retorno;
        }

        public async Task<bool> DeleteItemWhere<T>(Expression<Func<T, bool>> whereClause) where T : new()
        {
            bool retorno = true;
            List<T> datos = await db.Table<T>().Where(whereClause).ToListAsync();
            foreach (var item in datos)
            {
                retorno = (await db.DeleteAsync(item) > 0) && retorno;
            }
            return retorno;
        }
            
        //public async Task<int> DeleteItemWhereId<T>(int id)
        //{
        //    int deleted = await db.DeleteAsync<T>(id);
        //    return deleted;
        //}

        public async Task<bool> DeleteAll<T>()
        {
            return await db.DeleteAllAsync<T>() >= 0;
        }

        #endregion


        #region private

        #endregion
    }
}
