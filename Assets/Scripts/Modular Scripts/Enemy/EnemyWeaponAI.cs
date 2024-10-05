using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAI : MonoBehaviour
{
    public Transform player;  // The player to shoot towards

    private WeaponHandler weaponHandler;

    void Start()
    {
        // Get the WeaponHandler component attached to the enemy
        weaponHandler = GetComponent<WeaponHandler>();
        
        // Find the player if not manually set
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Shoot at the player (fire automatically)
        weaponHandler.Shoot();
    }
}

