using System.Collections.Generic;
using System.Threading.Tasks;
using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.DataAccess.Repositories.ContactRepository
{
    public interface IItemRepository
    {
        Task Add(StationEntity station);
        Task Remove(int id);
        Task RemoweAllFavorites();
        Task<List<StationEntity>> GetAllStations();
        Task<List<FavoriteEntity>> GetAllFavorites();
    }
}