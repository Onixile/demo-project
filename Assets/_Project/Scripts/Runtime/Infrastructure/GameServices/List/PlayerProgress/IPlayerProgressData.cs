namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress
{
  public interface IPlayerProgressData
  {
    int GetScore();
    void AddScore(int value);
    void SubtractScore(int value);
  }
}