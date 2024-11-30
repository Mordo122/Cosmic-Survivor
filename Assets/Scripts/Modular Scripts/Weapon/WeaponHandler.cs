using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float shootingInterval = 1f;
    private float timeSinceLastShot = 0f;

    private ShipStats shipStats; // Reference to ShipStats

    void Start()
    {
        // Get the ShipStats component
        shipStats = GetComponent<ShipStats>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (timeSinceLastShot >= shootingInterval && projectilePrefab != null && firePoint != null)
        {
            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Apply damage multiplier to the projectile's damage
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null && shipStats != null)
            {
                projectileScript.damage = shipStats.ApplyDamageMultiplier(projectileScript.baseDamage);
            }

            // Set the velocity of the projectile
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = firePoint.up * projectileSpeed;

            timeSinceLastShot = 0f; // Reset shooting timer
        }
    }
}

