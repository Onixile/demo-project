using System;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure
{
  public class GameEscaper : MonoBehaviour
  {
    public bool Sleep { get; set; }

    private Action _onEscape;

    public void Initialization(Action onEscape)
    {
      Sleep = false;
      _onEscape = onEscape;
    }

    private void Update()
    {
      if (!Sleep && Input.GetKey(KeyCode.Escape))
        _onEscape?.Invoke();
    }
  }
}