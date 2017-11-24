using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace sl.xamarin.sqlite
{
    public sealed class DatabaseContext : DbContext
    {
        //Filepath of database
        private readonly string _databasePath;

        /// <summary>
        /// Constructor of database context
        /// </summary>
        /// <param name="databasePath">Filepath of the database</param>
        public DatabaseContext(string databasePath)
        {
            this._databasePath = databasePath;
        }

        public void Add<T>() where T : BaseModel
        {
            this.Set<T>();
        }

        public async Task<bool> EnsureCreated()
        {
            return await Database.EnsureCreatedAsync();
        }

        public async Task<bool> EnsureDeleted()
        {
            return await Database.EnsureDeletedAsync();
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }

        /// <summary>
        /// Configure filepath of database
        /// </summary>
        /// <param name="optionsBuilder">OptionsBuilder of database context</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("Filename={0}", _databasePath));
        }
    }
}
