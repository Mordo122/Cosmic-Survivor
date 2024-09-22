using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public float thrustForce = 10f; // The force applied when moving forward/backward
    public float rotationSpeed = 100f; // The speed at which the spaceship rotates
    public float drag = 0.5f; // Drag to reduce velocity over time and avoid constant drift

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Set drag so it doesn't drift forever
        rb.drag = drag;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    // Handle spaceship rotation
    void HandleRotation()
    {
        // Input for rotation (A/D or Left Arrow/Right Arrow)
        float rotationInput = -Input.GetAxis("Horizontal");

        // Apply rotation
        transform.Rotate(0, 0, rotationInput * rotationSpeed * Time.deltaTime);
    }

    // Handle forward and backward thrust movement
    void HandleMovement()
    {
        // Input for thrust (W/S or Up Arrow/Down Arrow)
        float thrustInput = Input.GetAxis("Vertical");

        // Apply force in the direction the spaceship is facing
        rb.AddForce(transform.up * thrustInput * thrustForce);
    }

    // If you feel that the spaceship can reach too high speeds, you can cap its velocity.
    void FixedUpdate()
    {
        // Limit the spaceship's velocity
        float maxSpeed = 5f; // Adjust this value to control max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

}

