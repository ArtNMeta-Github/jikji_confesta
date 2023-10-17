using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceChecker : MonoBehaviour
{
    [HideInInspector]
    public float SqrDist = float.MaxValue;     
    void Update()
    {
        SqrDist = Vector3.SqrMagnitude(transform.position - PlayerTracker.GetPlayerPos());
    }
}
