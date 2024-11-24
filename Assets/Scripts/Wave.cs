using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float amplitude = 1f; // How high and low the object moves
    public float speed = 1f; // How fast the object moves

    private Vector3 startPosition;
    private bool active = true;
    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        if (active)
        {         // Calculate the new Y position using a sine wave
            float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

            // Apply the new position
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            active = false;
        }
    }
}
