using SQLite.Net;

namespace RadioPlayer.Services
{
    public interface IDatabaseService
    {
        SQLiteConnection Connection { get; }
    }
}