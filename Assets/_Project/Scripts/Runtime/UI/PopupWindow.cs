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
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    public void SetupWindow(string title, string description, UnityAction onOk, UnityAction onYes, UnityAction onNo)
    {
      _titleText.text = title;
      _descriptionText.text = description;

      _okButton.onClick.RemoveAllListeners();
      _yesButton.onClick.RemoveAllListeners();
      _noButton.onClick.RemoveAllListeners();
      
      _okButton.onClick.AddListener(onOk);
      _yesButton.onClick.AddListener(onYes);
      _noButton.onClick.AddListener(onNo);
      
      _okButton.gameObject.SetActive(onOk != null);
      _yesButton.gameObject.SetActive(onYes != null);
      _noButton.gameObject.SetActive(onNo != null);
    }
  }
}