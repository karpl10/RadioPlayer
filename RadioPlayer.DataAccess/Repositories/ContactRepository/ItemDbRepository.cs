using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadioPlayer.DataAccess.Entities;
using RadioPlayer.DataAccess.Repositories.Base;
using SQLite.Net;

namespace RadioPlayer.DataAccess.Repositories.ContactRepository
{
    public class ItemDbRepository : BaseDbRepository, IItemRepository
    {
        public ItemDbRepository(SQLiteConnection connection)
            : base(connection)
        {
            if (!DbConnection.Table<StationEntity>().Any()) AddFirstStations();
        }

        public Task Add(StationEntity station)
        {
            return Task.Run(() =>
            {
                lock (DatabaseLock)
                {
                    DbConnection.Insert(station);
                }
            });
        }

        public Task RemoweAllFavorites()
        {
            return Task.Run(() =>
            {
                lock (DatabaseLock)
                {
                    DbConnection.DeleteAll<FavoriteEntity>();
                }
            });
        }

        public Task<List<StationEntity>> GetAllStations()
        {
            List<StationEntity> contacts;

            lock (DatabaseLock)
            {
                contacts = DbConnection.Table<StationEntity>().ToList();
            }

            return Task.FromResult(contacts);
        }

        public Task<List<FavoriteEntity>> GetAllFavorites()
        {
            List<FavoriteEntity> contacts;

            lock (DatabaseLock)
            {
                contacts = DbConnection.Table<FavoriteEntity>().ToList();
            }

            return Task.FromResult(contacts);
        }

        public Task Remove(int id)
        {
            return Task.Run(() =>
            {
                lock (DatabaseLock)
                {
                    DbConnection.Delete<StationEntity>(id);
                }
            });
        }

        public void AddFirstStations()
        {
            var table = new List<StationEntity>
            {
                new StationEntity {Name = "RMF FM", Stream = "http://217.74.72.11:8000/rmf_fm"},
                new StationEntity {Name = "RMF MAXXX", Stream = "http://217.74.72.11:8000/rmf_maxxx"},
                new StationEntity {Name = "RMF Classic", Stream = "http://217.74.72.11:8000/rmf_classic"},
                new StationEntity {Name = "Best of RMFON", Stream = "http://31.192.216.7:8000/best_of_rmfon"},
                new StationEntity {Name = "RMF Club", Stream = "http://195.150.20.8:8000/rmf_club"},
                new StationEntity {Name = "RMF Dance", Stream = "http://31.192.216.5:8000/rmf_dance"},
                new StationEntity {Name = "RMF Reggae", Stream = "http://195.150.20.243:8000/rmf_reggae"},
                new StationEntity
                {
                    Name = "RMF Studencka impreza",
                    Stream = "http://31.192.216.7:8000/rmf_studencka_impreza"
                },
                new StationEntity {Name = "RMF Chillout", Stream = "http://195.150.20.5:8000/rmf_chillout"},
                new StationEntity {Name = "RMF Groove", Stream = "http://217.74.72.11:8000/rmf_groove"}
            };

            foreach (var radio in table) DbConnection.Insert(radio);
        }
    }
}