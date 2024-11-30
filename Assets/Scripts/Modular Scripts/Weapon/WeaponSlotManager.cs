using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    public Weapon[] weaponSlots;  // Array of all weapon slots on the ship
    private int activeWeaponIndex = -1;  // The index of the currently selected weapon (-1 means none)

    void Update()
    {
        // Handle weapon switching via keys 1, 2, 3, etc.
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectWeapon(i);
            }
        }
    }

    // Select a weapon by index
    public void SelectWeapon(int index)
    {
        if (index >= 0 && index < weaponSlots.Length)
        {
            // Deselect current weapon
            if (activeWeaponIndex != -1)
            {
                weaponSlots[activeWeaponIndex].DeactivatePlayerControl();
            }

            // Select the new weapon
            activeWeaponIndex = index;
            weaponSlots[activeWeaponIndex].ActivatePlayerControl();
            Debug.Log("Weapon " + (index + 1) + " selected");
        }
    }
}
