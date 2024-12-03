using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float baseDamage;   // Base damage of the projectile
    public float damage;             // Final damage (after multiplier)
    public float despawnTime = 5f;   // Time in seconds before the projectile is automatically destroyed

    void Start()
    {
        // Schedule the projectile to be destroyed after `despawnTime` seconds
        Destroy(gameObject, despawnTime);
    }

    // Example method when projectile hits something
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get the ShipStats component from the object it collided with
        ShipStats targetStats = collision.GetComponent<ShipStats>();
        if (targetStats != null)
        {
            // Apply damage to the target
            targetStats.TakeDamage(damage);


            // Check if the target ship was destroyed
            if (targetStats.currentHealth <= 0)
            {
                // If it was destroyed, give the player XP based on the tag
                PlayerXP playerXP = FindObjectOfType<PlayerXP>();
                if (playerXP != null)
                {
                    playerXP.AddXPBasedOnTag(collision.tag); // Check the tag of the destroyed ship
                }
            }
        }

        // Destroy the projectile after hitting something
        Destroy(gameObject);
    }
}

