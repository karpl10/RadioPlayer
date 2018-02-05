namespace RadioPlayer.Services
{
    public interface IAudioService
    {
        bool Play_Pause(string url);
        bool Stop(bool val);
    }
}