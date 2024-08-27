using _Project.Game.Scripts.Runtime.Configs;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Camera
{
  [RequireComponent(typeof(CameraController))]
  public class Camera : MonoBehaviour
  {
    public void Initialization(Player.Player player, PlaygroundConfig.CameraData cameraData) =>
      GetComponent<CameraController>().Initialization(player, cameraData);
  }
}