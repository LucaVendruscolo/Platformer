using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;         // Speed of the enemy's movement
    public float distance = 3f;      // Distance the enemy moves left and right

    private Vector3 startPosition;   // To store the starting position

    void Start()
    {
        // Save the initial position
        startPosition = transform.position;
    }

    void Update()
    {
        // Make the enemy move left and right using Mathf.PingPong
        float offsetX = Mathf.PingPong(Time.time * speed, distance) - (distance / 2);

        // Apply the movement to the enemy while keeping the other axes unchanged
        transform.position = new Vector3(startPosition.x + offsetX, startPosition.y, startPosition.z);
    }
}