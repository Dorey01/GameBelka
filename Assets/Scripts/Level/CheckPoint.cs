using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    // Позиция чекпоинта
    private Vector3 checkpointPosition;


    private void Start()
    {
        checkpointPosition = transform.position; 
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.SetCheckpointPosition(checkpointPosition);
    }


}
