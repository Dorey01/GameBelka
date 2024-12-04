using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����������, ���� �� ��������� � UI
public class PlayerController : MonoBehaviour
{

    [SerializeField] Image[] Life = new Image[3];
    int life = 3;


    private int jump; // ���������� ����� ��� ��������� ������

    private bool facingRight = true; // �������� ������� � �����

    public float speed; //��������
    public float jumpForce;
    public Vector2 moveVector;
    public float rayDistance = 0.6f;
    float horizantalMove = 0f;

    
    public Animator anim; //�������� 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject bullet; // ������
    public Transform shotPoint; // ����� ������ ����� ������ �����
    private float timeBtwShots; //�� �������
    public float startTimeBtwShots; //����� �����������

    //���������� ��� ������ � �����
    public Transform groundCheck;
    public bool isGround = false; //��� �������� ���� �� ����� ����� �������
    //���������� ��� ������ � �����
    public Transform frontCheck; 
    private bool isWallFront = false; //��� �������� ���� �� ����� ����� �������
    int f = 0;
    int x  = 0;
    private void Start() {

        rb = GetComponent<Rigidbody2D>(); // ��������� ���������� � Rigidbody2D �� ����� ��� ���������� ����������
        anim = GetComponent<Animator>(); // ����� ��������

    }

    private void FixedUpdate() {
        if (!blockMoveXforJump)
        {
            moveVector.x = Input.GetAxis("Horizontal"); // ������ �� �����������
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); // �������� �� X
        }
        if (facingRight == false && moveVector.x > 0) // ��������� ��������� � ������� A || D
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
        Collider2D[] collidersGround = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f); //��� ��������� �������������� � ������ (���� ����� ����� ����� �� ��������� >0)
        isGround = collidersGround.Length > 0; //�������� ������������ �� � ������

        Collider2D[] collidersFront = Physics2D.OverlapCircleAll(frontCheck.position, 0.001f); //��� ��������� �������������� � ������ (���� ����� ����� ����� �� ��������� >0)
        isWallFront = (collidersFront.Length > 0 && !isGround); //�������� ������������ �� �� ������
       

        horizantalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(horizantalMove)); //�������� ���������� � Animator � �������� Speed
        anim.SetFloat("Jump", jump); //�������� ���������� � Animator � �������� ���������� ������� Jump

        if (isWallFront == true)
        {
            anim.SetFloat("Silding", collidersFront.Length); //�������� ���������� � Animator � �������� ���������� ������� isWallFront
            rb.velocity = new Vector2(rb.velocity.x,-0.5f);
        }
        else
        {
            anim.SetFloat("Silding", 0); //�������� ���������� � Animator � �������� ���������� ������� isWallFront
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && jump < 2 && !isWallFront)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // �������� ������������ �������� ����� �������
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump += 1;
        }
        



        if (timeBtwShots <= 0 ) { //��� ���� ����� �� ������� �������
            if (Input.GetKeyDown(KeyCode.E))
            {
                FireBullet(); // �������� ����� ��������
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
        // ����� �������
        GameObject spawnedBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left; // ��������� ����������� �������
            bulletScript.SetDirection(shootDirection); // ������� ����������� �������
        }

        timeBtwShots = startTimeBtwShots; // ������� �����������
    }
    private void OnCollisionEnter2D(Collision2D collision) // � ������ ������� �� ����� ������������ ����� OnCollisionEnter2D() � ������� ����������� �����, ����� ��� ������ ������������� � ������ ��������
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jump = 0;
        }
    }


    void Flipe()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale; //������������ ��������� ������

        Scaler.x *= -1; // ���������
        transform.localScale = Scaler;

    }

    private void OnCollisionEnter2D(Collision collision) //��� ���� ����� ����������� � �������� ����������
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
            Vector3 Scaler = transform.localScale; //������������ ��������� ������  
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



