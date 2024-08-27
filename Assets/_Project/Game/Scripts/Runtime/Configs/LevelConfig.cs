using _Project.Game.Scripts.Runtime.Playground.Objects.Enemy;
using _Project.Infrastructure.Scripts.Runtime.Configs;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Configs
{
  [CreateAssetMenu(fileName = "Level Config", menuName = "Project/Level Config", order = 1000)]
  public class LevelConfig : Config
  {
    public LevelData LevelDatas => _levelDatas;

    [SerializeField] private LevelData _levelDatas;

    [System.Serializable]
    public struct LevelData
    {
      public uint        GoalTarget;
      public uint        Reward;
      public Sprite      PlanetIcon;
      public EnemyType[] EnemyTypes;
    }
  }
}