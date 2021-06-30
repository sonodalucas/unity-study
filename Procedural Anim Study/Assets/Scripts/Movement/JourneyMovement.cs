using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourneyMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    public int extraJumps;
    
    private Rigidbody2D rb;
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;
    private int ExtraJumpValue;
    private Animator anim;
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Jump = Animator.StringToHash("jump");

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ExtraJumpValue = extraJumps;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
        
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight && moveInput > 0)
        {
            anim.SetTrigger(Run);   
            Flip();
        }else if (!facingRight && moveInput < 0)
        {
            anim.SetTrigger(Run);
            Flip();
        }
    }

    private void Update()
    {
        if (isGrounded)
        {
            extraJumps = ExtraJumpValue;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            anim.SetTrigger(Jump);
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

