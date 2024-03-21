using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class PopupWindow : Window
  {
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    public void Initialization(string title, string description, UnityAction onYes, UnityAction onNo)
    {
      _titleText.text = title;
      _descriptionText.text = description;

      _yesButton.onClick.RemoveAllListeners();
      _noButton.onClick.RemoveAllListeners();
      
      _yesButton.onClick.AddListener(onYes);
      _noButton.onClick.AddListener(onNo);
    }
  }
}