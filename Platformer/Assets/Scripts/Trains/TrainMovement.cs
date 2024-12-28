using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainMovement : MonoBehaviour
{
    private float trainSpeed;
    private NavMeshAgent agent;

    public float minTrainSpeed = 9f; // this is the medium difficulty range. set as default.
    public float maxTrainSpeed = 12f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // adjust the speed based on the difficutly selected.
        AdjustSpeedForDifficulty();

        // train speed randomisation
        trainSpeed = Random.Range(minTrainSpeed, maxTrainSpeed);
        agent.speed = trainSpeed;

        Debug.Log($"Train speed set to {trainSpeed} (Range: {minTrainSpeed}-{maxTrainSpeed}) for difficulty: {LevelSelector.selectedDifficulty}");
    }

    private void AdjustSpeedForDifficulty()
    {
        switch (LevelSelector.selectedDifficulty)
        {
            case LevelSelector.Difficulty.Easy:
                minTrainSpeed = 6f; // easy range
                maxTrainSpeed = 8f;
                break;
            case LevelSelector.Difficulty.Medium:
                minTrainSpeed = 9f; // medium range
                maxTrainSpeed = 12f;
                break;
            case LevelSelector.Difficulty.Hard:
                minTrainSpeed = 12f; // hard range
                maxTrainSpeed = 15f;
                break;
        }
    }
}
