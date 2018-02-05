using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RadioPlayer.DataAccess.Repositories.ItemRepository;
using RadioPlayer.Events;
using RadioPlayer.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.ViewModels
{
    public class StationsListViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IItemRepository _stationRepository;
        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<Station> _stations;
        public ObservableCollection<Station> Stations
        {
            get => _stations;
            set => SetProperty(ref _stations, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        #region Commands

        private ICommand _addStation;
        public ICommand AddStation
        {
            get
            {
                return _addStation ?? (_addStation =
                           new DelegateCommand(() => { _navigationService.Navigate<AddStationPageViewModel>(); }));
            }
        }

        private ICommand _deleteStation;
        public ICommand DeleteStation
        {
            get
            {
                if (_deleteStation == null)
                {
                    _deleteStation = new DelegateCommand<Station>(async (station) =>
                    {
                        await _stationRepository.RemoveStation(station.Id);
                        OnResume();
                    });
                }

                return _deleteStation;
            }
        }

        private ICommand _addToFavorites;
        public ICommand AddToFavorites
        {
            get
            {
                if (_addToFavorites == null)
                {
                    _addToFavorites = new DelegateCommand<Station>(async (station) =>
                    {
                        await _stationRepository.AddToFavorites(station);
                        Stations.Add(station);
                        OnResume();
                    });

                }

                return _addToFavorites;

            }
        }

        private ICommand _changeIcon;
        public ICommand ChangeIcon
        {
            get
            {
                if (_changeIcon == null)
                {
                    _changeIcon = new DelegateCommand<Station>(async (station) =>
                    {
                        await _stationRepository.ChangeIconStation(station);
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

        public StationsListViewModel(IEventAggregator eventAggregator,
                                 INavigationService navigationService,
                                 IPageDialogService pageDialogService,
                                 IItemRepository stationRepository)
        {
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;
            this._stationRepository = stationRepository;
            this._eventAggregator = eventAggregator;

            eventAggregator.GetEvent<AddStationEvent>().Subscribe(async x =>
            {
                await stationRepository.Add(x);
                OnResume();
            }, true);
        }



        public async void OnResume()
        {
            try
            {
                Stations = new ObservableCollection<Station>();

                var stationEntities = await _stationRepository.GetAllStations();

                foreach (var stationEntity in stationEntities)
                {
                    Stations.Add(new Station(stationEntity));
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
