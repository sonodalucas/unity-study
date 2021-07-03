using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] private float minimalDistanceToPlayer;
    [SerializeField] private float attackSpeed;

    private bool m_IsAttacking;
    
    private void FixedUpdate()
    {
        if (m_IsAttacking || Player == null) return;

        if ((transform.position - Player.position).sqrMagnitude > Mathf.Pow(minimalDistanceToPlayer, 2))
        {
            Vector2 direction = (Player.position - transform.position).normalized * speed;
            m_Rb.velocity = direction;
        }
        else
        {
            if (Time.time > attackDelayTimer)
            {
                attackDelayTimer = Time.time + attackDelay;
                StartCoroutine(ChargeAttack());
            }
            else
            {
                m_Rb.velocity = Vector2.zero;
            }
        }
        
        AnimateEnemy();
    }

    private IEnumerator ChargeAttack()
    {
        m_IsAttacking = true;
        
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = Player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            m_Rb.position =Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }

        m_IsAttacking = false;
    }
}
