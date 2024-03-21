namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress
{
  public interface IPlayerProgress : IGameService
  {
    PlayerProgressData Data { get; }

    void Save();
  }
}