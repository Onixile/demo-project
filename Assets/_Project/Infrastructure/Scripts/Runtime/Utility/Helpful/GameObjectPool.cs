using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Helpful
{
  public class GameObjectPool
  {
    private readonly Func<GameObject> _createObject;
    private readonly List<GameObject> _pool;

    public GameObjectPool(Func<GameObject> createObject)
    {
      _createObject = createObject;

      _pool = new List<GameObject>();
      Add();
    }

    public GameObject Get()
    {
      foreach (GameObject obj in _pool)
      {
        if (!obj.activeInHierarchy)
        {
          obj.SetActive(true);
          return obj;
        }
      }

      return Add();
    }

    public void Put(GameObject obj) =>
      obj.SetActive(false);

    private GameObject Add()
    {
      GameObject obj = _createObject.Invoke();
      obj.SetActive(false);
      _pool.Add(obj);

      return obj;
    }
  }
}