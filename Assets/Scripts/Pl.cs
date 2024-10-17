using UnityEngine;

public class Pl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer

    void Start()
    {

        // Получаем компонент SpriteRenderer у объекта 1Player
      spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void Update()
    {
        // Проверяем, нажата ли клавиша V и найден ли SpriteRenderer
        if (Input.GetKeyDown(KeyCode.V))
        {
            spriteRenderer.sortingOrder = -1;
        }
    }
}