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
        m_InputActions.Player.Movement.performed += ctx => m_Direction = ctx.ReadValue<Vector2>();
        m_InputActions.Player.Movement.canceled += ctx => m_Direction = Vector2.zero;
        m_Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Debug.Log(m_Direction);
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
