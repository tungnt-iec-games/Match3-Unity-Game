using UnityEngine;

public static class GameHelper
{
    public static System.Func<GameObject, Vector3, Transform, GameObject> SpawnGameObjectDelegate;
    public static System.Action<GameObject> DespawnGameObjectDelegate;

    public static GameObject SpawnGameObject(GameObject prefab, Vector3 localPosition = default(Vector3), Transform parent = null)
    {
        if (SpawnGameObjectDelegate != null)
        {
            return SpawnGameObjectDelegate(prefab, localPosition, parent);
        }

        var go = GameObject.Instantiate(prefab, parent);
        go.transform.localPosition = localPosition;
        return go;
    }

    public static void DespawnGameObject(GameObject gameObject)
    {
        if (DespawnGameObjectDelegate != null)
        {
            DespawnGameObjectDelegate(gameObject);
            return;
        }

        GameObject.Destroy(gameObject);
    }
}
