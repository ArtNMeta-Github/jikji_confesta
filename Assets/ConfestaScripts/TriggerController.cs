using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    float sqrDist = float.MaxValue;


    private Animator animator;
    private int hashStart = Animator.StringToHash("Start");

    public float startDistance = 4.5f;
    private float SqrSD;
    
    protected bool isPlaying = false;
    public AudioClip audioClip;    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        SqrSD = startDistance * startDistance;
    }

    
    protected virtual void Update()
    {
        sqrDist = Vector3.SqrMagnitude(transform.position - PlayerTracker.GetPlayerPos());

        if (sqrDist > SqrSD)
        {
            isPlaying = false;
            return;
        }

        if (sqrDist < SqrSD && !isPlaying)
        {
            isPlaying = true;

            animator.SetTrigger(hashStart);   

            if (audioClip != null)
                SingletonAudioSource.PlayClip(audioClip);
        }
    }  
}
