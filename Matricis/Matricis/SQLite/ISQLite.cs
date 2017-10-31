using SQLite;

namespace XamarinForms.SQLite.SQLite {
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
