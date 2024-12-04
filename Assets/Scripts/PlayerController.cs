using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Необходимо, если ты работаешь с UI
public class PlayerController : MonoBehaviour
{

    [SerializeField] Image[] Life = new Image[3];
    int life = 3;


    private int jump; // переменная нужна для дввойного прыжка

    private bool facingRight = true; // персонаж смотрит в права

    public float speed; //скорость
    public float jumpForce;
    public Vector2 moveVector;
    public float rayDistance = 0.6f;
    float horizantalMove = 0f;

    
    public Animator anim; //Анимация 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject bullet; // Патрон
    public Transform shotPoint; // Место откуда будет лететь шишка
    private float timeBtwShots; //КД дброска
    public float startTimeBtwShots; //время перезорядки

    //переменные для зацема к стене
    public Transform groundCheck;
    public bool isGround = false; //Для проверки есть ли стена перед игроком
    //переменные для зацема к стене
    public Transform frontCheck; 
    private bool isWallFront = false; //Для проверки есть ли стена перед игроком
    int f = 0;
    int x  = 0;
    private void Start() {

        rb = GetComponent<Rigidbody2D>(); // получение информайии о Rigidbody2D эт вроде как управление персонажем
        anim = GetComponent<Animator>(); // вызов анимации

    }

    private void FixedUpdate() {
        if (!blockMoveXforJump)
        {
            moveVector.x = Input.GetAxis("Horizontal"); // Ходьба по горизонтали
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); // движение по X
        }
        if (facingRight == false && moveVector.x > 0) // переварот персонажа в стонону A || D
        {
            Flipe();
        }
        else if (facingRight == true && moveVector.x < 0)
        {
            Flipe();
        }

    }
    private void Update()
    {

        WallJump();
        Collider2D[] collidersGround = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f); //Все колайдеры контактирующие с точкой (Если игрок около стены то коллайдер >0)
        isGround = collidersGround.Length > 0; //Проверка контактирует ли с землей

        Collider2D[] collidersFront = Physics2D.OverlapCircleAll(frontCheck.position, 0.001f); //Все колайдеры контактирующие с точкой (Если игрок около стены то коллайдер >0)
        isWallFront = (collidersFront.Length > 0 && !isGround); //Проверка контактирует ли со стеной
       

        horizantalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(horizantalMove)); //Передача информации в Animator в значении Speed
        anim.SetFloat("Jump", jump); //Передача информации в Animator в значении количество прыжков Jump

        if (isWallFront == true)
        {
            anim.SetFloat("Silding", collidersFront.Length); //Передача информации в Animator в значении количество прыжков isWallFront
            rb.velocity = new Vector2(rb.velocity.x,-0.5f);
        }
        else
        {
            anim.SetFloat("Silding", 0); //Передача информации в Animator в значении количество прыжков isWallFront
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && jump < 2 && !isWallFront)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Обнуляем вертикальную скорость перед прыжком
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump += 1;
        }
        



        if (timeBtwShots <= 0 ) { //Для того чтобы не спамить атаками
            if (Input.GetKeyDown(KeyCode.E))
            {
                FireBullet(); // Вызываем метод выстрела
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (gameObject.transform.position.y <= -15)
        {
            Destroy(Life[life - 1]);
            life -= 1;
            gameObject.transform.position = new Vector3(0,0,0);

        }
        if(life == 0)
        {
            Respawn();
        }

    }
    private void FireBullet()
    {
        // Спавн снаряда
        GameObject spawnedBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left; // Учитываем направление взгляда
            bulletScript.SetDirection(shootDirection); // Передаём направление снаряду
        }

        timeBtwShots = startTimeBtwShots; // Рестарт перезарядки
    }
    private void OnCollisionEnter2D(Collision2D collision) // В данном способе мы будем использовать метод OnCollisionEnter2D() – который срабатывает тогда, когда наш объект соприкасается с другим объектом
    {
        if (collision.collider.CompareTag("Ground"))
        {
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

    private void OnCollisionEnter2D(Collision collision) //Для того чтобы магнителась к движущим платформам
    {
        if (collision.gameObject.name.Equals("tiles"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.name.Equals("tiles"))
        {
            this.transform.parent = null;
        }
    }

    private bool blockMoveXforJump;
    public float jumpWallTime = 0.5f;
    private float timerJumpWall;
    public Vector2 jumpAngle = new Vector2(3.5f, 10);
    void WallJump()
    {
        if (isWallFront && !isGround && Input.GetKeyDown(KeyCode.Space))
        {
            blockMoveXforJump = true;
            moveVector.x = 0;

            Flipe();
            Vector3 Scaler = transform.localScale; //оригинальное положение игрока  
            rb.velocity = new Vector2(0, 0);
            if(Scaler.x>0) { rb.velocity = new Vector2(4, jumpAngle.y); }
            else { rb.velocity = new Vector2(-4, jumpAngle.y); }
            
        }
        if (blockMoveXforJump && (timerJumpWall += Time.deltaTime) >= jumpWallTime)
        {
            if (isWallFront || isGround || Input.GetAxisRaw("Horizontal") != 0)
            {
                blockMoveXforJump = false;
                timerJumpWall = 0;
            }
        }
    }

    void Respawn()
    {
       gameObject.transform.position = transform.position;
    }

}



