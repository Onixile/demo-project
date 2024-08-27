using System.Collections.Generic;
using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using _Project.Infrastructure.Scripts.Runtime.Utility.Helpful.Structs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using AudioType = _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer
{
  public class AudioPlayer : IAudioPlayer
  {
    private const float  VolumeValueExponent      = 0.8f;
    private const string AudioMixerPath           = "Audio Mixer";
    private const string AudioItemsPath           = "Audio Configs";
    private const string MasterVolumeExposedParam = "MasterVolume";
    private const string MusicVolumeExposedParam  = "MusicVolume";
    private const string SoundsVolumeExposedParam = "SoundsVolume";

    private static readonly MinMax Volume = new(-55, 0);

    private readonly ISaves _saves;

    private readonly AudioMixer        _audioMixer;
    private readonly AudioItemConfig[] _audioItems;

    private readonly Dictionary<AudioItemType, AudioSource> _loopedAudioSources;
    private readonly GameObject                             _audioSourcesObject;

    public AudioPlayer(IAssetsProvider assetsProvider, ISaves saves)
    {
      _audioSourcesObject = new GameObject();
      _audioSourcesObject.name = $"{nameof(AudioPlayer).AddSpaceAfterCapital()} (Listener & Sources)";
      _audioSourcesObject.AddComponent<AudioListener>();

      Object.DontDestroyOnLoad(_audioSourcesObject);

      _audioMixer = assetsProvider.GetResource<AudioMixer>(AudioMixerPath);
      _audioItems = assetsProvider.GetResources<AudioItemConfig>(AudioItemsPath);

      _loopedAudioSources = new Dictionary<AudioItemType, AudioSource>();

      _saves = saves;
      _saves.Datas.Settings.Audio.RegisterUpdateListener(ApplySettings);

      DelayedInitialization();
    }

    public void Play(AudioItemType type)
    {
      AudioItemConfig audioItem = _audioItems.First(a => a.Type == type);
      AudioSource source = GetAudioSource(audioItem);

      if (source.isPlaying)
      {
        if (audioItem.HardReplayLooped)
          source.Stop();
        else
          return;
      }

      AudioClip audioClip = audioItem.AudioClip;

      source.clip = audioClip;
      source.playOnAwake = false;
      source.loop = audioItem.IsLoop;
      source.volume = audioItem.Volume;
      source.outputAudioMixerGroup = audioItem.AudioMixerGroup;

      source.Play();
    }

    public AudioSource GetAudioSource(AudioItemConfig audioItem)
    {
      if (audioItem.IsLoop)
      {
        AudioSource audioSource;
        if (_loopedAudioSources.ContainsKey(audioItem.Type))
          audioSource = _loopedAudioSources[audioItem.Type];
        else
        {
          audioSource = _audioSourcesObject.AddComponent<AudioSource>();
          _loopedAudioSources.Add(audioItem.Type, audioSource);
        }

        return audioSource;
      }

      AudioSource tempAudioSource = _audioSourcesObject.AddComponent<AudioSource>();
      Object.Destroy(tempAudioSource, audioItem.AudioClip.length * (Time.timeScale < 0.00999999977648258 ? 0.01f : Time.timeScale));

      return tempAudioSource;
    }

    private async void DelayedInitialization()
    {
      await UniTask.Delay(2000);
      ApplySettings();
    }

    private void ApplySettings()
    {
      SetMasterVolume(_saves.Datas.Settings.Audio.GetMasterVolume());
      SetMusicVolume(_saves.Datas.Settings.Audio.GetMusicVolume());
      SetSoundsVolume(_saves.Datas.Settings.Audio.GetSoundsVolume());
    }

    private void SetMasterVolume(float value)
    {
      _audioMixer.SetFloat(MasterVolumeExposedParam,
        Mathf.Lerp(Volume.Min, Volume.Max, Mathf.Pow(value, VolumeValueExponent)));
    }

    private void SetMusicVolume(float value)
    {
      _audioMixer.SetFloat(MusicVolumeExposedParam,
        Mathf.Lerp(Volume.Min, Volume.Max, Mathf.Pow(value, VolumeValueExponent)));
    }

    private void SetSoundsVolume(float value)
    {
      _audioMixer.SetFloat(SoundsVolumeExposedParam,
        Mathf.Lerp(Volume.Min, Volume.Max, Mathf.Pow(value, VolumeValueExponent)));
    }
  }
}