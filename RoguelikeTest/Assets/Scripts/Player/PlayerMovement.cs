using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 m_Direction;
    private InputSystem m_InputActions;
    private Rigidbody2D m_Rb;

    private void Awake()
    {
        m_InputActions = new InputSystem();
        m_Rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        m_Direction = value.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        m_Rb.velocity = speed * m_Direction;
    }

    private void OnEnable()
    {
        m_InputActions.Enable();
    }

    private void OnDisable()
    {
        m_InputActions.Disable();
    }
}
