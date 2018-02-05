using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.Models
{
    public class Station : StationEntity
    {
        public Station(StationEntity stationEntity)
            : base(stationEntity.Name, stationEntity.Stream)
        {
            Id = stationEntity.Id;
        }
    }

    public class Favorite : FavoriteEntity
    {
        public Favorite(FavoriteEntity stationEntity)
            : base(stationEntity.Name, stationEntity.Stream)
        {
            Id = stationEntity.Id;
        }
    }
}