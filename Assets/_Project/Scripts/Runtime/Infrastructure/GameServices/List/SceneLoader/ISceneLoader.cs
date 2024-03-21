using System;
using _Project.Scripts.Runtime.UI;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.SceneLoader
{
  public interface ISceneLoader : IGameService
  {
    void Load(string sceneName, Action onComplete, Action<float> onSetProgress);
  }
}