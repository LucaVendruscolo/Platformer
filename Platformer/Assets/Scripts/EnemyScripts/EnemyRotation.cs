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
        while (true)
        {
            targetAngle = Random.Range(-maxRotationAngle, maxRotationAngle);
            Quaternion targetRotation = baseRotation * Quaternion.Euler(0f, targetAngle, 0f);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        }
    }
}
