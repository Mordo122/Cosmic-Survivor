using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private WeaponHandler weaponHandler;

    void Start()
    {
        // Get the WeaponHandler component attached to the player
        weaponHandler = GetComponent<WeaponHandler>();
    }

    void Update()
    {
        // Shoot when the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            weaponHandler.Shoot();
        }
    }
}
