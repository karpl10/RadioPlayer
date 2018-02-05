using Prism.Events;
using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.Events
{
    public class AddStationEvent : PubSubEvent<StationEntity>
    {
    }

    public class AddFavoriteEvent : PubSubEvent<FavoriteEntity>
    {
    }
}