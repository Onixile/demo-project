using _Project.Game.Scripts.Runtime.UI.View.List;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class LoadingScreenController : ScreenController<LoadingScreenView>
  {
    public LoadingScreenController(LoadingScreenView view) : base(view) =>
      _view.Initialization();

    public void SetProgress(float value) =>
      _view.SetProgress(value);
  }
}