using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance { get; private set; }
    private void Awake() => Instance = this;    
    public static Vector3 GetPlayerPos() => Instance.transform.position;
    public static void SetPlayerPos(Vector3 pos) => Instance.transform.position = pos;
}
