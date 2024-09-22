using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementStrafe : MonoBehaviour
{
    // Movement variables
    public float thrustForce = 10f;  // Controls the force applied when moving
    public float maxSpeed = 5f;      // Maximum speed for the spaceship
    public float drag = 0.9f;        // Controls how fast the ship slows down (affects inertia)

    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // Set the camera (required to convert screen point to world point)
        mainCamera = Camera.main;

        // Optionally set linear drag if needed
        rb.drag = drag; // Tweak this for a better floaty effect
    }

    void Update()
    {
        // Get the input for movement
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Apply force based on input direction
        if (movementInput != Vector2.zero)
        {
            // Add force in the direction of the input
            rb.AddForce(movementInput * thrustForce);

            // Limit the velocity to the maximum speed to avoid too fast movement
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        // Rotate spaceship to face the cursor
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Get mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction to the mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees (top of object is the y-axis, so add 90 degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Apply the rotation to the spaceship
        rb.rotation = angle;
    }
}


