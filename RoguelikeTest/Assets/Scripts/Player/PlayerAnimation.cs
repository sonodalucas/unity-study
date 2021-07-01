using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class PlayerAnimation : MonoBehaviour
{
    private enum Facing
    {
        up,
        down,
        left,
        right
    }
    
    private enum AnimationName
    {
        idleUp,
        idleDown,
        idleLeft,
        idleRight,
        walkingUp,
        walkingDown,
        walkingRight,
        walkingLeft
    }

    private string m_CurrentAnimation;
    private Facing m_Facing;
    private Animator m_Animator;
    private Rigidbody2D m_Rb;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (m_Rb.velocity == Vector2.zero)
        {
            switch (m_Facing)
            {
                case Facing.up:
                    PlayAnimation(AnimationName.idleUp);
                    break;
                case Facing.down:
                    PlayAnimation(AnimationName.idleDown);
                    break;
                case Facing.right:
                    PlayAnimation(AnimationName.idleRight);
                    break;
                case Facing.left:
                    PlayAnimation(AnimationName.idleLeft);
                    break;
            }
        }
        else
        {
            if (m_Rb.velocity.x > 0.1f)
            {
                PlayAnimation(AnimationName.walkingRight);
                m_Facing = Facing.right;
            }
            else if (m_Rb.velocity.x < -0.1f)
            {
                PlayAnimation(AnimationName.walkingLeft);
                m_Facing = Facing.left;
            } else if (m_Rb.velocity.y > 0.1f)
            {
                PlayAnimation(AnimationName.walkingUp);
                m_Facing = Facing.up;
            } else if (m_Rb.velocity.y < -0.1f)
            {
                PlayAnimation(AnimationName.walkingDown);
                m_Facing = Facing.down;
            }
        }
    }

    private void PlayAnimation(AnimationName animName)
    {
        if (m_CurrentAnimation == animName.ToString()) return;

        m_CurrentAnimation = animName.ToString();
        m_Animator.Play(m_CurrentAnimation);
    }
}
