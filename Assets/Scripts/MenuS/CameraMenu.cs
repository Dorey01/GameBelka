using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    public float speed = 5f; // Скорость движения камеры

    void Update()
    {
        // Двигаем камеру вправо по оси X
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
