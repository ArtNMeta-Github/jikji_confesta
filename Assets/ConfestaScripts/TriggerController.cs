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
    
    private bool isPlaying = false;
    public AudioClip audioClip;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        distanceChecker = GetComponent<PlayerDistanceChecker>();
        animator = GetComponentInChildren<Animator>();
        SqrSD = startDistance * startDistance;
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    private void Update()
    {
        if (distanceChecker.SqrDist > SqrSD)
        {
            isPlaying = false;

            if(canvasGroup  != null) 
                canvasGroup.alpha = 0f;
            return;
        }

        if (distanceChecker.SqrDist < SqrSD && !isPlaying)
        {
            isPlaying = true;

            animator.SetTrigger(hashStart);

            if (canvasGroup != null)
                canvasGroup.alpha = 1f;

            if (audioClip != null)
                SingletonAudioSource.PlayClip(audioClip);
        }
    }  
}
