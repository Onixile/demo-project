using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class GameResultWindow : Window
  {
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _okButton;

    public void Initialization( UnityAction onOk) => 
      _okButton.onClick.AddListener(onOk);

    public void SetupWindow(string title, string description)
    {
      _titleText.text = title;
      _descriptionText.text = description;
    }
  }
}