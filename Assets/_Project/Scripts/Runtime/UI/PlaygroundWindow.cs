using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class PlaygroundWindow : Window
  {
    [SerializeField] private Button _pauseButton;

    public virtual void Initialization(UnityAction onPause)
    {
      _pauseButton.onClick.AddListener(onPause);
      
      Show();
    }
  }
}