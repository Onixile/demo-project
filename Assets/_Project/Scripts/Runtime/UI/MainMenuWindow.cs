using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class MainMenuWindow : Window
  {
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;

    public void Initialization(UnityAction onOpenPlayground, UnityAction onOpenShop, string score)
    {
      _playButton.onClick.AddListener(onOpenPlayground);
      _shopButton.onClick.AddListener(onOpenShop);

      _scoreText.text = $"Score: {score}";
      
      Show();
    }
  }
}