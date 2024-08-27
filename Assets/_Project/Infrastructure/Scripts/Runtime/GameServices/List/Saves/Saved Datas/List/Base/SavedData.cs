using Newtonsoft.Json;
using UnityEngine.Events;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base
{
  public class SavedData : ISavedData
  {
    [JsonIgnore] private UnityEvent _onUpdate { get; }

    protected SavedData() => 
      _onUpdate = new UnityEvent();

    public void RegisterUpdateListener(UnityAction listener) => 
      _onUpdate.AddListener(listener);

    public void UnregisterUpdateListener(UnityAction listener) => 
      _onUpdate.RemoveListener(listener);

    protected void NotifyUpdate() => 
      _onUpdate?.Invoke();
  }
}