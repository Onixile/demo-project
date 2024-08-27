using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.PathPoint
{
  public class FinishPathPoint : PathPoint
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetIcon(Sprite sprite) => 
      _spriteRenderer.sprite = sprite;
  }
}