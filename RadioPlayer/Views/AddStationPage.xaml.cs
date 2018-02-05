using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RadioPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStationPage : ContentPage
    {
        public AddStationPage()
        {
            InitializeComponent();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}