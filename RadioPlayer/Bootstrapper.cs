using Microsoft.Practices.Unity;
using Prism.Unity;
using RadioPlayer.DataAccess.Entities;
using RadioPlayer.DataAccess.Repositories.ItemRepository;
using RadioPlayer.Services;
using RadioPlayer.ViewModels;
using RadioPlayer.Views;
using Xamarin.Forms;

namespace RadioPlayer
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override Page CreateMainPage()
        {
            return Container.Resolve<MainPage>();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<AddStationPage, AddStationPageViewModel>();

            var database = Container.Resolve<IDatabaseService>();
            //database.Connection.DropTable<StationEntity>();
            //database.Connection.DropTable<FavoriteEntity>();
            database.Connection.CreateTable<StationEntity>();
            database.Connection.CreateTable<FavoriteEntity>();

            Container.RegisterInstance<IItemRepository>(new ItemDbRepository(database.Connection));
        }
    }
}