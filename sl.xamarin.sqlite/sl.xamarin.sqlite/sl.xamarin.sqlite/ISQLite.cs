using Microsoft.Data.Sqlite;

namespace sl.xamarin.sqlite
{
    public interface ISQLite
    {
        // ---------------------------------------------------------------------
        /// <summary>
        /// Returns the applications filepath 
        /// </summary>
        /// <returns>The file path as string</returns>
        // ---------------------------------------------------------------------
        string GetDatabasePath();

        // ---------------------------------------------------------------------
        /// <summary>
        /// Delivers synchronous access to SQLite database object 
        /// </summary>
        /// <returns>The SQLiteConnection object</returns>
        // ---------------------------------------------------------------------
        SqliteConnection GetDatabaseConnection();

        // ---------------------------------------------------------------------
        /// <summary>
        /// Determine the connection to the SQLite database object 
        /// </summary>
        /// <returns>The result as boolean</returns>
        // ---------------------------------------------------------------------
        bool CloseDatabaseConnection();

        // ---------------------------------------------------------------------
        /// <summary>
        /// Deletes the SQLite database object 
        /// </summary>
        /// <returns>The result as boolean</returns>
        // ---------------------------------------------------------------------
        bool DeleteDatabase();
    }
}
