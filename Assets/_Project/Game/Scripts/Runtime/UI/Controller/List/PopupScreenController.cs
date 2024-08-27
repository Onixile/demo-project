using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class PopupScreenController : ScreenController<PopupScreenView>
  {
    private readonly IAudioPlayer _audio;

    public PopupScreenController(PopupScreenView view, IAudioPlayer audio) : base(view) =>
      _audio = audio;

    public void Initialization(string title, string description,
      UnityAction onOk, UnityAction onYes, UnityAction onNo) =>
      _view.Initialization(title, description, onOk, onYes, onNo, _audio.Play);

    public void Initialization(string title, string description, int price, Sprite icon,
      UnityAction onOk, UnityAction onYes, UnityAction onNo) =>
      _view.Initialization(title, description, price, icon, onOk, onYes, onNo, _audio.Play);
  }
}