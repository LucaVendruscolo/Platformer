using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject energyPrefab;
    public GameObject spawnArea;
    public GameObject emergencySpawnArea;
    public GameObject player;
    public float spawnDistance = 20f; // Distance from the player 


    private void OnEnable()
    {
        BarEventManager.SliderSpawnEnergy += BarEventManager_OnSliderSpawnEnergy;
    }

    private void OnDisable()
    {
        BarEventManager.SliderSpawnEnergy -= BarEventManager_OnSliderSpawnEnergy;
    }


    private void Update()
    {
        UpdateSpawnAreaPositionAndRotation();
    }

    private void UpdateSpawnAreaPositionAndRotation()
    {
        if (cameraTransform != null && player != null && spawnArea != null)
        {
            // Calculate the new position for the spawn area
            Vector3 newPosition = player.transform.position + cameraTransform.forward * spawnDistance;
            spawnArea.transform.position = newPosition;

            // Update the rotation of the spawn area to match the camera's rotation
            spawnArea.transform.rotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
        }
    }

    private void SpawnEnergy()
    {
        Vector3 randomPosition;
        int maxAttempts = 20;
        int attempts = 0;
        Debug.Log("Trying to spawn energy");
        //Keep trying to find a valid position to spawn the orb
        do
        {
            randomPosition = GetRandomPositionInSpawnArea();
            Debug.Log("Trying to spawn energy at: " + randomPosition);
            attempts++;
        } while ((IsPositionOccupied(randomPosition) || !IsPathClearToPlayer(randomPosition)) && attempts < maxAttempts);

        Vector3 directionToPlayer = Vector3.zero;

        if (attempts < maxAttempts)
        {
            //Try to randomise a spawn within the spawn area
            Debug.Log("Found a valid position to spawn the orb");
            directionToPlayer = player.transform.position - randomPosition;
        }
        else {
            //If we can't find a valid position to spawn the orb, we spawn it in the emergency spawn area
            Debug.Log("Couldn't find a valid position to spawn the orb, spawning in front of the player");
            randomPosition = emergencySpawnArea.transform.position;
            directionToPlayer = player.transform.position - randomPosition;
        }
        //Create the orb and make it face the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        Debug.Log("Energy spawned at: " + randomPosition);
        Instantiate(energyPrefab, randomPosition, lookRotation);

    }

    private bool IsPositionOccupied(Vector3 position)
    {
        //Check if the place we want to spawn the orb in has another gameobject in it
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f); // Adjust the radius as needed
        bool occupied = colliders.Length > 0;
        Debug.Log("Is position occupied: " + occupied);
        return occupied;
    }

    private bool IsPathClearToPlayer(Vector3 position)
    {
        //We shoot a ray cast from the position we want to spawn the orb in to the player
        //This is to make sure that the player can see the orb when it spawns
        Ray ray = new Ray(position, player.transform.position - position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the hit object is not the player and not the energy prefab
            bool obstructed = hit.collider.gameObject != player;
            Debug.Log("Is path clear to player: " + !obstructed);
            return !obstructed;
        }


        return true;
    }

    private Vector3 GetRandomPositionInSpawnArea()
    {
        BoxCollider boxCollider = spawnArea.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            return transform.position;
        }

        Vector3 center = boxCollider.center + spawnArea.transform.position;
        Vector3 size = boxCollider.size;

        float randomX = UnityEngine.Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = center.y;
        float randomZ = UnityEngine.Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        return randomPosition;
    }

    private void BarEventManager_OnSliderSpawnEnergy(){SpawnEnergy();}
}
