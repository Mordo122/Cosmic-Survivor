using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    // Ship's Stats
    public float maxHealth = 100f;      // Maximum health
    public float currentHealth;         // Current health
    public float maxShield = 50f;       // Maximum shield
    public float currentShield;         // Current shield
    public float shieldRegenRate = 5f;  // Shield regeneration per second
    public float shieldRegenCooldown = 3f; // Time before shield starts regenerating after damage
    public float damageMultiplier = 1f;  // Multiplier for outgoing damage

    private float timeSinceLastDamage;  // Time since the last time the ship took damage
 
   public float magnet; // range of pickup collection

    void Start()
    {
        // Initialize health and shield to their maximum values
        currentHealth = maxHealth;
        currentShield = maxShield;
       
    }

    void Update()
    {
        // Check if the shield can regenerate
        if (Time.time - timeSinceLastDamage >= shieldRegenCooldown)
        {
            RegenerateShield();
        }
    }

    // Method to handle taking damage
    public void TakeDamage(float damage)
    {
        // Reset the time since last damage
        timeSinceLastDamage = Time.time;

        if (currentShield > 0)
        {
            // If shield exists, absorb damage with shield first
            float remainingDamage = damage - currentShield;
            currentShield -= damage;

            if (currentShield < 0)
            {
                currentShield = 0;
            }

            // If damage exceeds the shield, apply remaining damage to health
            if (remainingDamage > 0)
            {
                currentHealth -= remainingDamage;
            }
        }
        else
        {
            // If no shield, apply damage directly to health
            currentHealth -= damage;
        }

        // If health goes below zero, trigger death (you can implement death handling here)
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to regenerate shield over time
    private void RegenerateShield()
    {
        if (currentShield < maxShield)
        {
            currentShield += shieldRegenRate * Time.deltaTime;
            if (currentShield > maxShield)
            {
                currentShield = maxShield;  // Ensure shield doesn't exceed max
            }
        }
    }

    // Method to handle death (this can be customized for your game)
    private void Die()
    {
        Debug.Log("Ship has been destroyed!");
        // Destroy the ship, trigger explosion, or game over logic
        Destroy(gameObject);  // Example: destroy the GameObject
    }

    // Method to apply damage multiplier to outgoing projectiles
    public float ApplyDamageMultiplier(float baseDamage)
    {
        return baseDamage * damageMultiplier;
    }

}

