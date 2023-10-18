using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorongAnimation : MonoBehaviour
{
    void Update()
    {
        transform.position = transform.position + Vector3.up * Mathf.Cos(Time.time) * 0.001f;
    }
}
