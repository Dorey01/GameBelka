using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{

    void Start()
    {
        // Уничтожить объект через 1 секунду
        Destroy(gameObject, 1f);
    }


    void Update()
    {

    }
}
