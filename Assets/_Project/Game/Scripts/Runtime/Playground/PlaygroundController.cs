using System;
using System.Linq;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Game.Scripts.Runtime.Playground.Objects.Player;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using UnityEngine;
using Camera = _Project.Game.Scripts.Runtime.Playground.Objects.Camera.Camera;

namespace _Project.Game.Scripts.Runtime.Playground
{
  public class PlaygroundController : MonoBehaviour
  {
    [SerializeField] private LineRenderer _pathRenderer;

    public void Initialization(PlaygroundFactory factory, PlaygroundConfig playgroundConfig, PlayerItemConfig playerConfig,
      Action<int, int> setHearts, Action<bool> onComplete, int currentLevel)
    {
      Vector3[] path = PlayerPath.Create(factory, _pathRenderer.transform, _pathRenderer, 
        playgroundConfig.PathDatas, playgroundConfig.LevelConfigs[currentLevel].LevelDatas, playgroundConfig.EnemyDatas);

      Player player = factory.CreatePlayer<Player>(path.First(), Quaternion.identity);
      player.Initialization(path, playgroundConfig.PlayerDatas, playerConfig, setHearts, onComplete);

      FindObjectOfType<Camera>().Initialization(player, playgroundConfig.CameraDatas);
    }
  }
}