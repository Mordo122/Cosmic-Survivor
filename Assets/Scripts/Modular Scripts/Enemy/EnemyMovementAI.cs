using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAI : MonoBehaviour
{
    public Transform player;           // The player the enemy will redirect towards
    public float turnSpeed = 5f;       // Speed at which the enemy turns towards the player
    public float stopDuration = 2f;    // Time to wait before resetting the behavior
    public float reachThreshold = 1f; // Distance threshold to consider "reaching" the player

    private MovementHandler movementHandler;
    private bool isPassingThrough = false; // Whether the enemy is in the "passing through" phase

    void Start()
    {
        // Get the MovementHandler component attached to the enemy
        movementHandler = GetComponent<MovementHandler>();

        // Find the player if not manually set
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        if (!isPassingThrough)
        {
            // Rotate to face the player
            RotateTowardsPlayer();

            // Move upward (relative to the enemy's current orientation)
            movementHandler.Move(transform.up);

            // Check if the enemy has "reached" the player
            if (Vector2.Distance(transform.position, player.position) < reachThreshold)
            {
                StartPassingThrough();
            }
        }
        else
        {
            // Continue moving upward (passing phase)
            movementHandler.Move(transform.up);
        }
    }

    void RotateTowardsPlayer()
    {
        // Calculate direction to the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the rotation needed to look at the player
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;

        // Smoothly rotate towards the target direction
        Quaternion targetRotation = Quaternion.Euler(0, 0, angleToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void StartPassingThrough()
    {
        isPassingThrough = true;

        // Wait for the specified duration, then reset the behavior
        StartCoroutine(ResetBehaviorAfterDelay());
    }

    IEnumerator ResetBehaviorAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(stopDuration);

        // Reset state to start redirecting towards the player again
        isPassingThrough = false;
    }
}
