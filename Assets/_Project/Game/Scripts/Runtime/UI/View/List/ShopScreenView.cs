using System;
using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class ShopScreenView : ScreenView
  {
    public Transform ContentRoot => _contentRoot;

    [SerializeField] private Button    _hideButton;
    [SerializeField] private Transform _contentRoot;

    private ShopScreenContentView[] _contents;

    public void Initialization(ShopScreenContentView[] contents,
      UnityAction onHide, Action<AudioItemType> onPlayAudio)
    {
      _contents = contents;

      _hideButton.onClick.AddListener(onHide);
      _hideButton.AddClickSound(onPlayAudio);
      _hideButton.AddClickAnimation();
    }

    public void UpdateContentStatus(string id, bool isLocked, bool isBought, bool isCurrent) =>
      _contents.First(x => x.Id == id).UpdateStatus(isLocked, isBought, isCurrent);
  }
}