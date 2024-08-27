using UnityEngine.Events;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base
{
  public interface ISavedData
  {
    void RegisterUpdateListener(UnityAction listener);
    void UnregisterUpdateListener(UnityAction listener);
  }
}