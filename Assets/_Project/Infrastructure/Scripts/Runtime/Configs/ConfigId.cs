using _Project.Game.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.Utility.Attributes;
using UnityEditor;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.Configs
{
  public abstract class ConfigId : Config
  {
    public string Id
    {
      get => _id;
      set => _id = value;
    }
  
    [SerializeField] [ReadOnlyField] private string _id;
  
#if UNITY_EDITOR
    private void OnEnable()
    {
      if(string.IsNullOrEmpty(Id))
        GenerateId();
    }

    [InspectorButton]
    public void GenerateId()
    {
      Id = $"{nameof(PlayerItemConfig)}_{GUID.Generate().ToString()}";
      EditorUtility.SetDirty(this);
    }
#endif
  }
}
