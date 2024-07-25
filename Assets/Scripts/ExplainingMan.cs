using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class ExplainingMan : MonoBehaviour
{
    [Serializable]
    public struct AnimationObject
    {
        public GameObject[] animToolObjects;
    }

    int positionIndex;
    Animator manAnimator;
    List<GameObject> enableObjectList = new List<GameObject>();

    [SerializeField] AnimationObject[] animObjects;

    
    void ResetObjectList() 
    {
        foreach (var obj in enableObjectList) obj.SetActive(false);

        enableObjectList.Clear();
    }
    public void AddEnableObjectList(int positionIndex) 
    {
        enableObjectList = animObjects[positionIndex].animToolObjects.ToList();
        foreach (var temp in enableObjectList)
            temp.SetActive(true);
    }
    public void PlayAnimation() => manAnimator.StartPlayback();
    public void StopAnimation() => manAnimator.StopPlayback();

    public void SetAnimation(int positionIndex, AnimatorController explainAnimController)
    {
        if (manAnimator.runtimeAnimatorController == null || manAnimator.runtimeAnimatorController != explainAnimController)
            manAnimator.runtimeAnimatorController = explainAnimController;

        ResetObjectList();
        AddEnableObjectList(positionIndex);
    }




    // Start is called before the first frame update
    void Start()
    {
        manAnimator = GetComponent<Animator>();
    }
}
