using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Transform followTransform;

    private void Awake()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        // Ищем игрока по тегу "Player"
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            followTransform = player.transform;
            Debug.Log("Камера следует за игроком");
        }
        else
        {
            Debug.LogError("Объект с тегом 'Player' не найден! Добавьте тег 'Player' игроку.");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (followTransform == null)
        {
            FindPlayer();
            if (followTransform == null) return;
        }

        this.transform.position = new Vector3(followTransform.position.x,0,this.transform.position.z);
    }
}