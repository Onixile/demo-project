using _Project.Game.Scripts.Runtime.Configs;
using _Project.Game.Scripts.Runtime.Playground.Objects.PathPoint;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Player
{
  public static class PlayerPath
  {
    public static Vector3[] Create(PlaygroundFactory factory, Transform parent, LineRenderer lineRenderer,
      PlaygroundConfig.PathData pathData, LevelConfig.LevelData levelData, PlaygroundConfig.EnemyData enemyData)
    {
      uint length = levelData.GoalTarget;
      Vector3[] positions = new Vector3[length];

      if (length < 2)
        throw new UnityException("Path can't be shorter than 2");

      int enemyIndex = 0;

      for (int i = 0; i < length; i++)
      {
        Vector3 position = i == 0
          ? Vector3.zero
          : positions[i - 1] + new Vector3(Random.Range(-pathData.OffsetXMax, pathData.OffsetXMax), Random.Range(pathData.OffsetYMin, pathData.OffsetYMax), 0);

        positions[i] = position;

        GameObject point = i != length - 1
          ? factory.CreatePoint<PathPoint.PathPoint>(position, Quaternion.identity).gameObject
          : factory.CreatePoint<FinishPathPoint>(position, Quaternion.identity).gameObject;

        point.transform.SetParent(parent);

        if (i == length - 1)
          point.GetComponent<FinishPathPoint>().SetIcon(levelData.PlanetIcon);
        else
        {
          Enemy.Enemy enemy = factory.CreateEnemy<Enemy.Enemy>(position, Quaternion.identity, levelData.EnemyTypes[enemyIndex]);
          enemy.transform.parent = point.transform;

          enemy.Initialization(enemyData);

          enemyIndex += 1;
          if (enemyIndex >= levelData.EnemyTypes.Length)
            enemyIndex = 0;
        }
      }

      lineRenderer.positionCount = positions.Length;
      lineRenderer.SetPositions(positions);

      return positions;
    }
  }
}