using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomiser : MonoBehaviour
{
    public GameObject spawnZone;
    public GameObject wayPointObject;


    void Awake()
    {
        Debug.Log("Awake");
        wayPointObject.transform.position = selectRandomSpawnPoint();
    }


    private Vector3 selectRandomSpawnPoint()
    {
        BoxCollider boxCollider = spawnZone.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.Log("Collider could not be found");
            return transform.position;
        }

        Vector3 center = boxCollider.center + spawnZone.transform.position;
        Vector3 size = boxCollider.size;

        float randomX = UnityEngine.Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        Debug.Log("Random X: " + randomX);
        float randomY = center.y;
        float randomZ = UnityEngine.Random.Range(center.z - size.z / 2, center.z + size.z / 2);
        Debug.Log("Random Z: " + randomZ);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
        Debug.Log("Random Position: " + randomPosition);

        return randomPosition;
    }



}
