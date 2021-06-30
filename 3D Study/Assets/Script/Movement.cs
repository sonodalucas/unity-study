using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform player;
    public float movSpeed;
 
    private Vector2 input;

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.magnitude != 0)
        {
            player.position += new Vector3(input.x, 0, input.y) * (movSpeed * Time.deltaTime);
        }
    }
}
