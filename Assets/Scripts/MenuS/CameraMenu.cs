using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMenu : MonoBehaviour
{
    public float speed = 5f; // �������� �������� ������

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // ������� ������ ������ �� ��� X
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
