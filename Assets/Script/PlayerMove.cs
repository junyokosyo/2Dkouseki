using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMove : MonoBehaviour
{
    [Header("�ړ�����")]
    [SerializeField] float speed = 5f;

    [Header("�W�����v")]
    [SerializeField] float _jumpPawer = 0.1f;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] private Transform groundCheck; // �ڒn����̈ʒu
    [SerializeField] private LayerMask groundLayer; // �u�n�ʁv�Ƃ݂Ȃ����C���[


    //�v���C�x�[�g�֐�
    Rigidbody2D rb;
    float moveHorizontal;
    private bool isGrounded;
    private bool isFacingRight = true;
    Animator animator;

    // Start is called before the first frame update


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Jumpanim();
        

    }
    private void FixedUpdate()
    {
        // groundCheck�̈ʒu�ɁA�w�肵�����a�̉~�����A���̉~��groundLayer�ɐG��Ă��邩����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        // --- �L�����N�^�[�̌����𔽓] ---
        // ���͕����ƌ��݂̌������Ⴄ�ꍇ��Flip()���Ăяo��

    }

    
    private void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        Anim();
        if (moveHorizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void Jump()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * _jumpPawer, ForceMode2D.Impulse);

        }
        

    }
    private void Jumpanim()
    {
        float verticalVelocity = rb.velocity.y;
        animator.SetBool("Jump", verticalVelocity > 0.1f);
        animator.SetBool("Foll", verticalVelocity < -0.1f);
        
    }
    private void Anim()
    {
        animator.SetBool("move", moveHorizontal != 0.0f);

    }
    // Unity��Scene�ɁA�f�o�b�O�p�̉~��`�悷��
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
