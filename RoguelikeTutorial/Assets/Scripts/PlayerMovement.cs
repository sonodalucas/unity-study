using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField] private float dashManaCost;
    [SerializeField]
    private float dashSpeed;
    
    private Vector2 direction;
    private Animator anim;
    private Vector2 lastDirection;
    private bool dashing;
    private PlayerStats stats;
    private static readonly int XIdle = Animator.StringToHash("xIdle");
    private static readonly int YIdle = Animator.StringToHash("yIdle");
    private static readonly int YDir = Animator.StringToHash("yDir");
    private static readonly int XDir = Animator.StringToHash("xDir");
    private static readonly int Magnitude = Animator.StringToHash("magnitude");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!dashing)
        {
            TakeInput();
            Move();
        }
        else
        {
            Dash();
        }
        
        
    }

    private void Move()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
        anim.SetFloat(XDir, direction.x);
        anim.SetFloat(YDir, direction.y);
        anim.SetFloat(Magnitude, direction.magnitude);
        
        if (direction.magnitude != 0)
        {
            lastDirection = direction;
            anim.SetFloat(XIdle, 0);
            anim.SetFloat(YIdle, 0);
        }
        else
        {
            anim.SetFloat(YIdle, lastDirection.y);
            anim.SetFloat(XIdle, lastDirection.x);
        }
    }

    private void TakeInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashing = true;
        }
    }

    private void Dash()
    {
        dashing = false;
        
        if (!stats.HasEnoughMana(dashManaCost)) return;
        
        transform.Translate(direction * (dashSpeed * Time.deltaTime));
        stats.SpendMana(dashManaCost);
    }
}
