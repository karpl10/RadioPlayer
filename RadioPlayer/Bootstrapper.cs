using Microsoft.Practices.Unity;
using Prism.Unity;
using RadioPlayer.DataAccess.Entities;
using RadioPlayer.DataAccess.Repositories.ContactRepository;
using RadioPlayer.Services;
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
            //Container.RegisterTypeForNavigation<AddContactPage, AddContactPageViewModel>();

            var database = Container.Resolve<IDatabaseService>();
            database.Connection.DropTable<StationEntity>();
            database.Connection.CreateTable<StationEntity>();
            database.Connection.CreateTable<FavoriteEntity>();

            Container.RegisterInstance<IItemRepository>(new ItemDbRepository(database.Connection));
        }
    }
}