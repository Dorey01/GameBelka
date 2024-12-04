using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ”ничтожить объект через 1 секунду
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
