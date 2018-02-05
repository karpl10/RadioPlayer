using System.Windows.Input;
using Prism.Commands;
using RadioPlayer.DataAccess.Entities;
using RadioPlayer.Services;
using Xamarin.Forms;

namespace RadioPlayer.Models
{
    public class Station : StationEntity
    {
        private ICommand _playStream;
        public ICommand PlayStream
        {
            get
            {
                if (_playStream == null)
                {
                    _playStream = new DelegateCommand(() =>
                    {
                        DependencyService.Get<IAudioService>().Play_Pause(Stream);
                    });
                }
                return _playStream;
            }
        }

        public Station(StationEntity stationEntity)
            : base(stationEntity.Name, stationEntity.Stream, stationEntity.FavoriteSource, stationEntity.PlayIcon)
        {
            Id = stationEntity.Id;
            Name = stationEntity.Name;
            Stream = stationEntity.Stream;
            FavoriteSource = stationEntity.FavoriteSource;
            PlayIcon = stationEntity.PlayIcon;
        }
    }

    public class Favorite : FavoriteEntity
    {
        private ICommand _playStream;
        public ICommand PlayStream
        {
            get
            {
                if (_playStream == null)
                {
                    _playStream = new DelegateCommand(() =>
                    {
                        DependencyService.Get<IAudioService>().Play_Pause(Stream);
                    });
                }
                return _playStream;
            }
        }

        public Favorite(FavoriteEntity favoriteEntity)
            : base(favoriteEntity.Name, favoriteEntity.Stream, favoriteEntity.FavoriteSource, favoriteEntity.PlayIcon)
        {
            Id = favoriteEntity.Id;
            Name = favoriteEntity.Name;
            Stream = favoriteEntity.Stream;
            FavoriteSource = favoriteEntity.FavoriteSource;
            PlayIcon = favoriteEntity.PlayIcon;

        }
    }
}