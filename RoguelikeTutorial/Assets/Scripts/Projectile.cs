using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private new string tag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tag)) return;
        if (other.GetComponent<TakeDamage>() != null)
        {
            other.GetComponent<TakeDamage>().ReceiveDamage(damage);
        }
        Destroy(gameObject);
    }
}
