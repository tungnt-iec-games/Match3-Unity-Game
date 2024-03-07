using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public static class PrefabDictionaryPool
{
    private static readonly Dictionary<GameObject, ObjectPool<GameObject>> m_poolDict = new();

    public static void Clear()
    {
        m_poolDict.Clear();   
    }

    public static GameObject GetGameObject(GameObject prefab, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null)
    {
        if (m_poolDict.TryGetValue(prefab, out var pool))
        {
            return pool.Get();
        }

        pool = CreateNewPool(prefab, actionOnGet, actionOnRelease);
        m_poolDict.Add(prefab, pool);
        return pool.Get();
    }

    public static void ReleaseGameObject(GameObject prefab, GameObject obj)
    {
        if (m_poolDict.TryGetValue(prefab, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            GameObject.Destroy(obj);   
        }
    }

    private static ObjectPool<GameObject> CreateNewPool(GameObject prefab, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null)
    {
        return new ObjectPool<GameObject>(
            () => GameObject.Instantiate(prefab),
            obj =>
            {
                actionOnGet?.Invoke(obj);
                obj.gameObject.SetActive(true);
            },
            obj =>
            {
                actionOnRelease?.Invoke(obj);
                obj.gameObject.SetActive(false);
            }, 
            defaultCapacity: 5);
    }
}
