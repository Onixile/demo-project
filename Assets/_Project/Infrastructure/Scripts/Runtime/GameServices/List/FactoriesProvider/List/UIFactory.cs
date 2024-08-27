using System.Collections.Generic;
using _Project.Game.Scripts.Runtime.UI.View;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List
{
  public class UIFactory : GameObjectFactory
  {
    private const string UIPath = "UI";

    private Canvas _canvas { get; set; }

    public UIFactory(IAssetsProvider assetsProvider) : base(assetsProvider)
    {
    }

    public void CreateCanvas()
    {
      GameObject ui = InstantiateFromAssetsHandles(UIPath);
      _canvas = ui.GetComponentInChildren<Canvas>();

      ui.name = UIPath;

      Object.DontDestroyOnLoad(ui);
    }

    public T CreateScreen<T>() where T : ScreenView =>
      CreateScreen<T>(nameof(T));

    public T CreateScreen<T>(Transform parent) where T : ScreenView =>
      CreateScreen<T>(parent, nameof(T));

    public T CreateScreen<T>(string objName) where T : ScreenView =>
      CreateScreen<T>(_canvas.transform, objName);

    private T CreateScreen<T>(Transform parent, string objName) where T : ScreenView
    {
      T window = InstantiateFromAssetsHandles<T>(parent).GetComponent<T>();
      window.gameObject.name = GetScreenName(objName);

      return window;
    }

    public T[] CreateScreenContent<T>(Transform parent, int amount) where T : ScreenView
    {
      List<T> contentViews = new List<T>(amount);
      for (int i = 0; i < amount; i++)
        contentViews.Add(CreateScreen<T>(parent, nameof(T)));

      return contentViews.ToArray();
    }

    private string GetScreenName(string objName) =>
      objName.Remove("View").AddSpaceAfterCapital();
  }
}