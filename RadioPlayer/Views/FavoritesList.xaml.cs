using RadioPlayer.Models;
using RadioPlayer.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace RadioPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesList : ContentPage
    {
        public FavoritesList()
        {
            InitializeComponent();

            MyFavoritesListView.ItemSelected += (sender, e) => MyFavoritesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as FavoritesListViewModel)?.OnResume();
        }

        private void ChangeIcon(object sender, EventArgs e)
        {
            (BindingContext as FavoritesListViewModel).ChangeIcon.Execute(
                (sender as ImageButton).CommandParameter as Favorite);
        }

        private void DeleteAll(object sender, EventArgs e)
        {
            (BindingContext as FavoritesListViewModel).DeleteAllFavorites.Execute(
                (sender as ToolbarItem));
        }

        private void DeleteClicked(object sender, System.EventArgs e)
        {
            (BindingContext as FavoritesListViewModel)?.DeleteFavorite.Execute(
                (sender as ImageButton)?.CommandParameter as Favorite);
        }
    }
}
