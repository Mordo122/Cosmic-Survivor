using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    // Movement variables
    public float thrustForce = 10f;  // Controls the force applied when moving
    public float maxSpeed = 5f;      // Maximum speed for the object
    public float drag = 0.9f;        // Controls how fast the object slows down

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // Set linear drag for the "floaty" effect
        rb.drag = drag;
    }

    // Call this method to move the object in a direction
    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Add force based on the input direction
            rb.AddForce(direction * thrustForce);

            // Limit the velocity to the maximum speed
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    // Call this method to rotate the object to a specific angle
    public void RotateTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
