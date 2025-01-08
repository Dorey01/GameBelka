using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float timer = 1f;

    void Update()
    {
        Invoke(nameof(DisableLevelObject), timer);
    }
    private void DisableLevelObject()
    {
        gameObject.SetActive(false);
    }
}
