using System;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Helpful.Observer
{
  public abstract class TriggerObserver2D: MonoBehaviour
  {
    public event Action<Collider2D> OnEnter;
    public event Action<Collider2D> OnStay;
    public event Action<Collider2D> OnExit;
    
    private void OnTriggerEnter2D(Collider2D other) => 
      OnEnter?.Invoke(other);

    private void OnTriggerStay2D(Collider2D other) => 
      OnStay?.Invoke(other);

    private void OnTriggerExit2D(Collider2D other) => 
      OnExit?.Invoke(other);
  }
}