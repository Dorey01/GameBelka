using UnityEngine;

public class Pl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ������ �� SpriteRenderer

    void Start()
    {

        // �������� ��������� SpriteRenderer � ������� 1Player
      spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void Update()
    {
        // ���������, ������ �� ������� V � ������ �� SpriteRenderer
        if (Input.GetKeyDown(KeyCode.V))
        {
            spriteRenderer.sortingOrder = -1;
        }
    }
}