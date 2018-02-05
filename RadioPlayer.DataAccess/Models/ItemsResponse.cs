using System.Collections.Generic;
using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.DataAccess.Models
{
    public class StationResponse : BaseResponse
    {
        public List<StationEntity> Stations { get; set; }
    }

    public class FavoriteResponse : BaseResponse
    {
        public List<StationEntity> Favorites { get; set; }
    }
}