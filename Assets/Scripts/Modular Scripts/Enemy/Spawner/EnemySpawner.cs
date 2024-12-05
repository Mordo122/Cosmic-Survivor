using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Enemy prefab to spawn
    public float baseSpawnFrequency = 2f;  // Base time between spawns
    public float spawnDistance = 5f;    // Distance outside the screen where enemies spawn

    private Camera mainCamera;          // Reference to the main camera

    // Reference to the LevelUp script
    public LevelUp levelUp;

    // Modifier for the influence of the level on spawn frequency
    public float levelUpMultiplier = 0.1f;

    void Start()
    {
        // Reference to the main camera
        mainCamera = Camera.main;

        // Start the spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn enemies at regular intervals
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();

            // Adjust spawn frequency based on the level and the multiplier
            float adjustedSpawnFrequency = baseSpawnFrequency / (1 + levelUp.Level * levelUpMultiplier);
            yield return new WaitForSeconds(adjustedSpawnFrequency);
        }
    }

    // Method to spawn an enemy at a random position outside the camera bounds
    void SpawnEnemy()
    {
        // Calculate the camera bounds (edges of the screen in world space)
        Vector3 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)); // Bottom-left corner
        Vector3 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane)); // Top-right corner

        // Randomize spawn location (just outside the screen)
        Vector3 spawnPosition = GetRandomSpawnPosition(screenMin, screenMax, spawnDistance);

        // Instantiate the enemy at the random spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Enemy spawned");
    }

    // Method to get a random spawn position outside the screen
    Vector3 GetRandomSpawnPosition(Vector3 screenMin, Vector3 screenMax, float distanceOutside)
    {
        // Choose which side of the screen to spawn from (top, bottom, left, or right)
        int side = Random.Range(0, 4);  // 0 = Top, 1 = Bottom, 2 = Left, 3 = Right
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0:  // Top
                spawnPosition = new Vector3(
                    Random.Range(screenMin.x, screenMax.x), 
                    screenMax.y + distanceOutside, 0); // Above the top screen edge
                break;
            case 1:  // Bottom
                spawnPosition = new Vector3(
                    Random.Range(screenMin.x, screenMax.x), 
                    screenMin.y - distanceOutside, 0); // Below the bottom screen edge
                break;
            case 2:  // Left
                spawnPosition = new Vector3(
                    screenMin.x - distanceOutside, 
                    Random.Range(screenMin.y, screenMax.y), 0); // To the left of the screen edge
                break;
            case 3:  // Right
                spawnPosition = new Vector3(
                    screenMax.x + distanceOutside, 
                    Random.Range(screenMin.y, screenMax.y), 0); // To the right of the screen edge
                break;
        }

        return spawnPosition;
    }
}
