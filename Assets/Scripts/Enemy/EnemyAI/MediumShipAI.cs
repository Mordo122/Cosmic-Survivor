using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumShipAI : MonoBehaviour
{
    public float strafeSpeed = 3f;        // Speed during strafing
    public float randomFlySpeed = 5f;    // Speed when flying randomly
    public float randomFlyTime = 2f;     // Time spent flying in a random direction
    public float strafeTime = 3f;        // Time spent strafing and shooting
    public float shootInterval = 1f;     // Interval between weapon shoots

    private Rigidbody2D rb;
    private Transform player;
    private float stateTimer;
    private float shootTimer;
    private Vector2 randomDirection;
    private enum State { Strafe, RandomFly }
    private State currentState;

    private WeaponHandler[] weapons;  // Reference to the ship's weapons

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weapons = GetComponentsInChildren<WeaponHandler>(); // Find all weapons attached to the ship

        currentState = State.Strafe;
        stateTimer = strafeTime;
        shootTimer = shootInterval;
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            SwitchState();
        }

        // Shoot weapons if it's time
        if (shootTimer <= 0)
        {
            ShootWeapons();
            shootTimer = shootInterval;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (currentState == State.Strafe)
        {
            Strafe();
        }
        else if (currentState == State.RandomFly)
        {
            FlyRandomly();
        }
    }

    private void SwitchState()
    {
        if (currentState == State.Strafe)
        {
            currentState = State.RandomFly;
            stateTimer = randomFlyTime;

            // Pick a random direction for flying
            randomDirection = Random.insideUnitCircle.normalized;
        }
        else
        {
            currentState = State.Strafe;
            stateTimer = strafeTime;
        }
    }

    private void Strafe()
    {
        // Move sideways relative to the player's position
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 strafeDirection = Vector2.Perpendicular(directionToPlayer); // Perpendicular to player
        rb.velocity = strafeDirection * strafeSpeed;

        // Rotate to face the player
        RotateTowards(player.position);
    }

    private void FlyRandomly()
    {
        // Move in the previously picked random direction
        rb.velocity = randomDirection * randomFlySpeed;

        // Optionally keep the ship facing the player
        if (player != null)
        {
            RotateTowards(player.position);
        }
    }

    private void RotateTowards(Vector2 targetPosition)
    {
        Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f; // Align with the upward direction
    }

    private void ShootWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.Shoot(); // Use the WeaponHandler's Shoot method
        }
    }
}
