using Xamarin.Forms;

namespace sl.xamarin.sqlite
{
    public static class SQLiteDatabase
    {
        public static string DatabaseName { get; private set;}
        //public static TableRepository Table { get; private set; }                     <----- Add your specific tables of the database

        static SQLiteDatabase()
        {
            //DatabaseName = "DatabaseName";                                            <----- Add your database name

            //Dynamic filepath of the database
            var databasePath = DependencyService.Get<ISQLite>().GetDatabasePath();

            //Table = new TableRepository(databasePath);                                <----- Add your specific tables of the database
        }
    }
}
