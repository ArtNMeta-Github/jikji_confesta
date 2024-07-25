using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainTrigger : MonoBehaviour
{
    bool isPlayerStay = false;
    public bool IsPlayerStay { get { return isPlayerStay; } }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
            isPlayerStay = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            isPlayerStay = false;
    }


}
