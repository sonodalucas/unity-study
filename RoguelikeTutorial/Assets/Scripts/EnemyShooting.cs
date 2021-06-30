using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown;
    
    private Transform player;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(ShootPlayer), 0, cooldown );
    }

    private void ShootPlayer()
    {
        if (player != null)
        {
            var spell = Instantiate(projectile, transform.position, quaternion.identity);
            Vector2 myPos = transform.position;
            var direction = ((Vector2)player.position - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}
