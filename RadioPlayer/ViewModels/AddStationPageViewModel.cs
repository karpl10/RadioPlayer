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
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

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

        private ICommand _addStationSave;
        public ICommand AddStationSave
        {
            get
            {
                if (_addStationSave == null)
                {
                    _addStationSave = new DelegateCommand(() => OnAddStation());
                }

                return _addStationSave;
            }
        }

        private ICommand _addStationCancel;
        public ICommand AddStationCancel => _addStationCancel ?? (_addStationCancel = new DelegateCommand(OnCancelStation));

        public AddStationPageViewModel(IEventAggregator eventAggregator,
                                       INavigationService navigationService)
        {
            this._eventAggregator = eventAggregator;
            this._navigationService = navigationService;
        }

        private void OnAddStation()
        {
            var newStation = new StationEntity(Name, Stream, "ic_favorite_border.png", /*false,*/ "ic_play.png");

            _eventAggregator.GetEvent<AddStationEvent>().Publish(newStation);

            _navigationService.GoBack();
        }

        private void OnCancelStation()
        {
            _navigationService.GoBack();
        }

        private void CheckIsAddButtonEnabled()
        {
            IsAddButtonEnabled = !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Stream);
        }
    }
}
