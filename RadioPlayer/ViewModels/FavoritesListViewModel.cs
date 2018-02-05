using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RadioPlayer.DataAccess.Repositories.ItemRepository;
using RadioPlayer.Events;
using RadioPlayer.Models;

namespace RadioPlayer.ViewModels
{
    public class FavoritesListViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IItemRepository _favoriteRepository;

        private ObservableCollection<Favorite> _favorites;
        public ObservableCollection<Favorite> Favorites
        {
            get => _favorites;
            set => SetProperty(ref _favorites, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        #region Commands

        private ICommand _deleteFavorite;
        public ICommand DeleteFavorite
        {
            get
            {
                if (_deleteFavorite == null)
                {
                    _deleteFavorite = new DelegateCommand<Favorite>(async (favorite) =>
                    {
                        await _favoriteRepository.RemoveFavorite(favorite);
                        OnResume();
                    });
                }

                return _deleteFavorite;
            }
        }

        private ICommand _deleteAllFavorites;
        public ICommand DeleteAllFavorites
        {
            get
            {
                if (_deleteAllFavorites == null)
                {
                    _deleteAllFavorites = new DelegateCommand(async() =>
                    {
                        await _favoriteRepository.RemoweAllFavorites();
                        OnResume();
                    });
                }

                return _deleteFavorite;
            }
        }

        private ICommand _changeIcon;
        public ICommand ChangeIcon
        {
            get
            {
                if (_changeIcon == null)
                {
                    _changeIcon = new DelegateCommand<Favorite>(async (station) =>
                    {
                        await _favoriteRepository.ChangeIconStation(station);
                        OnResume();
                    });
                }

                return _changeIcon;
            }
        }

        private ICommand _refresh;
        public ICommand Refresh
        {
            get
            {
                if (_refresh == null)
                {
                    _refresh = new DelegateCommand(() => OnResume());
                }

                return _refresh;
            }
        }

        #endregion

        public FavoritesListViewModel(IEventAggregator eventAggregator,
                                 INavigationService navigationService,
                                 IPageDialogService pageDialogService,
                                 IItemRepository favoriteRepository)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._favoriteRepository = favoriteRepository;

            eventAggregator.GetEvent<AddFavoriteEvent>().Subscribe(async x =>
            {
                await favoriteRepository.Add(x);
                OnResume();
            }, true);
        }

        public async void OnResume()
        {
            try
            {
                Favorites = new ObservableCollection<Favorite>();

                var favoriteEntities = await _favoriteRepository.GetAllFavorites();

                foreach (var favoriteEntity in favoriteEntities)
                {
                    Favorites.Add(new Favorite(favoriteEntity));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            IsRefreshing = false;
        }
    }
}