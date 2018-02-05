using SQLite.Net;

namespace RadioPlayer.DataAccess.Repositories.Base
{
    public class BaseDbRepository
    {
        protected static readonly object DatabaseLock = new object();

        public BaseDbRepository(SQLiteConnection connection)
        {
            DbConnection = connection;
        }

        protected SQLiteConnection DbConnection { get; set; }
    }
}