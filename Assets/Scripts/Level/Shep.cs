using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shep : MonoBehaviour
{

    private PlayerController playerController;


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController.ChangeLife(-1);
        playerController.CheckPoint();
        
    }

}
