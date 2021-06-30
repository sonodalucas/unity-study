using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HakuMove : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;
    
    private Vector2 direction;

    private void Start()
    {
        InvokeRepeating(nameof(Move), 0, 0.2f);
    }

    private void Move()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        Vector2 cursoPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position,
            cursoPos, moveSpeed * Time.deltaTime);
        
    }
}
