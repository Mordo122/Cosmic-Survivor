using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipStatsUI : MonoBehaviour
{
    public ShipStats shipStats;    // Reference to the ShipStats script
    public Image shieldBar;        // Image component for the shield bar
    public Image healthBar;        // Image component for the health bar

    void Update()
    {
        // Update the shield bar based on current shield percentage
        shieldBar.fillAmount = shipStats.currentShield / shipStats.maxShield;

        // Update the health bar based on current health percentage
        healthBar.fillAmount = shipStats.currentHealth / shipStats.maxHealth;
    }
}
