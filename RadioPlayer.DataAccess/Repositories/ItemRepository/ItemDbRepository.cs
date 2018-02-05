using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadioPlayer.DataAccess.Entities;
using RadioPlayer.DataAccess.Repositories.Base;
using SQLite.Net;

namespace RadioPlayer.DataAccess.Repositories.ItemRepository
{
    public class ItemDbRepository : BaseDbRepository, IItemRepository
    {
        public ItemDbRepository(SQLiteConnection connection)
            : base(connection)
        {
            if (!DbConnection.Table<StationEntity>().Any()) AddFirstStations();
            //if (!DbConnection.Table<FavoriteEntity>().Any()) AddFirstFavorites();

        }

        public Task Add<T>(T station)
        {
            return Task.Run(() =>
            {
                lock (DatabaseLock)
                {
                    DbConnection.InsertOrReplace(station);
                }
            });
        }

        public Task AddToFavorites(StationEntity station)
        {
            if (station.FavoriteSource == "ic_favorite_border.png")
            {
                var stationFavorite = new FavoriteEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = "ic_favorite.png",
                    PlayIcon = station.PlayIcon
                };
                var stationChange = new StationEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = "ic_favorite.png",
                    PlayIcon = station.PlayIcon
                };
                return Task.Run(() =>
                {
                    lock (DatabaseLock)
                    {
                        DbConnection.InsertOrReplace(stationFavorite);
                        DbConnection.InsertOrReplace(stationChange);
                    }
                });
            }
            else
            {
                var stationChange = new StationEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = "ic_favorite_border.png",
                    PlayIcon = station.PlayIcon
                };
                return Task.Run(() =>
                {
                    lock (DatabaseLock)
                    {
                        DbConnection.InsertOrReplace(stationChange);
                        DbConnection.Delete<FavoriteEntity>(station.Id);
                    }
                });
            }

        }

        public Task RemoveFavorite(FavoriteEntity favorite)
        {
            var stationChange = new StationEntity()
            {
                Id = favorite.Id,
                Name = favorite.Name,
                Stream = favorite.Stream,
                FavoriteSource = "ic_favorite_border.png",
                PlayIcon = favorite.PlayIcon
            };
            return Task.Run(() =>
            {
                lock (DatabaseLock)
                {
                    DbConnection.InsertOrReplace(stationChange);
                    DbConnection.Delete<FavoriteEntity>(favorite.Id);
                }
            });
        }

        public Task RemoveStation(int id)
        {
            return Task.Run(() =>
            {
                lock (DatabaseLock)
                {
                    DbConnection.Delete<StationEntity>(id);
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
            List<StationEntity> stations;

            lock (DatabaseLock)
            {
                stations = DbConnection.Table<StationEntity>().ToList();
            }

            return Task.FromResult(stations);
        }

        public Task<List<FavoriteEntity>> GetAllFavorites()
        {
            List<FavoriteEntity> favorites;

            lock (DatabaseLock)
            {
                favorites = DbConnection.Table<FavoriteEntity>().ToList();
            }

            return Task.FromResult(favorites);
        }

        public Task ChangeIconStation(StationEntity station)
        {
            if (station.PlayIcon=="ic_play.png")
            {
                var stationChange = new StationEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_pause.png"
                };
                var favoriteChange = new FavoriteEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_pause.png"
                };
                return Task.Run(() =>
                {
                    lock (DatabaseLock)
                    {
                        DbConnection.InsertOrReplace(stationChange);
                        DbConnection.InsertOrReplace(favoriteChange);
                    }
                });
            }
            else
            {
                var stationChange = new StationEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_play.png"
                };
                var favoriteChange = new FavoriteEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_play.png"
                };
                return Task.Run(() =>
                {
                    lock (DatabaseLock)
                    {
                        DbConnection.InsertOrReplace(stationChange);
                        DbConnection.InsertOrReplace(favoriteChange);
                    }
                });
            }
        }

        public Task ChangeIconStation(FavoriteEntity station)
        {
            if (station.PlayIcon == "ic_play.png")
            {
                var stationChange = new StationEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_pause.png"
                };
                var favoriteChange = new FavoriteEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_pause.png"
                };
                return Task.Run(() =>
                {
                    lock (DatabaseLock)
                    {
                        DbConnection.InsertOrReplace(stationChange);
                        DbConnection.InsertOrReplace(favoriteChange);
                    }
                });
            }
            else
            {
                var stationChange = new StationEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_play.png"
                };
                var favoriteChange = new FavoriteEntity()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Stream = station.Stream,
                    FavoriteSource = station.FavoriteSource,
                    PlayIcon = "ic_play.png"
                };
                return Task.Run(() =>
                {
                    lock (DatabaseLock)
                    {
                        DbConnection.InsertOrReplace(stationChange);
                        DbConnection.InsertOrReplace(favoriteChange);
                    }
                });
            }
        }

        private void AddFirstStations()
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
                new StationEntity {Name = "RMF Studencka impreza", Stream = "http://31.192.216.7:8000/rmf_studencka_impreza"},
                new StationEntity {Name = "RMF Chillout", Stream = "http://195.150.20.5:8000/rmf_chillout"},
                new StationEntity {Name = "RMF Groove", Stream = "http://217.74.72.11:8000/rmf_groove"}
            };

            foreach (var radio in table) DbConnection.Insert(radio);
        }
    }
}