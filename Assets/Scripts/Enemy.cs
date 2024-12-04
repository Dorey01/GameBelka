using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // Здоровье врага
    public float moveSpeed = 2f; // Скорость движения
    public float moveDistance = 1f; // Дистанция движения в одну сторону
    public float attackRange = 2f; // Дистанция для атаки
    public Animator animator; // Ссылка на аниматор

    private bool facingRight = true; // персонаж смотрит в права

    private Vector3 startPosition; // Начальная позиция врага
    private bool movingLeft = true; // Направление движения
    private Transform player; // Ссылка на игрока

    int atack;
    public Animator anim; //Анимация 

    private void Start()
    {
        startPosition = transform.position; // Запоминаем начальную позицию
        player = GameObject.FindWithTag("Player").transform; // Находим игрока по тегу "Player"
        anim = GetComponent<Animator>(); // вызов анимации
    }

    private void Update()
    {
        anim.SetFloat("Health", health); //Передача информации в Animator в значении количество прыжков Jump
        anim.SetFloat("Atack", atack); //Передача информации в Animator в значении количество прыжков Jump
        if (health <= 0)
        {
            health = 0;
            anim.SetFloat("Health", health); //Передача информации в Animator в значении количество прыжков Jump
            Destroy(gameObject, 1f) ; // Уничтожение врага при нуле здоровья
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            atack = 1;
            anim.SetFloat("Atack", atack); //Передача информации в Animator в значении количество прыжков Jump
        }
        else
        {
            atack = 0;
            anim.SetFloat("Atack", atack); //Передача информации в Animator в значении количество прыжков Jump
            Patrol(); // Патрулируем, если игрок далеко

        }
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingLeft = false; // Меняем направление на правое
                Flipe();
            }
        }
        else
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (transform.position.x >= startPosition.x)
            {
                movingLeft = true; // Меняем направление на левое
                Flipe();
            }
        }
    }
    void Flipe()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale; //оригинальное положение игрока

        Scaler.x *= -1; // переворот
        transform.localScale = Scaler;

    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Уменьшаем здоровье
    }
}
