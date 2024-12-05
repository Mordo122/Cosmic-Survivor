using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    public Weapon[] weaponSlots;  // Array of all weapon slots on the ship
    private int activeWeaponIndex = -1;  // The index of the currently selected weapon (-1 means none)

    private LevelUp playerLevel; // Reference to the LevelUp script for level checking

    [Header("Weapon Level Unlock Requirements")]
    public int[] weaponUnlockLevels; // Array to specify the required level for each weapon slot

    void Start()
    {
        // Get the LevelUp component from the player object
        playerLevel = GetComponent<LevelUp>();

        if (weaponSlots.Length != weaponUnlockLevels.Length)
        {
            Debug.LogError("Weapon slots and unlock levels arrays must have the same length!");
        }

        UpdateWeaponAvailability(); // Ensure all weapons are properly enabled/disabled at start
    }

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

        UpdateWeaponAvailability(); // Continuously check and update weapon availability
    }

    // Select a weapon by index
    public void SelectWeapon(int index)
    {
        if (index >= 0 && index < weaponSlots.Length)
        {
            // Check if the player has reached the required level to unlock this weapon
            if (playerLevel != null && playerLevel.Level < weaponUnlockLevels[index])
            {
                Debug.Log("Weapon " + (index + 1) + " is locked. Reach level " + weaponUnlockLevels[index] + " to unlock it.");
                return;
            }

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

    // Update weapon availability based on the player's current level
    void UpdateWeaponAvailability()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (playerLevel != null && playerLevel.Level < weaponUnlockLevels[i])
            {
                // Disable the weapon if the level requirement is not met
                weaponSlots[i].gameObject.SetActive(false);
            }
            else
            {
                // Enable the weapon if the level requirement is met
                weaponSlots[i].gameObject.SetActive(true);
            }
        }
    }
}
