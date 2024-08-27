using _Project.Infrastructure.Scripts.Runtime.Configs;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Configs
{
  [CreateAssetMenu(fileName = "Playground Config", menuName = "Project/Playground Config", order = 1000)]
  public class PlaygroundConfig : Config
  {
    public PlayerData    PlayerDatas => _playerDatas;
    public CameraData    CameraDatas => _cameraDatas;
    public EnemyData     EnemyDatas  => _enemyDatas;
    public PathData      PathDatas   => _pathDatas;
    public LevelConfig[] LevelConfigs  => _levelConfigs;

    [SerializeField] private PlayerData    _playerDatas;
    [SerializeField] private CameraData    _cameraDatas;
    [SerializeField] private EnemyData     _enemyDatas;
    [SerializeField] private PathData      _pathDatas;
    [SerializeField] private LevelConfig[] _levelConfigs;

    [System.Serializable]
    public struct PlayerData
    {
      [Range(0, 10)] public int   Hearts;
      [Range(0, 20)] public float MovementSpeed;
      [Range(0, 20)] public float RotationSpeed;
    }

    [System.Serializable]
    public struct CameraData
    {
      [Range(0, 20)] public float MovementSpeed;
      [Range(0, 20)] public float RotationSpeed;
    }

    [System.Serializable]
    public struct EnemyData
    {
      [Range(0, 500)] public float RotationSpeed;
    }


    [System.Serializable]
    public struct PathData
    {
      public float OffsetYMin;
      public float OffsetYMax;
      public float OffsetXMax;
    }
  }
}