using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // �������� �����
    public float moveSpeed = 2f; // �������� ��������
    public float moveDistance = 1f; // ��������� �������� � ���� �������
    public float attackRange = 2f; // ��������� ��� �����
    public Animator animator; // ������ �� ��������

    private bool facingRight = true; // �������� ������� � �����

    private Vector3 startPosition; // ��������� ������� �����
    private bool movingLeft = true; // ����������� ��������
    private Transform player; // ������ �� ������

    int atack;
    public Animator anim; //�������� 

    private void Start()
    {
        startPosition = transform.position; // ���������� ��������� �������
        player = GameObject.FindWithTag("Player").transform; // ������� ������ �� ���� "Player"
        anim = GetComponent<Animator>(); // ����� ��������
    }

    private void Update()
    {
        anim.SetFloat("Health", health); //�������� ���������� � Animator � �������� ���������� ������� Jump
        anim.SetFloat("Atack", atack); //�������� ���������� � Animator � �������� ���������� ������� Jump
        if (health <= 0)
        {
            health = 0;
            anim.SetFloat("Health", health); //�������� ���������� � Animator � �������� ���������� ������� Jump
            Destroy(gameObject, 1f) ; // ����������� ����� ��� ���� ��������
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            atack = 1;
            anim.SetFloat("Atack", atack); //�������� ���������� � Animator � �������� ���������� ������� Jump
        }
        else
        {
            atack = 0;
            anim.SetFloat("Atack", atack); //�������� ���������� � Animator � �������� ���������� ������� Jump
            Patrol(); // �����������, ���� ����� ������

        }
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingLeft = false; // ������ ����������� �� ������
                Flipe();
            }
        }
        else
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (transform.position.x >= startPosition.x)
            {
                movingLeft = true; // ������ ����������� �� �����
                Flipe();
            }
        }
    }
    void Flipe()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale; //������������ ��������� ������

        Scaler.x *= -1; // ���������
        transform.localScale = Scaler;

    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ��������� ��������
    }
}
