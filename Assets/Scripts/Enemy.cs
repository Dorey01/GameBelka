using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
 

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health; 
    public float moveSpeed = 2f; 
    public float moveDistance = 1f; // ��������� �������� � ���� �������
    public float attackRange = 1f; // ��������� ��� �����
    public Animator animator; // ������ �� ��������

    private bool facingRight = true; // �������� ������� � �����

    private Vector3 startPosition; // ��������� ������� �����
    private bool movingLeft = true; // ����������� ��������
    private Transform player; // ������ �� ������
    private Rigidbody rb;

    private PlayerController playerController;
    public Transform playerCheck;
    private void Start()
    {
        startPosition = transform.position; // ���������� ��������� �������
        player = GameObject.FindWithTag("Player").transform; // ������� ������ �� ���� "Player"
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>(); // �������� ��������� Animator, ���� �� ���� �� ��� �� �������
    }


    private void Update()
    {
        if (health <= 0)
        {
            // Фиксируем позицию Y
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // Отключаем все коллайдеры на объекте
            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
            
            // Замораживаем объект на месте
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Обнуляем скорость
                rb.constraints = RigidbodyConstraints2D.FreezeAll; // Замораживаем все движения
            }
            
            health = 0;
            animator.SetFloat("Health", health);
            Destroy(gameObject, 1f);
            return;
        }

        // ���������, ���� ���� ����� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ���� ����� � �������� ������������, �������
        if (distanceToPlayer <= attackRange && timeBtwAttack <= 0)
        {
            Attack();
            timeBtwAttack = startTimeBtwAttack; // ������������ ����� ����� �������
        }
        else if (distanceToPlayer > attackRange)
        {
            Patrol(); // �����������, ���� ����� ������
        }

        // ������ ��� ��������� �����
        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack", true); // ��������� �������� �����

    }

    public void InEnemyAttack()
    {
        // ������ ���� ������, ���� � ���� �����
        Collider2D[] collidersFront = Physics2D.OverlapCircleAll(playerCheck.position, 0.001f); //��� ��������� �������������� � ������ (���� ����� ����� ����� �� ��������� >0)
        if (collidersFront.Length > 0) { 
            playerController.ChangeLife(-1); // ��������� ���� � ������
        }
    }

    private void Patrol()
    {
        animator.SetBool("Attack", false); // ��������� �������� �����
        if (movingLeft)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingLeft = false; // ������ ����������� �� ������
                Flip();
            }
        }
        else
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (transform.position.x >= startPosition.x)
            {
                movingLeft = true; // ������ ����������� �� �����
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale; // �������� ������� ��������� ������
        scaler.x *= -1; // �������������� �� ��� X
        transform.localScale = scaler;
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ��������� ��������
        animator.SetFloat("Health", health); // ��������� �������� � ���������
    }
}
