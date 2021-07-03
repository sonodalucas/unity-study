using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private int projectilePoolSize;

    private InputSystem m_InputSystem;
    private Queue<GameObject> m_ProjectilePool;

    private void Awake()
    {
        m_InputSystem = new InputSystem();

        m_ProjectilePool = new Queue<GameObject>();

        for (int i = 0; i < projectilePoolSize; i++)
        {
            var newProjectile = Instantiate(projectile, spawnPoint.position, transform.rotation);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();

            projectileComponent.speed = projectileSpeed;
            projectileComponent.damage = projectileDamage;
            
            m_ProjectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
        }
    }

    private void Update()
    {
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = rotation;
    }

    private void OnShoot()
    {
        var projectile = m_ProjectilePool.Dequeue();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;
        
        projectile.SetActive(true);
        m_ProjectilePool.Enqueue(projectile);
    }
}
