using UnityEngine;

namespace _Project.Scripts.Runtime.Game
{
  [CreateAssetMenu(fileName = "Tic Tac Toe Config", menuName = "Tic Tac Toe/Config")]
  public class TicTacToeConfig : ScriptableObject
  {
    public Sprite CrossSprite => _crossSprite;
    public Sprite CircleSprite => _circleSprite;
    public Vector3 FiledOffset => _filedOffset;
    public Vector2 StartPosition => _startPosition;
    public float CellOffset => _cellOffset;

    [SerializeField] private Sprite _crossSprite;
    [SerializeField] private Sprite _circleSprite;
    [SerializeField] private Vector3 _filedOffset = new(0, 1.51f, 0);
    [SerializeField] private Vector2 _startPosition = new(-1.9f, -1.9f);
    [SerializeField] [Range(0, 10)] private float _cellOffset = 1.9f;
  }
}