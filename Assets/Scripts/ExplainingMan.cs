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

    int currentPositionIndex = -1;
    Animator manAnimator;
    List<Transform> enableObjectList = new List<Transform>();

    [SerializeField] GameObject[] parentsAnimObjects;

    
    void ResetObjectList() 
    {
        foreach (var obj in enableObjectList) obj.SetParent(parentsAnimObjects[currentPositionIndex].transform);

        enableObjectList.Clear();
    }

    public void AddEnableObjectList(int positionIndex) 
    {
        this.currentPositionIndex = positionIndex;
        int tempChildCount = parentsAnimObjects[positionIndex].transform.childCount;

        for (int i = 0; i < tempChildCount; i++)
        {
            enableObjectList.Add(parentsAnimObjects[positionIndex].transform.GetChild(0));
            parentsAnimObjects[positionIndex].transform.GetChild(0).gameObject.SetActive(true);
            parentsAnimObjects[positionIndex].transform.GetChild(0).SetParent(transform);
        }
    }

    public void PlayAnimation() => manAnimator.StartPlayback();
    public void StopAnimation() => manAnimator.StopPlayback();

    public void SetAnimation(int positionIndex, AnimatorController explainAnimController)
    {
        ResetObjectList();
        AddEnableObjectList(positionIndex);

        if (manAnimator.runtimeAnimatorController == null || manAnimator.runtimeAnimatorController != explainAnimController)
            manAnimator.runtimeAnimatorController = explainAnimController;

    }

    [ContextMenu("log")]
    public void PrintIndex()
    {
        print(enableObjectList.Count);
    }
    

    private void Awake()
    {
        manAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        manAnimator = GetComponent<Animator>();
    }


}
