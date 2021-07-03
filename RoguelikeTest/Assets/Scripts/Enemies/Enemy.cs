using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum AnimList
    {
        walkUp,
        walkDown,
        walkLeft,
        walkRight
    }
    
    [SerializeField] protected float speed;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackDelay;

    protected float attackDelayTimer;
    protected Transform Player;
    protected Rigidbody2D m_Rb;
    protected Animator m_Anim;

    private float m_CurrentHealth;
    private AnimList m_CurrentAnimation;
    
    protected virtual void Awake()
    {
        m_CurrentHealth = health;
        m_Rb = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerMovement>().transform;
    }

    public virtual void ReceiveHit(float dmg)
    {
        m_CurrentHealth -= dmg;

        if (m_CurrentHealth <= 0)
        {
            StartDeath();
        }
    }

    protected virtual void StartDeath()
    {
        Destroy(gameObject);
    }

    protected void AnimateEnemy()
    {
        if (Mathf.Abs(m_Rb.velocity.x) > Mathf.Abs(m_Rb.velocity.y))
        {
            if (m_Rb.velocity.x > 0) ChangeAnimation(AnimList.walkRight);
            else ChangeAnimation(AnimList.walkLeft);
        }
        else
        {
            if (m_Rb.velocity.y > 0) ChangeAnimation(AnimList.walkUp);
            else ChangeAnimation(AnimList.walkDown);
        }
    }

    private void ChangeAnimation(AnimList animationName)
    {
        if (m_CurrentAnimation == animationName) return;

        m_CurrentAnimation = animationName;
        m_Anim.Play(animationName.ToString());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Player")) return;
        
        other.transform.GetComponent<PlayerHealth>().ReceiveHit(damage);
    }
}
