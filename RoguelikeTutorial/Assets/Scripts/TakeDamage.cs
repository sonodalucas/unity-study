using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using  UnityEngine.UI;
using Random = UnityEngine.Random;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected Slider healthBar;

    [SerializeField] private GameObject coin;
    
    protected float health;
    private void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
        CheckDeath();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
            healthBar.value = health;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            var rand = Random.Range(1, 4);
            if (rand == 2)
            {
                Instantiate(coin, transform.position, quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
