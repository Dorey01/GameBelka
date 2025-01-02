using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform followTransform;
    private bool isFollowing = true; // Флаг для проверки, следует ли камера за игроком

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
        if (!isFollowing) return; // Если камера не должна следовать, пропускаем

        if (followTransform == null)
        {
            FindPlayer();
            if (followTransform == null) return;
        }

        this.transform.position = new Vector3(
            followTransform.position.x,
            0,
            this.transform.position.z
        );
    }

    // Метод для включения/выключения следования камеры
    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }
}
