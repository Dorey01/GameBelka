using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CameraFollow cameraFollow;
    public AudioClip[] chorusFilter;
    public AudioSource chorusSource;


    #region Переменные здоровья
    [SerializeField] private Image[] Life = new Image[7];
    public int life = 6;
    public int life_;
    #endregion

    #region Переменные движения
    public float speed;
    public float jumpForce;
    public Vector2 moveVector;
    public float rayDistance = 0.6f;
    private float horizantalMove = 0f;
    private bool facingRight = true;
    public int jump;
    #endregion

    #region Компоненты
    private Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Стрельба
    public GameObject bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    #endregion

    #region Проверка земли и стены
    public Transform groundCheck;
    public Transform frontCheck;
    public bool isGround = false;
    private bool isWallFront = false;
    private float xCheck, yCheck, zCheck = 0;
    #endregion

    #region Прыжок от стены
    private bool blockMoveXforJump;
    public bool blockMoveBoss = false;
    public float jumpWallTime = 0.5f;
    private float timerJumpWall;
    public Vector2 jumpAngle = new Vector2(3.5f, 10);
    #endregion

    #region Способности
    private bool canJump = false;
    private bool canDoubleJump = false;
    private bool canShoot = false;
    private bool canWallJump = false;

    private void Start()
    {

        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        life_ = life;

        // Проверяем текущую сцену
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "Level1.1")  
        {
            // Разблокируем все способности если это не первый уровень
            UnlockAllAbilities();
        }
    }

    // Добавляем новый метод для разблокировки всех способностей
    private void UnlockAllAbilities()
    {
        canJump = true;
        canDoubleJump = true;
        canShoot = true;
        canWallJump = true;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        CheckFlip();
    }

    private void Update()
    {
        HandleWallJump();
        CheckGroundAndWall();
        if (!blockMoveBoss)
        {
            HandleShooting();
            HandleJump();
            HandleAnimation();
        }
        CheckHealth();

    }

    #region Методы движения
    private void HandleMovement()
    {
        if (!blockMoveXforJump && !blockMoveBoss)
        {
            moveVector.x = Input.GetAxis("Horizontal");
            
            // Проверяем, не уперся ли игрок в стену
            if (isWallFront)
            {
                // Если игрок пытается двигаться в сторону стены, блокируем движение
                if ((facingRight && moveVector.x > 0) || (!facingRight && moveVector.x < 0))
                {
                    moveVector.x = 0;
                }
            }

            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        }
    }
    private void CheckFlip()
    {
        if (facingRight == false && moveVector.x > 0 || facingRight == true && moveVector.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    #endregion

    #region Методы проверки состояния
    private void CheckGroundAndWall()
    {
        Collider2D[] collidersGround = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);
        isGround = collidersGround.Length > 0;

        // Модифицируем проверку стены
        Collider2D[] collidersFront = Physics2D.OverlapCircleAll(frontCheck.position, 0.001f);
        isWallFront = false;
        
        foreach (Collider2D collider in collidersFront)
        {
            // Проверяем, что коллайдер не имеет теги, которые нужно игнорировать
            if (collider.CompareTag("Sliding"))
            {
                isWallFront = true;
                break;
            }
        }
        
        // Применяем проверку только если не на земле
        isWallFront = isWallFront && !isGround;
    }

    public void JumpAndSpeed()
    {
        moveVector.x = 0;
        rb.velocity = new Vector2(moveVector.x, rb.velocity.y);
        anim.SetFloat("Jump", jump);
        anim.SetFloat("Speed", 0);
    }
    private void HandleAnimation()
    {
        horizantalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(horizantalMove));
        anim.SetFloat("Jump", jump);

        // Скольжение по стене только если способность разблокирована
        if (canWallJump && isWallFront)
        {
            anim.SetFloat("Silding", 1);
            rb.velocity = new Vector2(rb.velocity.x, -0.5f);
        }
        else
        {
            anim.SetFloat("Silding", 0);
        }
    }
    #endregion

    #region Методы прыжка и стрельбы

    private void HandleJump()
    {
        if (!canJump) return; // Если прыжок не разблокирован, выходим

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGround)
            {
                chorusSource.clip = chorusFilter[1];
                chorusSource.Play();
                ExecuteJump();
                jump = 1;
            }
            else if (canDoubleJump && jump < 2 && !isWallFront)
            {
                ExecuteJump();
                jump = 2;
            }
        }
    }




    private void HandleShooting()
    {
        if (!canShoot) return; // Выходим, если стрельба не разблокирована

        if (timeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                chorusSource.clip = chorusFilter[0];
                chorusSource.Play();
                FireBullet();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void FireBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        if (spawnedBullet.TryGetComponent<Bullet>(out Bullet bulletScript))
        {
            Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;
            bulletScript.SetDirection(shootDirection);
        }
        timeBtwShots = startTimeBtwShots;
    }
    #endregion

    #region Методы здоровья и респавна
    private void CheckHealth()
    {
        // Проверяем, не упал ли игрок
        if (transform.position.y <= -15)
        {
            if (life > 0)  // Добавляем проверку
            {
                life--;
                Debug.Log(Life.Length);
                if (life >= 0 && life < Life.Length)  // Проверяем границы массива
                {
                    Destroy(Life[life]);
                }
                CheckPoint();
            }
        }

        // Обновление отображения жизней
        if (life < life_)
        {
            life_ = life;
            if (life >= 0 && life < Life.Length)  // Проверяем границы массива
            {
                Destroy(Life[life]);
            }
        }

        // Проверка на смерть
        if (life <= 0)
        {
            Respawn();
        }
    }

    public void CheckPoint()
    {
        transform.position = new Vector3(xCheck, yCheck, zCheck);
    }

    private void Respawn()
    {
        if (life <= 0)
        {
            if (SceneManager.GetActiveScene().name == "Level1.1" || SceneManager.GetActiveScene().name == "Level1.2")
            {
                cameraFollow.ResetCam();
                SceneManager.LoadScene("Level1.1");
            }
            if (SceneManager.GetActiveScene().name == "Level2.1" || SceneManager.GetActiveScene().name == "Level2.2")
            {
                cameraFollow.ResetCam();
                SceneManager.LoadScene("Level2.1");
            }
        }
    }

    public void ChangeLife(int damage)
    {
        life += damage;
    }
    #endregion

    #region Обработчики столкновений
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jump = 0;
        }

        // Проверяем столкновение с движущейся платформой
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Проверяем, что контакт происходит сверху платформы
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f)  // Игрок находится сверху платформы
                {
                    transform.SetParent(collision.transform);
                    jump = 0;
                    break;
                }
            }
        }
    }

    // Добавляем метод для отсоединения от платформы
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    private void HandleWallJump()
    {
        if (!canWallJump) return; // Выходим, если прыжок от стены не разблокирован

        if (isWallFront && !isGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            chorusSource.clip = chorusFilter[1];
            chorusSource.Play();
            ExecuteWallJump();
        }

        if (blockMoveXforJump)
        {
            UpdateWallJumpTimer();
        }
    }

    private void ExecuteWallJump()
    {
        blockMoveXforJump = true;
        moveVector.x = 0;
        Flip();
        rb.velocity = Vector2.zero;
        
        float jumpDirection = transform.localScale.x > 0 ? 4 : -4;
        rb.velocity = new Vector2(jumpDirection, jumpAngle.y);
    }

    private void UpdateWallJumpTimer()
    {
        timerJumpWall += Time.deltaTime;
        if (timerJumpWall >= jumpWallTime)
        {
            if (isWallFront || isGround || Input.GetAxisRaw("Horizontal") != 0)
            {
                blockMoveXforJump = false;
                timerJumpWall = 0;
            }
        }
    }
    #endregion

    public void UnlockJump()
    {
        canJump = true;
    }

    public void UnlockDoubleJump()
    {
        canDoubleJump = true;
    }

    public void UnlockShooting()
    {
        canShoot = true;
    }

    public void UnlockWallJump()
    {
        canWallJump = true;
    }

    private void ExecuteJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void SetCheckpointPosition(Vector3 position)
    {
        xCheck = position.x;
        yCheck = position.y;
        zCheck = position.z;
    }
    #endregion
}
