using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{

    public float bounceForce;
    public void bouncePlayer(Rigidbody rb) {
        // bounces the player up.
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }
}