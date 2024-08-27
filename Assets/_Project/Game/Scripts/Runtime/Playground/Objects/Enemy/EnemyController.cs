using _Project.Game.Scripts.Runtime.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Enemy
{
  public class EnemyController : MonoBehaviour
  {
    private float _rotationSpeed;

    public void Initialization(PlaygroundConfig.EnemyData enemyData)
    {
      transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
      _rotationSpeed = enemyData.RotationSpeed;
    }

    private void Update() =>
      transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
  }
}