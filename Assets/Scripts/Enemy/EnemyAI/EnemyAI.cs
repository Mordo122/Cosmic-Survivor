using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player (to target)
    public Transform player;

    // Movement variables
    public float thrustForce = 5f;   // Adjusted for enemy
    public float maxSpeed = 3f;      // Adjusted for enemy
    public float drag = 0.9f;        // Same floaty effect as player

    // Shooting variables
    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint;         // The point from where the projectile will be fired
    public float projectileSpeed = 8f;  // Speed of the projectile
    public float shootingInterval = 2f; // Time between shots

    private Rigidbody2D rb;
    private Camera mainCamera;
    private float timeSinceLastShot = 0f;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // Optionally set linear drag if needed
        rb.drag = drag; // Same floaty effect

        // Optionally, you can find the player automatically if it's tagged as "Player"
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Move towards the player
        MoveTowardsPlayer();

        // Rotate to face the player
        RotateTowardsPlayer();

        // Shoot at the player at regular intervals
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shootingInterval)
        {
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate direction to the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Apply force to move towards the player
        rb.AddForce(direction * thrustForce);

        // Limit the velocity to the maximum speed to avoid too fast movement
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate the angle in degrees (top of object is the y-axis, so add 90 degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Apply the rotation to the enemy
        rb.rotation = angle;
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody2D component of the projectile
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            // Set the velocity of the projectile in the direction the firePoint is facing
            projectileRb.velocity = firePoint.up * projectileSpeed;  // Use 'up' as the firePoint's forward direction
        }
        else
        {
            Debug.LogWarning("Projectile Prefab or Fire Point not set!");
        }
    }
}
