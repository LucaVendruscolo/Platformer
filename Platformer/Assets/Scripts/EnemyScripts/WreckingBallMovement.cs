using UnityEngine;

public class PendulumMotion : MonoBehaviour
{
    public float swingAngle = 30f; 
    public float swingSpeed = 2f;  
    public Transform apple;     
    public Transform rope;      

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // calc angle of swing.
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        // rope is rotated in a pivotal manner (from the support)
        transform.rotation = initialRotation * Quaternion.Euler(0, 0, angle);

        // position of the bottom of the rope is calculated, for the apple to match it.
        if (apple != null && rope != null)
        {
            float ropeLength = rope.localScale.y; 
            Vector3 bottomOfRope = rope.position - transform.up * (ropeLength)  * 2f;

            // apple matches the position and rotation of the bottom of the rope.
            apple.position = bottomOfRope;
            apple.rotation = transform.rotation;
        }
    }
}
