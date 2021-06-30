using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        Vector2 position = transform.position;
        var spell = Instantiate(projectile, position, quaternion.identity);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - position).normalized;
        spell.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
