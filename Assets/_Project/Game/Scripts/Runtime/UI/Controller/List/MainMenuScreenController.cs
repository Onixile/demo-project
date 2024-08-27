using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using UnityEngine.Events;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class MainMenuScreenController : ScreenController<MainMenuScreenView>
  {
    public MainMenuScreenController(MainMenuScreenView view,
      UnityAction onPlay, UnityAction onSettings, UnityAction onShop, IAudioPlayer audio) : base(view) =>
      _view.Initialization(onPlay, onSettings, onShop, audio.Play);
  }
}