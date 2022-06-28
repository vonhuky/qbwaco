using SQLite;

namespace ODAMobile.IService
{
    public interface ILocalDatabaseService
    {
        SQLiteConnection GetDatabaseConnection();

        SQLiteConnection GetDatabaseConnection(string dbPath);
    }
}
