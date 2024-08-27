using System;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using UnityEngine.Events;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class LevelCompleteScreenController : ScreenController<LevelCompleteScreenView>
  {
    private const string WinDescriptionText  = "You are win!";
    private const string LoseDescriptionText = "You are lose!";

    public LevelCompleteScreenController(LevelCompleteScreenView view, UnityAction onNext, IAudioPlayer audio) : base(view) =>
      _view.Initialization(onNext, audio.Play);

    public void Show(bool isWin, Action onComplete = null)
    {
      SetDescription(isWin);
      base.Show(onComplete);
    }

    public void Show(bool isWin)
    {
      SetDescription(isWin);
      base.Show();
    }

    private void SetDescription(bool isWin) =>
      _view.SetDescription(isWin ? WinDescriptionText : LoseDescriptionText);
  }
}