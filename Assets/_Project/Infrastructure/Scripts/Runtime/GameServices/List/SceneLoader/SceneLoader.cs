using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.SceneLoader
{
  public class SceneLoader : ISceneLoader
  {
    private const float FakeProgressThreshold = 0.8f;
    private const float ProgressThreshold     = 1.0f;

    public void Load(string sceneName, Action onComplete, Action<float> onSetProgress) =>
      LoadSceneAsync(sceneName, onComplete, onSetProgress);

    private async void LoadSceneAsync(string sceneName, Action onComplete, Action<float> onSetProgress)
    {
      AsyncOperationHandle<SceneInstance> asyncOperation = Addressables.LoadSceneAsync(sceneName);

      await SetFakeProgress(onSetProgress);
      await SetProgress(onSetProgress, asyncOperation);

      await UniTask.Delay(50);

      onComplete?.Invoke();
    }

    private async UniTask SetFakeProgress(Action<float> onSetProgress)
    {
      if (onSetProgress != null)
      {
        await UniTask.Delay(100);

        float progress = 0;
        while (progress < FakeProgressThreshold)
        {
          progress += Time.deltaTime;
          onSetProgress(Mathf.Clamp(progress, 0, FakeProgressThreshold));

          await UniTask.Yield();
        }
      }
    }

    private async UniTask SetProgress(Action<float> onSetProgress, AsyncOperationHandle<SceneInstance> asyncOperation)
    {
      while (!asyncOperation.IsDone)
      {
        onSetProgress?.Invoke(Mathf.Clamp(asyncOperation.PercentComplete, FakeProgressThreshold, ProgressThreshold));
        await UniTask.Yield();
      }

      onSetProgress?.Invoke(ProgressThreshold);
    }
  }
}