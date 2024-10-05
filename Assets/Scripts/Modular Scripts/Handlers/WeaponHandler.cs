using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint;         // The point from where the projectile will be fired
    public float projectileSpeed = 10f; // Speed of the projectile
    public float shootingInterval = 1f; // Time between shots

    private float timeSinceLastShot = 0f;

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    // Call this method to fire a projectile
    public void Shoot()
    {
        if (timeSinceLastShot >= shootingInterval && projectilePrefab != null && firePoint != null)
        {
            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody2D of the projectile and apply velocity
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = firePoint.up * projectileSpeed;

            timeSinceLastShot = 0f; // Reset the timer after shooting
        }
    }
}

