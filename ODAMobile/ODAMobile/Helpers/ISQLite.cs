using SQLite;

namespace ODAMobile.Helpers
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
