using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using UnityEngine.Events;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class PlaygroundScreenController : ScreenController<PlaygroundScreenView>
  {
    private const string LevelTitleText = "Level";
    
    public PlaygroundScreenController(PlaygroundScreenView view, int numberOfLevel, UnityAction onBack, IAudioPlayer audio) : base(view) =>
      _view.Initialization($"{LevelTitleText} {numberOfLevel + 1}", onBack, audio.Play);

    public void SetHealth(int value, int max) =>
      _view.SetHealth(value, max);
  }
}