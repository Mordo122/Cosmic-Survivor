using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    public float detectionRange = 10f;
    public float projectileSpeed;

    private float nextFireTime = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            RotateTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        nextFireTime = Time.time + 1f / fireRate;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = firePoint.up * projectileSpeed;
    }
}

