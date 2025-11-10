using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float thresholdFloorY = 0f;
    public float thresholdCelingY = 2f;// The Y position threshold
    public float smoothSpeed = 0.125f; // The speed at which the camera smooths

    private float initialCameraY; // Initial Y position of the camera
    private Vector3 offset;   // Offset between the camera and the player

    void Start()
    {
        // Store the initial Y position of the camera
        initialCameraY = transform.position.y;

        // Calculate the initial offset from the camera to the player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition;

        // Check if the player's Y position is above the threshold
        if (player.position.y > thresholdFloorY && player.position.y < thresholdCelingY)
        {
            // Calculate the target position to move the camera up to follow the player
            targetPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            initialCameraY = targetPosition.y;
        }
        else
        {
            // Keep the camera at its initial Y position if the player is below the threshold
            targetPosition = new Vector3(transform.position.x, initialCameraY, transform.position.z);
        }

        // Smoothly interpolate the camera's position towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
