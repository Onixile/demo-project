using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Extensions
{
  public static class GameObjectExtensions
  {
    public static GameObject RemoveCloneFromName(this GameObject obj)
    {
      obj.name = obj.name.Remove("(Clone)");
      return obj;
    }
  }
}