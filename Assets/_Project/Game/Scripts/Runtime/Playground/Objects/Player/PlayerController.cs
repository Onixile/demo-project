using System;
using System.Collections;
using _Project.Game.Scripts.Runtime.Configs;
using DG.Tweening;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.Playground.Objects.Player
{
  public class PlayerController : MonoBehaviour
  {
    private const float CloseDistance = 0.05f;

    private SpriteRenderer _renderer;

    private float _movementSpeed;
    private float _rotationSpeed;

    private Vector3 _rotationPositionTarget;
    private Action  _getDamage;

    private bool _ignoreDamage;

    public void Initialization(PlaygroundConfig.PlayerData playerData, SpriteRenderer renderer, Vector3 startPosition, Vector3 startRotationPosition, Action getDamage)
    {
      _renderer = renderer;

      transform.position = startPosition;

      _movementSpeed = playerData.MovementSpeed;
      _rotationSpeed = playerData.RotationSpeed;

      _rotationPositionTarget = startRotationPosition;
      _getDamage = getDamage;
    }

    public void MoveTo(Vector3 position, Action onComplete) =>
      StartCoroutine(Moving(position, onComplete));

    public void RotateTo(Vector3 position) =>
      _rotationPositionTarget = position;

    private void Update() =>
      transform.up = Vector3.Lerp(transform.up, _rotationPositionTarget - transform.position, Time.deltaTime * _rotationSpeed);

    private IEnumerator Moving(Vector3 position, Action onComplete)
    {
      while (Vector3.Distance(transform.position, position) > CloseDistance)
      {
        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * _movementSpeed);
        yield return null;
      }

      onComplete?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      Enemy.EnemyObject enemy = other.gameObject.GetComponent<Enemy.EnemyObject>();

      if (enemy != null && _ignoreDamage == false)
      {
        _ignoreDamage = true;
        DoBlink();
        _getDamage?.Invoke();

        enemy.gameObject.SetActive(false);
      }
    }

    private void DoBlink()
    {
      StopBlink();
      _renderer.color = Color.white;

      _renderer.DOFade(0f, 0.1f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InOutQuad).SetUpdate(true)
        .SetDelay(0.1f)
        .OnComplete(() =>
        {
          _renderer.DOFade(1, 0.1f);
          _ignoreDamage = false;
        });
    }

    private void StopBlink() =>
      _renderer.DOKill();

    private void OnDestroy() =>
      StopBlink();
  }
}