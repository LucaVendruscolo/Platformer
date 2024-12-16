using UnityEngine;

public class TrainCollider : MonoBehaviour
{
    public string destroyTrainTag = "TrainDestroyer";

    private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag(destroyTrainTag))
        {
            Destroy(gameObject); 
        }
    }
}
