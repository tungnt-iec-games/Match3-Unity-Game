using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<GameObject, Pool> m_poolMap;

    private void Awake()
    {
        m_poolMap = new Dictionary<GameObject, Pool>();
    }

    public GameObject Spawn(GameObject prefab, Vector3 localPosition, Transform parent)
    {
        if (!m_poolMap.TryGetValue(prefab, out var pool))
        {
            var poolParent = new GameObject(prefab.name);
            poolParent.transform.SetParent(transform);

            pool = new Pool(poolParent);
            m_poolMap.Add(prefab, pool);
        }

        return pool.Spawn(prefab, localPosition, parent);
    }

    public void Despawn(GameObject gameObject)
    {
        foreach (var pool in m_poolMap.Values)
        {
            if (pool.Own(gameObject))
            {
                pool.Despawn(gameObject);
                return;
            }
        }

        Destroy(gameObject);
    }

    private class Pool
    {
        private Queue<GameObject> m_gameObjectQueue;
        private List<GameObject> m_activeGameObjects;
        private GameObject m_parent;
        private int poolSize;

        public Pool(GameObject parent)
        {
            m_gameObjectQueue = new Queue<GameObject>();
            m_activeGameObjects = new List<GameObject>();
            m_parent = parent;
        }

        public GameObject Spawn(GameObject prefab, Vector3 localPosition, Transform parent)
        {
            GameObject go;
            if (m_gameObjectQueue.Count == 0)
            {
                poolSize++;

                go = GameObject.Instantiate(prefab, parent);
                go.transform.localPosition = localPosition;
                go.name = $"{prefab.name}({poolSize})";
            }
            else
            {
                go = m_gameObjectQueue.Dequeue();
                go.transform.SetParent(parent);
                go.transform.localPosition = localPosition;

            }

            go.SetActive(true);
            m_activeGameObjects.Add(go);

            return go;
        }

        public void Despawn(GameObject gameObject)
        {
            m_gameObjectQueue.Enqueue(gameObject);
            m_activeGameObjects.Remove(gameObject);

            gameObject.transform.SetParent(m_parent.transform);
            gameObject.SetActive(false);
        }

        public bool Own(GameObject gameObject)
        {
            return m_activeGameObjects.Contains(gameObject);
        }
    }
}
