using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Необходимо, если ты работаешь с UI
public class PlayerController : MonoBehaviour
{
    public bool isGround; //проверка стоит ли персонаж на земле
    private int jump; // переменная нужна для дввойного прыжка

    private bool facingRight = true; // персонаж смотрит в права

    public float speed; //скорость
    public float jumpForce;
    public float moveInput;
    public float rayDistance = 0.6f;
    float horizantalMove = 0f;

    
    public Animator anim; //Анимация 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject bullet; // Патрон
    public Transform shotPoint; // Место откуда будет лететь шишка
    private float timeBtwShots; //КД дброска
    public float startTimeBtwShots; //время перезорядки


    private void Start() {

        rb = GetComponent<Rigidbody2D>(); // получение информайии о Rigidbody2D эт вроде как управление персонажем
        anim = GetComponent<Animator>(); // вызов анимации

    }

    private void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal"); // эт для того чтобы ходил онли по горизонтали
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); // переменные x и y
                                                                     // Получить скорость тока (Vector2)
     

        if (facingRight == false && moveInput > 0) // переварот персонажа в стонону A || D
        {
            Flipe();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flipe();
        }

    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && jump < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Обнуляем вертикальную скорость перед прыжком
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump += 1;
        }

        horizantalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(horizantalMove)); //Передача информации в Animator в значении Speed
        anim.SetFloat("Jump", jump); //Передача информации в Animator в значении количество прыжков Jump

        if (timeBtwShots <= 0 ) { //Для того чтобы не спамить атаками
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots; //рестарт перезарядки
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision) // В данном способе мы будем использовать метод OnCollisionEnter2D() – который срабатывает тогда, когда наш объект соприкасается с другим объектом
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
            jump = 0;
        }
    }


    void Flipe()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale; //оригинальное положение игрока
        
        Scaler.x *= -1; // переворот
        transform.localScale = Scaler;

    }


   
}



