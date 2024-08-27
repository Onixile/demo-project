namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer
{
  public interface IAudioPlayer : IGameService
  {
    void Play(AudioItemType type);
  }
}
