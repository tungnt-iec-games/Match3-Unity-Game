using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxHub : MonoBehaviour
{
    public GameObject fxExplodeView;

    public void SpawnExplodeView(Vector3 position)
    {
        var g = Instantiate(fxExplodeView);
        g.transform.SetParent(transform);
        g.transform.position = position;
    }
}
