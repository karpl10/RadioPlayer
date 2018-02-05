using SQLite.Net.Attributes;

namespace RadioPlayer.DataAccess.Entities
{
    public class StationEntity
    {
        public StationEntity()
        {
        }

        public StationEntity(string name, string stream)
        {
            Name = name;
            Stream = stream;
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Stream { get; set; }
    }

    public class FavoriteEntity
    {
        public FavoriteEntity()
        {
        }

        public FavoriteEntity(string name, string stream)
        {
            Name = name;
            Stream = stream;
        }

        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Stream { get; set; }
    }
}