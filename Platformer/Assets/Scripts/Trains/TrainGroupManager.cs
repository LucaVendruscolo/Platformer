using UnityEngine;
using UnityEngine.AI;

public class TrainGroupManager : MonoBehaviour
{
    private bool trainsMoving = false;
    public string destroyTrainTag = "TrainDestroyer"; 

    public void ActivateAllTrains()
    {
        if (!trainsMoving)
        {
            foreach (Transform child in transform) 
            {
                NavMeshAgent childAgent = child.GetComponent<NavMeshAgent>();
                if (childAgent != null)
                {
                    childAgent.enabled = true;
                }

                TrainCollider trainCollider = child.GetComponent<TrainCollider>();
                if (trainCollider == null)
                {
                    trainCollider = child.gameObject.AddComponent<TrainCollider>();
                }

                trainCollider.destroyTrainTag = destroyTrainTag;

                Collider childCollider = child.GetComponent<Collider>();
                if (childCollider == null)
                {
                    childCollider = child.gameObject.AddComponent<BoxCollider>();
                    childCollider.isTrigger = true; 
                }
            }

            trainsMoving = true;
        }
    }
}
