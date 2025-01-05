using System.Collections;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 30f;

    private Quaternion baseRotation;
    private float targetAngle;

    void Start()
    {
        baseRotation = transform.rotation;
        StartCoroutine(RotateRandomly());
    }

    IEnumerator RotateRandomly()
    {
        while (true) // while true is used here because they will rotate randomly until the enemy dies.
        {
            targetAngle = Random.Range(-maxRotationAngle, maxRotationAngle);
            Quaternion targetRotation = baseRotation * Quaternion.Euler(0f, targetAngle, 0f);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // rotate in a randomly decided direction.
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f)); // how long before randomly rotating again.
        }
    }
}
