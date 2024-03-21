using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class PlaygroundWindow : Window
  {
    [SerializeField] private Button _backButton;

    public virtual void Initialization(UnityAction onBack)
    {
      _backButton.onClick.AddListener(onBack);
      
      Show();
    }
  }
}