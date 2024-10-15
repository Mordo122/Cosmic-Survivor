using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // The target to follow
    public float offset = 0f; // Distance from the target
    public float smoothing = 0.1f; // Smoothing factor
    public float zoomOutFactor = 0.5f; // How much to zoom out towards the cursor

    private void LateUpdate()
    {
        if (target != null)
        {
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0 for 2D

            // Calculate the midpoint between the target and the mouse position
            Vector3 targetPosition = target.position;
            Vector3 midPoint = Vector3.Lerp(targetPosition, mousePosition, zoomOutFactor);

            // Calculate the desired camera position based on the midpoint
            Vector3 desiredPosition = midPoint + (targetPosition - midPoint).normalized * offset;

            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);
            // Update the camera position
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}

