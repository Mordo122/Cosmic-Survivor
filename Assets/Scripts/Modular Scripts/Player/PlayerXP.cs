using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    // XP and Leveling variables
    public int currentLevel = 1;               // Player's current level
    public float currentXP = 0f;               // Player's current XP
    public float xpToNextLevel = 100f;         // XP needed to level up
    public float xpIncreasePerLevel = 50f;     // How much more XP is required per level up

    // XP rewards based on ship tags
    public float smallEnemyXP = 10f;           // XP for destroying a small enemy
    public float mediumEnemyXP = 25f;          // XP for destroying a medium enemy
    public float largeEnemyXP = 50f;           // XP for destroying a large enemy


    



    // Method to add XP based on the tag of the destroyed enemy
    public void AddXPBasedOnTag(string tag)
    {
        Debug.Log("Checking tag: " + tag);  // Add this line to ensure the method is called

        float xpToAdd = 0f;

        // Add XP based on the enemy tag
        switch (tag)
        {
            case "SmallEnemy":
                xpToAdd = smallEnemyXP;
                break;
            case "MediumEnemy":
                xpToAdd = mediumEnemyXP;
                break;
            case "LargeEnemy":
                xpToAdd = largeEnemyXP;
                break;
            default:
                Debug.LogWarning("Unknown enemy tag: " + tag);
                break;
        }

        // Add the XP
        AddXP(xpToAdd);
    }

    // Method to add XP and check for level up
    public void AddXP(float xpAmount)
    {
        currentXP += xpAmount;
        Debug.Log("Gained " + xpAmount + " XP!");

        // Check if the player has enough XP to level up
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    // Method to handle leveling up
    private void LevelUp()
    {
        currentXP -= xpToNextLevel;   // Deduct XP required for the current level
        currentLevel++;               // Increase the player's level
        xpToNextLevel += xpIncreasePerLevel; // Increase the XP needed for the next level

        Debug.Log("Leveled up! New Level: " + currentLevel);
    }
}

