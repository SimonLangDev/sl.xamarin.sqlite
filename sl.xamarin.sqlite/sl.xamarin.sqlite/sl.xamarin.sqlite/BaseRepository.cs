using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace sl.xamarin.sqlite
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        //Database context
        protected readonly DatabaseContext DatabaseContext;

        /// <summary>
        /// Base constructor of the database Repositories.
        /// Controls the access of the different database tables.
        /// </summary>
        /// <param name="databasePath">Filepath of the database file</param>
        protected BaseRepository(string databasePath)
        {
            this.DatabaseContext = new DatabaseContext(databasePath);
        }

        /// <summary>
        /// Get all objects of the table.
        /// </summary>
        /// <returns>IEnumerable of the table objects</returns>
        public async Task<List<T>> GetObjectsAsync()
        {
            try
            {
                var models = await DatabaseContext.Set<T>().ToListAsync();
                return models;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Get specific object by id.
        /// </summary>
        /// <param name="id">Id of the object as integer</param>
        /// <returns>Object of the table</returns>
        public async Task<T> GetObjectByIdAsync(int id)
        {
            try
            {
                var model = await DatabaseContext.Set<T>().FindAsync(id);
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Add new object to the table.
        /// </summary>
        /// <param name="t">Object of the table</param>
        /// <returns>Result of successful insert as boolean</returns>
        public async Task<bool> AddObjectAsync(T t)
        {
            try
            {
                var state = await DatabaseContext.Set<T>().AddAsync(t);
                await DatabaseContext.SaveChangesAsync();
                var result = state.State == EntityState.Added;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Update specific object of the table.
        /// </summary>
        /// <param name="t">Object of the table</param>
        /// <returns>Result of successful update as boolean</returns>
        public async Task<bool> UpdateObjectAsync(T t)
        {
            try
            {
                var state = DatabaseContext.Update(t);
                await DatabaseContext.SaveChangesAsync();
                var result = state.State == EntityState.Modified;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Remove specific object of the table.
        /// </summary>
        /// <param name="id">Id of the object</param>
        /// <returns></returns>
        public async Task<bool> RemoveObjectAsync(int id)
        {
            try
            {
                var model = DatabaseContext.Set<T>().FindAsync(id);
                var state = DatabaseContext.Remove(model);
                await DatabaseContext.SaveChangesAsync();
                var result = state.State == EntityState.Deleted;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> QueryObjectAsync(Func<T, bool> predicate)
        {
            try
            {
                var model = DatabaseContext.Set<T>().Where(predicate).First();
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryObjectsAsync(Func<T, bool> predicate)
        {
            try
            {
                var models = DatabaseContext.Set<T>().Where(predicate);
                return models;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
