using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAI : MonoBehaviour
{
    public Transform player;  // The player the enemy will move towards

    private MovementHandler movementHandler;

    void Start()
    {
        // Get the MovementHandler component attached to the enemy
        movementHandler = GetComponent<MovementHandler>();
        
        // Find the player if not manually set
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Move towards the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        movementHandler.Move(directionToPlayer);

        // Rotate to face the player
        movementHandler.RotateTowards(player.position);
    }
}

