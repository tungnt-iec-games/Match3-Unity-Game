using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyFx : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    private void Update()
    {
        if (particleSystem)
        {
            if (!particleSystem.IsAlive())
                Destroy(gameObject);
        }
    }
}
