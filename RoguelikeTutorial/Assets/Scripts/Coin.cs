using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerStats>() != null)
            {
                other.GetComponent<PlayerStats>().IncrementCoins(coinValue);
            }
            Destroy(gameObject);    
        }
        
    }
}
