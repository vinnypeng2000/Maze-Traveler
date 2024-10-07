using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 90f;

    [Tooltip("Axis of rotation")]
    public Vector3 rotationAxis = Vector3.up;

    private void Update()
    {
        // Calculate rotation amount
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Apply rotation around the specified axis
        transform.Rotate(rotationAxis, rotationAmount, Space.World);
    }
}