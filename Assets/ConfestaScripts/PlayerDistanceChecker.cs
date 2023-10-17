using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceChecker : MonoBehaviour
{
    public float SqrDist = float.MaxValue; 

    // Update is called once per frame
    void Update()
    {
        SqrDist = Vector3.SqrMagnitude(transform.position - PlayerTracker.GetPlayerPos());
    }
}
