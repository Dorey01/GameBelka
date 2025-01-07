using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    private int damage; // ������ public, ������ ������������� ����� �����
    public LayerMask whatIsSolid;
    private Vector2 moveDirection;
    private string currentZone; // ��������� ���������� ��� �������� ������� ����

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    // ��������� ����� ����� ��� ��������� ����
    public void SetZone(string zone)
    {
        currentZone = zone;
        // ������������� ���� � ����������� �� ����
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
                Debug.Log($"������� ���� {damage} � ���� {currentZone}"); // ��� �������
            }
            Destroy(gameObject);
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}