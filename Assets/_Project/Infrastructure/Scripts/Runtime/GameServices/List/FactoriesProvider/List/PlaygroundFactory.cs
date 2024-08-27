using System.Linq;
using _Project.Game.Scripts.Runtime.Playground.Objects.Enemy;
using _Project.Game.Scripts.Runtime.Playground.Objects.PathPoint;
using _Project.Game.Scripts.Runtime.Playground.Objects.Player;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List
{
  public class PlaygroundFactory : GameObjectFactory
  {
    public PlaygroundFactory(IAssetsProvider assetsProvider) : base(assetsProvider)
    {
    }

    public T CreatePlayer<T>(Vector3 position, Quaternion rotation) where T : Player =>
      InstantiateFromAssetsHandles<T>(position, rotation).GetComponent<T>();

    public T CreateEnemy<T>(Vector3 position, Quaternion rotation, EnemyType type) where T : Enemy
    {
      GameObject[] enemies = GetFromAssetsHandles<T>();

      return (from enemyObj in enemies
        let enemy = enemyObj.GetComponent<T>()
        where enemy.Type == type
        select Instantiate(enemyObj, position, rotation)).FirstOrDefault().GetComponent<T>();
    }

    public T CreatePoint<T>(Vector3 position, Quaternion rotation) where T : PathPoint =>
      InstantiateFromAssetsHandles<T>(position, rotation).GetComponent<T>();
  }
}