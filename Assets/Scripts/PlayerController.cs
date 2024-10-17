using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����������, ���� �� ��������� � UI
public class PlayerController : MonoBehaviour
{
    public bool isGround; //�������� ����� �� �������� �� �����
    private int jump; // ���������� ����� ��� ��������� ������

    private bool facingRight = true; // �������� ������� � �����

    public float speed; //��������
    public float jumpForce;
    public float moveInput;
    public float rayDistance = 0.6f;
    float horizantalMove = 0f;
    public Animator anim;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;



    private void Start() {

        rb = GetComponent<Rigidbody2D>(); // ��������� ���������� � Rigidbody2D �� ����� ��� ���������� ����������
        anim = GetComponent<Animator>(); // ����� ��������

    }

    private void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal"); // �� ��� ���� ����� ����� ���� �� �����������
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); // ���������� x � y

        if(facingRight == false && moveInput > 0) // ��������� ��������� � ������� A || D
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
        if (Input.GetKeyDown(KeyCode.Space) && (jump < 2))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump += 1;
        }

        horizantalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(horizantalMove)); //�������� ���������� � Animator � �������� Speed
        anim.SetFloat("Jump", jump); //�������� ���������� � Animator � �������� ���������� ������� Jump
        anim.SetFloat("Ground", isGround); //�������� ���������� � Animator � �������� ���������� ������� Jump


    }
    private void OnCollisionEnter2D(Collision2D collision) // � ������ ������� �� ����� ������������ ����� OnCollisionEnter2D() � ������� ����������� �����, ����� ��� ������ ������������� � ������ ��������
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
        Vector3 Scaler = transform.localScale; //������������ ��������� ������
        
        Scaler.x *= -1; // ���������
        transform.localScale = Scaler;

    }


   
}



