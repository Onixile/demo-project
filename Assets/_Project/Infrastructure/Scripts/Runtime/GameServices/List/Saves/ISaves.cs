using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves
{
  public interface ISaves : IGameService
  {
    ISavedDatas Datas { get; }

    void Update();
    void Reset();
  }
}