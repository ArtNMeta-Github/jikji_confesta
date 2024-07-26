using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class ExplainTrigger : MonoBehaviour
{
    static bool isAllTrigger = false;
    static int activatedTriggerCount = 0;

    bool isPlayerStay = false;

    AudioSource narrationSound;

    [SerializeField] UnityEvent stayEvent,exitEvent;
    public static bool IsAllTrigger { get { return isAllTrigger; } }
    public bool IsPlayerStay { get { return isPlayerStay; } }

    private void Start()
    {
        narrationSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (activatedTriggerCount == 0)
            isAllTrigger = false;
        else
            isAllTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && !(isPlayerStay))
        {
            
            print(other.name);
            isPlayerStay = true;
            activatedTriggerCount++;
            print(activatedTriggerCount.ToString());
            stayEvent.Invoke();
            if(!(narrationSound.isPlaying))
                narrationSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerStay = false;
            activatedTriggerCount--;
            print(activatedTriggerCount.ToString());
            exitEvent.Invoke();
            narrationSound.Stop();
        }
    }


}
