using System;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress
{
  [Serializable]
  public class PlayerProgressData : IPlayerProgressData
  {
    public int Score;
    
    public int GetScore() => 
      Score;

    public void AddScore(int value) => 
      Score += value;

    public void SubtractScore(int value) => 
      Score = Mathf.Clamp(Score - value, 0, int.MaxValue);
  }
}