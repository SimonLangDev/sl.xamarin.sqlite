using System;
using System.IO;
using Microsoft.Data.Sqlite;
using sl.plugins.sqlite.Droid;
using sl.xamarin.sqlite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace sl.plugins.sqlite.Droid
{
    public class SQLite_Android : ISQLite
    {
        private SqliteConnection _connection;

        public string GetDatabasePath()
        {
            var databasePath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), SQLiteDatabase.DatabaseName + ".db3");
            return databasePath;
        }

        public SqliteConnection GetDatabaseConnection()
        {
            var databasePath = GetDatabasePath();
            _connection = new SqliteConnection(databasePath);
            return _connection;
        }

        public bool CloseDatabaseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;

                //Activate the garbage collector to delete unused resources
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return true;
            }
            return false;
        }

        public bool DeleteDatabase()
        {
            var databasePath = GetDatabasePath();

            try
            {
                if(_connection != null)
                    _connection.Close();

                if(File.Exists(databasePath))
                    File.Delete(databasePath);

                _connection = null;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
