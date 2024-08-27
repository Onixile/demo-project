using System;
using _Project.Game.Scripts.Runtime.UI.View.List;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class GameStateScreenController : ScreenController<GameStateScreenView>
  {
    public GameStateScreenController(GameStateScreenView view) : base(view) =>
      _view.Initialization();

    public override void Cleanup(Action onComplete = null) =>
      Hide(() => base.Cleanup(onComplete));
  }
}