using SQLite.Net.Attributes;

namespace RadioPlayer.DataAccess.Entities
{
    public class StationEntity
    {
        public StationEntity()
        {
            FavoriteSource = "ic_favorite_border.png";
            PlayIcon = "ic_play.png";
        }

        public StationEntity(string name, string stream, string favoriteSource, string playIcon)
        {
            Name = name;
            Stream = stream;
            FavoriteSource = favoriteSource;
            PlayIcon = playIcon;
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Stream { get; set; }
        public string FavoriteSource { get; set; }
        public string PlayIcon { get; set; }
    }

    public class FavoriteEntity
    {
        public FavoriteEntity()
        {
            FavoriteSource = "ic_favorite.png";
            PlayIcon = "ic_play.png";
        }

        public FavoriteEntity(string name, string stream, string favoriteSource,string playIcon)
        {
            Name = name;
            Stream = stream;
            FavoriteSource = favoriteSource;
            PlayIcon = playIcon;
        }

        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Stream { get; set; }
        public string FavoriteSource { get; set; }
        public string PlayIcon { get; set; }

    }
}