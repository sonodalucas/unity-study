using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float invulnerabilityAfterHit;
    
    private float invulnerabilityTimer;
    private float m_CurrentHealth;

    private void Awake()
    {
        m_CurrentHealth = health;
    }

    public void ReceiveHit(float damage)
    {
        if (Time.time < invulnerabilityTimer) return;
        invulnerabilityTimer = Time.time + invulnerabilityAfterHit;
        
        m_CurrentHealth -= damage;

        if (m_CurrentHealth <= 0)
        {
            ActivateDeath();
        }
        else
        {
            StunPlayer();
        }
    }

    private void ActivateDeath()
    {
        Destroy(gameObject);
    }

    private void StunPlayer()
    {
        
    }
}
