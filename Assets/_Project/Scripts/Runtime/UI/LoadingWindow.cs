using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class LoadingWindow : Window
  {
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Slider _progressBar;

    public override void Show(Action onComplete = null)
    {
      _progressBar.value = 0;

      base.Show(onComplete);
    }

    public void SetupWindow(string descriptionText) =>
      _descriptionText.text = descriptionText;

    public void SetProgress(float value) =>
      _progressBar.value = value;
  }
}