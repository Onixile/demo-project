using _Project.Game.Scripts.Runtime.Configs;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Enemy
{
  [RequireComponent(typeof(EnemyController))]
  public class Enemy : MonoBehaviour
  {
    public EnemyType Type => _type;

    [SerializeField] private EnemyType _type;

    public void Initialization(PlaygroundConfig.EnemyData enemyData) => 
      GetComponent<EnemyController>().Initialization(enemyData);
  }
}