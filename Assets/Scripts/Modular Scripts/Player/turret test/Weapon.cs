using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;        // Where the weapon fires from
    public GameObject projectilePrefab;  // The projectile to shoot
    public float projectileSpeed;
    public float fireRate = 1f;         // Time between shots
    public float detectionRange = 10f;  // How far the turret can detect enemies
    public float detectionAngle = 90f;  // The turret's firing arc (in degrees)

    private float nextFireTime = 0f;    // Time when the weapon can fire again
    private bool isPlayerControlled = false;  // Is the weapon under player control?

    private Transform target;           // Current target (if in AI mode)

    void Update()
    {
        if (isPlayerControlled)
        {
            // Handle player-controlled shooting
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
        else
        {
            // AI Turret mode: scan for enemies and shoot
            if (target == null || !IsTargetInRangeAndArc(target))
            {
                target = ScanForEnemies();
            }

            if (target != null && Time.time >= nextFireTime)
            {
                ShootAtTarget(target);
            }
        }
    }

    // Activate manual control
    public void ActivatePlayerControl()
    {
        isPlayerControlled = true;
    }

    // Deactivate manual control and return to AI mode
    public void DeactivatePlayerControl()
    {
        isPlayerControlled = false;
    }

    // Shoot the weapon (player-controlled)
    private void Shoot()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Projectile fired: " + projectile.name + " from " + firePoint.position);

        // Get the Rigidbody2D component from the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Apply velocity to the projectile to make it move forward
        rb.velocity = firePoint.up * projectileSpeed;
        Debug.Log("Projectile velocity: " + rb.velocity);
        

        nextFireTime = Time.time + 1f / fireRate;
        Debug.Log("Player shooting from weapon " + name);
    }

    // Shoot at a specific target (AI-controlled)
    private void ShootAtTarget(Transform target)
    {
        // Calculate the direction to the target
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // Calculate the angle in degrees (from the weapon to the target)
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Rotate the entire weapon (its body) towards the target
        transform.rotation = Quaternion.Euler(0, 0, angle-90);

        // Fire the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Set the velocity of the projectile
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = firePoint.up * projectileSpeed;

        nextFireTime = Time.time + 1f / fireRate;

        Debug.Log("AI shooting at " + target.name);
    }



    // Scan for enemies in range and within the arc
    private Transform ScanForEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                if (IsTargetInRangeAndArc(hit.transform))
                {
                    return hit.transform;
                }
            }
        }

        return null;  // No enemy found in range
    }

    // Check if the target is within range and within the detection arc
    private bool IsTargetInRangeAndArc(Transform target)
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > detectionRange)
            return false;

        float angleToTarget = Vector2.Angle(transform.up, directionToTarget);
        return angleToTarget <= detectionAngle / 2f;
    }
}

