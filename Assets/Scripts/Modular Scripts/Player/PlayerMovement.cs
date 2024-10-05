using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovementHandler movementHandler;

    void Start()
    {
        // Get the MovementHandler component attached to the player
        movementHandler = GetComponent<MovementHandler>();
    }

    void Update()
    {
        // Get the input for movement
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Move the player based on the input
        movementHandler.Move(movementInput);

        // Make the player rotate towards the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movementHandler.RotateTowards(mousePosition);
    }
}
