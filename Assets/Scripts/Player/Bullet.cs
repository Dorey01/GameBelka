using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    private int damage; // Убрали public, теперь устанавливаем через метод
    public LayerMask whatIsSolid;
    private Vector2 moveDirection;
    private string currentZone; // Добавляем переменную для хранения текущей зоны

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    // Добавляем новый метод для установки зоны
    public void SetZone(string zone)
    {
        currentZone = zone;
        // Устанавливаем урон в зависимости от зоны
        damage = (zone == "Green") ? 2 : 1;
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, moveDirection, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(2);
            }
            if (hitInfo.collider.CompareTag("Boss"))
            {
                hitInfo.collider.GetComponent<BossEnemy>().TakeDamage(damage);
                Debug.Log($"Нанесен урон {damage} в зоне {currentZone}"); // Для отладки
            }
            Destroy(gameObject);
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}