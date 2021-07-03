using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;

    private void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        other.GetComponent<Enemy>().ReceiveHit(damage);
        gameObject.SetActive(false);
    }
}
