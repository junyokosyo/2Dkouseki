using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMove : MonoBehaviour
{
    [Header("移動処理")]
    [SerializeField] float speed = 5f;

    [Header("ジャンプ")]
    [SerializeField] float _jumpPawer = 0.1f;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] private Transform groundCheck; // 接地判定の位置
    [SerializeField] private LayerMask groundLayer; // 「地面」とみなすレイヤー


    //プライベート関数
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
        // groundCheckの位置に、指定した半径の円を作り、その円がgroundLayerに触れているか判定
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        // --- キャラクターの向きを反転 ---
        // 入力方向と現在の向きが違う場合にFlip()を呼び出す

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
    // UnityのSceneに、デバッグ用の円を描画する
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
