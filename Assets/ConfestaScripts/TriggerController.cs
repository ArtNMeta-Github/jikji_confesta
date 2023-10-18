using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private PlayerDistanceChecker distanceChecker;

    private Animator animator;
    private int hashStart = Animator.StringToHash("Start");

    public float startDistance = 4.5f;
    private float SqrSD;
    
    protected bool isPlaying = false;
    public AudioClip audioClip;    

    private void Awake()
    {
        distanceChecker = GetComponent<PlayerDistanceChecker>();
        animator = GetComponentInChildren<Animator>();
        SqrSD = startDistance * startDistance;
    }
    protected virtual void Update()
    {
        if (distanceChecker.SqrDist > SqrSD)
        {
            isPlaying = false;
            return;
        }

        if (distanceChecker.SqrDist < SqrSD && !isPlaying)
        {
            isPlaying = true;

            animator.SetTrigger(hashStart);   

            if (audioClip != null)
                SingletonAudioSource.PlayClip(audioClip);
        }
    }  
}
