using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    public float speed = 5f; // �������� �������� ������

    void Update()
    {
        // ������� ������ ������ �� ��� X
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
