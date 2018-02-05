using Xamarin.Forms;

namespace RadioPlayer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run(this);

            //MainPage = new RadioPlayerPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}