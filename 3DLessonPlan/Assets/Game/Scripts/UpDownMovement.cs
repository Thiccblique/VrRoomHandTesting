using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    // Public variables
    [Header("Move Settings")]
    public float speed = 2.0f;
    public float height = 1.0f;

    // Private variables
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the game object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(Time.time * speed) * height;

        // Apply the new position to the game object
        transform.position = newPosition;
    }
}
