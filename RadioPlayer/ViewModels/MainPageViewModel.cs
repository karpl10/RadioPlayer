using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RadioPlayer.DataAccess.Repositories.ContactRepository;
using RadioPlayer.Events;
using RadioPlayer.Models;

namespace RadioPlayer.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;
        private IItemRepository StationRepository;

        private ObservableCollection<Station> stations;
        public ObservableCollection<Station> Stations
        {
            get { return stations; }
            set
            {
                SetProperty(ref stations, value);
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                SetProperty(ref isRefreshing, value);
            }
        }

        #region Commands

        private ICommand addStation;
        public ICommand AddStation
        {
            get
            {
                if (addStation == null)
                {
                    addStation = new DelegateCommand(() =>
                    {
                        navigationService.Navigate<AddStationPageViewModel>();
                    });
                }

                return addStation;
            }
        }

        private ICommand deleteStation;
        public ICommand DeleteStation
        {
            get
            {
                if (deleteStation == null)
                {
                    deleteStation = new DelegateCommand<Station>(async (station) =>
                    {
                        var result = await pageDialogService.DisplayAlert("Delete Item",
                                string.Format("Are you sure you want to delete {0}?", station.Name),
                                "Yes", "No");

                        if (result)
                        {
                            await StationRepository.Remove(station.Id);
                            OnResume();
                        }
                    });
                }

                return deleteStation;
            }
        }

        private ICommand refresh;
        public ICommand Refresh
        {
            get
            {
                if (refresh == null)
                {
                    refresh = new DelegateCommand(() => OnResume());
                }

                return refresh;
            }
        }

        #endregion

        public MainPageViewModel(IEventAggregator eventAggregator,
                                 INavigationService navigationService,
                                 IPageDialogService pageDialogService,
                                 IItemRepository stationRepository)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.StationRepository = stationRepository;

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

                var stationEntities = await StationRepository.GetAllStations();

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
