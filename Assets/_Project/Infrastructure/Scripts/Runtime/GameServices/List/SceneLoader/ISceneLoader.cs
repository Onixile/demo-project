using System;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.SceneLoader
{
  public interface ISceneLoader : IGameService
  {
    void Load(string sceneName, Action onComplete, Action<float> onSetProgress);
  }
}