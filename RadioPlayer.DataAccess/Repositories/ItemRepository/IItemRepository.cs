using System.Collections.Generic;
using System.Threading.Tasks;
using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.DataAccess.Repositories.ItemRepository
{
    public interface IItemRepository
    {
        Task Add<T>(T station);
        Task AddToFavorites(StationEntity station);
        Task RemoveFavorite(FavoriteEntity favorite);
        Task RemoveStation(int id);
        Task RemoweAllFavorites();
        Task<List<StationEntity>> GetAllStations();
        Task<List<FavoriteEntity>> GetAllFavorites();
        Task ChangeIconStation(StationEntity station);
        Task ChangeIconStation(FavoriteEntity station);
    }
}