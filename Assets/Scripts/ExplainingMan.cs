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
        
        for(int i=0;i< parentsAnimObjects[positionIndex].transform.childCount;i++)
        {
            enableObjectList.Add(parentsAnimObjects[positionIndex].transform.GetChild(i));
            parentsAnimObjects[positionIndex].transform.GetChild(i).gameObject.SetActive(true);
        }

        //foreach(Transform obj in parentsAnimObjects[positionIndex].transform.chil)
        //{
        //    enableObjectList.Add(obj);
        //    obj.SetParent(transform);
        //}
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

    [ContextMenu("log")]
    public void PrintIndex()
    {
        print(currentPositionIndex);
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
