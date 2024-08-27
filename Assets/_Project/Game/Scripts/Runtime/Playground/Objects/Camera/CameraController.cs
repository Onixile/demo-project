using _Project.Game.Scripts.Runtime.Configs;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Camera
{
  public class CameraController : MonoBehaviour
  {
    private Player.Player _player;

    private float _movementSpeed;
    private float _rotationSpeed;
    
    public void Initialization(Player.Player player, PlaygroundConfig.CameraData cameraData)
    {
      _player = player;

      _movementSpeed = cameraData.MovementSpeed;
      _rotationSpeed = cameraData.RotationSpeed;
    }

    private void Update()
    {
      if(!_player)
        return;
      
      transform.position = Vector3.Lerp(transform.position, _player.transform.position, Time.deltaTime * _movementSpeed);
      transform.up = Vector3.Lerp(transform.up, _player.transform.up, Time.deltaTime * _rotationSpeed);
    }
  }
}