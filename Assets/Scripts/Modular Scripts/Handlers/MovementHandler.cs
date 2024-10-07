using System.Collections;
using System.Collections.Generic;

//ITERATION 1: follow mouse
// using UnityEngine;

// public class MovementHandler : MonoBehaviour
// {
//     // Movement variables
//     public float thrustForce = 10f;  // Controls the force applied when moving
//     public float maxSpeed = 5f;      // Maximum speed for the object
//     public float drag = 0.9f;        // Controls how fast the object slows down

//     private Rigidbody2D rb;

//     void Start()
//     {
//         // Get the Rigidbody2D component
//         rb = GetComponent<Rigidbody2D>();
        
//         // Set linear drag for the "floaty" effect
//         rb.drag = drag;
//     }

//     // Call this method to move the object in a direction
//     public void Move(Vector2 direction)
//     {
//         if (direction != Vector2.zero)
//         {
//             // Add force based on the input direction
//             rb.AddForce(direction * thrustForce);

//             // Limit the velocity to the maximum speed
//             if (rb.velocity.magnitude > maxSpeed)
//             {
//                 rb.velocity = rb.velocity.normalized * maxSpeed;
//             }
//         }
//     }

//     // Call this method to rotate the object to a specific angle
//     public void RotateTowards(Vector2 targetPosition)
//     {
//         Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
//         rb.rotation = angle;
//     }
// }

//ITERATION 2: Mouse drag
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    // Movement variables
    public float thrustForce = 10f;  // Controls the force applied when moving
    public float maxSpeed = 5f;      // Maximum speed for the object
    public float drag = 0.9f;        // Controls how fast the object slows down

    // Rotation variables
    public float rotationSpeed = 180f;  // Rotation speed in degrees per second

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

    // // Call this method to rotate the object smoothly towards the target position
    // public void RotateTowards(Vector2 targetPosition)
    // {
    //     // Get the direction to the target (mouse position)
    //     Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

    //     // Calculate the desired angle (in degrees)
    //     float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

    //     // Get the current rotation of the ship
    //     float currentAngle = rb.rotation;

    //     // Smoothly interpolate the rotation angle towards the target angle
    //     float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime / 360f);

    //     // Apply the new rotation to the Rigidbody2D
    //     rb.rotation = newAngle;
    // }

        //Quaternion
        public void RotateTowards(Vector2 targetPosition)
    {
        // Get the direction to the target (mouse position)
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Calculate the desired angle (in degrees)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Get the current rotation as a Quaternion
        Quaternion currentRotation = Quaternion.Euler(0, 0, rb.rotation);

        // Get the target rotation as a Quaternion
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Smoothly rotate towards the target rotation using RotateTowards
        Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the new rotation to the Rigidbody2D
        rb.rotation = newRotation.eulerAngles.z;
    }

}
