using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private PlayerDistanceChecker distanceChecker;

    public Animator animator;
    private int hashStart = Animator.StringToHash("Start");

    public float startDistance = 4.5f;
    private float SqrSD;
    
    private bool isPlaying = false;

    private void Awake()
    {
        distanceChecker = GetComponent<PlayerDistanceChecker>();        
        SqrSD = startDistance * startDistance;
    }
    private void Update()
    {
        if (distanceChecker.SqrDist > SqrSD)
        {
            isPlaying = false;
            return;
        }

        if (distanceChecker.SqrDist < SqrSD && !isPlaying)
        {
            animator.SetTrigger(hashStart);
            isPlaying = true;
        }
    }
}
