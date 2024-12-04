using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private Vector2 moveDirection; // Направление движения

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // Убедимся, что направление нормализовано
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, moveDirection, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject); // Уничтожение объекта
        }
        
        
        transform.Translate(moveDirection * speed * Time.deltaTime);
       
    }
}
