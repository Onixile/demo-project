using _Project.Infrastructure.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.Utility.Attributes;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer
{
  [CreateAssetMenu(fileName = "Audio Item Config", menuName = "Project/Audio Item Config", order = 1000)]
  public class AudioItemConfig : Config
  {
    public AudioItemType   Type             => _type;
    public AudioClip       AudioClip        => _audioClip;
    public AudioMixerGroup AudioMixerGroup  => _audioMixerGroup;
    public float           Volume           => _volume;
    public bool            IsLoop           => _isLoop;
    public bool            HardReplayLooped => _hardReplayLooped;

    [SerializeField]               private AudioItemType   _type;
    [SerializeField]               private AudioClip       _audioClip;
    [SerializeField]               private AudioMixerGroup _audioMixerGroup;
    [SerializeField] [Range(0, 1)] private float           _volume = 1;
    [SerializeField]               private bool            _isLoop;
    [SerializeField]               private bool            _hardReplayLooped;

#if UNITY_EDITOR
    [InspectorButton]
    public void MatchAssetNameToType()
    {
      string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
      string assetName = $"Audio Item ({Type.ToString()})";

      if (name != assetName)
      {
        AssetDatabase.RenameAsset(assetPath, $"Audio Item ({Type.ToString()})");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
      }
    }
#endif
  }
}