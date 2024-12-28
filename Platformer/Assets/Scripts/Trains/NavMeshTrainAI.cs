using UnityEngine;
using UnityEngine.AI;

public class NavMeshTrainAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] target;
    private TrainGroupManager groupManager; // the train's group
    private int targetIndex = 0;
    private const float targetReachedThreshold = 15.0f; 

    private void Start()
    {
        // navmesh disabled initiallty
        if (agent != null)
        {
            agent.enabled = false;
        }
        targetIndex = 0;

        // find the train's group manager
        groupManager = GetComponentInParent<TrainGroupManager>();
    }

    private void FixedUpdate()
    {
        if (agent != null && agent.enabled && target != null)
        {
            agent.SetDestination(target[targetIndex].position); // navmesh moving towards the target.
        }

        // Check if the train has reached the target
        if (!agent.pathPending && agent.remainingDistance <= targetReachedThreshold)
        {
            if(targetIndex != target.Length - 1)
            {
                targetIndex = targetIndex + 1;
            }
        }
    }

    // Detect when the player jumps onto the train
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.CompareTag("player"))
        {
            // tell the group manager to start all the trains
            if (groupManager != null)
            {
                groupManager.ActivateAllTrains(); // move all the trains
            } else{
                Debug.Log("group manager null");
            }
        }
    }
}
