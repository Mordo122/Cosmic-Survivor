using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float baseDamage = 10f;  // Base damage of the projectile
    public float damage;            // Final damage (after multiplier)

    // Example method when projectile hits something
    void OnTriggerEnter2D(Collider2D collision)
    {
        ShipStats targetStats = collision.GetComponent<ShipStats>();
        if (targetStats != null)
        {
            // Apply damage to the target
            targetStats.TakeDamage(damage);
        }

        // Destroy the projectile after impact
        Destroy(gameObject);
    }
}
