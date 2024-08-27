using System;
using System.Collections;
using System.Linq;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.Utility.Helpful.Structs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Player
{
  [RequireComponent(typeof(PlayerController))]
  public class Player : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _outlineSpriteRenderer;
    
    private Vector3[] _path;

    private PlayerController _playerController;

    private CurrentMaxInt _goals;
    private CurrentMaxInt _hearts;

    private Action<int, int> _setHearts;
    private Action<bool>     _onComplete;

    private bool _canMove = true;

    private void Awake() =>
      _playerController = GetComponent<PlayerController>();

    public void Initialization(Vector3[] path, PlaygroundConfig.PlayerData playerData, PlayerItemConfig playerConfig,
      Action<int, int> setHearts, Action<bool> onComplete)
    {
      _spriteRenderer.sprite = playerConfig.Sprite;
      _outlineSpriteRenderer.sprite = playerConfig.Sprite;
      
      _path = path;

      _playerController.Initialization(playerData,_spriteRenderer, path.First(), _path[1], GetDamage);

      _goals = new CurrentMaxInt(0, path.Length);
      _hearts = new CurrentMaxInt(playerData.Hearts, playerData.Hearts);

      _onComplete = onComplete;
      _setHearts = setHearts;

      StartCoroutine(UpdateRoutine());
    }

    private IEnumerator UpdateRoutine()
    {
      while (true)
      {
        if (Input.GetMouseButtonDown(0))
          MoveToNextPoint();

        yield return null;
      }
    }

    private void MoveToNextPoint()
    {
      if (_canMove == false)
        return;

      _goals.Current++;
      _canMove = false;
      
      _playerController.MoveTo(_path[_goals.Current], ArriveToDestinationPoint);
    }

    private void ArriveToDestinationPoint()
    {
      if (_goals.Current == _goals.Max - 1)
      {
        Complete(true);
        return;
      }

      if (_goals.Current + 1 <= _goals.Max - 1)
        _playerController.RotateTo(_path[_goals.Current + 1]);

      _canMove = true;
    }

    private void GetDamage()
    {
      _hearts.Current = Mathf.Clamp(_hearts.Current - 1, 0, _hearts.Max);
      _setHearts?.Invoke(_hearts.Current, _hearts.Max);

      if (_hearts.Current == 0)
        Complete(false);
    }

    private async void Complete(bool isWin)
    {
      StopCoroutine(UpdateRoutine());

      if (isWin)
      {
        await UniTask.Delay(500);
        _onComplete?.Invoke(true);
      }
      else
        _onComplete?.Invoke(false);
    }
  }
}