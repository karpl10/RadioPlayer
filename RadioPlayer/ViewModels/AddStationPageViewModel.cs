using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using RadioPlayer.DataAccess.Entities;
using RadioPlayer.Events;

namespace RadioPlayer.ViewModels
{
    public class AddStationPageViewModel : BindableBase
    {
        private INavigationService navigationService;
        private IEventAggregator eventAggregator;

        #region Properties

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
                CheckIsAddButtonEnabled();
            }
        }

        private string stream;
        public string Stream
        {
            get { return stream; }
            set
            {
                SetProperty(ref stream, value);
                CheckIsAddButtonEnabled();
            }
        }

        public bool isAddButtonEnabled;
        public bool IsAddButtonEnabled
        {
            get { return isAddButtonEnabled; }
            set
            {
                SetProperty(ref isAddButtonEnabled, value);
            }
        }

        #endregion

        private ICommand addStation;
        public ICommand AddStation
        {
            get
            {
                if (addStation == null)
                {
                    addStation = new DelegateCommand(() => OnAddStation());
                }

                return addStation;
            }
        }

        public AddStationPageViewModel(IEventAggregator eventAggregator,
                                       INavigationService navigationService)
        {
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
        }

        private void OnAddStation()
        {
            var newStation = new StationEntity(Name, Stream);

            eventAggregator.GetEvent<AddStationEvent>().Publish(newStation);

            navigationService.GoBack();
        }

        private void CheckIsAddButtonEnabled()
        {
            IsAddButtonEnabled = !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Stream);
        }
    }
}
