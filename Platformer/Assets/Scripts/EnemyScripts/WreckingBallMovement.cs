using UnityEngine;

public class PendulumMotion : MonoBehaviour
{
    public float swingAngle = 30f; // Maximum swing angle (degrees)
    public float swingSpeed = 2f;  // Swing speed
    public Transform apple;       // Reference to the apple
    public Transform rope;        // Reference to the rope (cylinder)

    private Quaternion initialRotation; // Initial rotation of the rope

    void Start()
    {
        initialRotation = transform.rotation; // Save the initial rotation
    }

    void Update()
    {
        // Calculate the current swing angle
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        // Rotate the rope from its pivot (top of the rope)
        transform.rotation = initialRotation * Quaternion.Euler(0, 0, angle);

        // Calculate the bottom of the rope
        if (apple != null && rope != null)
        {
            float ropeLength = rope.localScale.y; // Assumes the rope's length is scaled along Y-axis
            Vector3 bottomOfRope = rope.position - transform.up * (ropeLength)  * 2f;

            // Update the apple's position and rotation
            apple.position = bottomOfRope;
            apple.rotation = transform.rotation;
        }
    }
}
