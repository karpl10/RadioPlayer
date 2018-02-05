using RadioPlayer.Models;
using RadioPlayer.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace RadioPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StationsList : ContentPage
    {
        public StationsList()
        {
            InitializeComponent();

            MyStationsListView.ItemSelected += (sender, e) => MyStationsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as StationsListViewModel)?.OnResume();
        }

        private void ChangeIcon(object sender, EventArgs e)
        {
            (BindingContext as StationsListViewModel).ChangeIcon.Execute(
                (sender as ImageButton).CommandParameter as Station);
        }

        private void AddToFavorites(object sender, System.EventArgs e)
        {

            (BindingContext as StationsListViewModel).AddToFavorites.Execute(
                (sender as ImageButton).CommandParameter as Station);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddStationPage()));
        }

        private void DeleteClicked(object sender, System.EventArgs e)
        {
            (BindingContext as StationsListViewModel)?.DeleteStation.Execute(
                (sender as MenuItem)?.CommandParameter as Station);
        }
    }
}
