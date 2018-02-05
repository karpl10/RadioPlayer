using Android.Media;
using RadioPlayer.Droid.Services;
using RadioPlayer.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace RadioPlayer.Droid.Services
{
    public class AudioService : IAudioService
    {
        bool clicks = false;
        MediaPlayer player;

        public AudioService()
        {
        }

        public bool Play_Pause(string url)
        {
            if (clicks == false)
            {
                this.player = new MediaPlayer();
                this.player.SetDataSource(url);
                this.player.SetAudioStreamType(Stream.Music);
                this.player.Prepare();
                this.player.Start();
                clicks = true;
            }
            else
            {
                this.player.Stop();
                clicks = false;
            }


            return true;
        }

        public bool Stop(bool val)
        {
            this.player.Stop();
            clicks = false;
            return true;
        }
    }

}