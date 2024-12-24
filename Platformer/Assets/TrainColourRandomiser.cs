using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainColourRandomiser : MonoBehaviour
{

    public Material[] trainColours;
    public GameObject train;

    void Awake()
    {
        Material trainColour = trainColours[Random.Range(0, trainColours.Length)];
        train.GetComponent<Renderer>().material = trainColour;
    }

}
