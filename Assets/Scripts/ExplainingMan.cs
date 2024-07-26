using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

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
    [SerializeField] GameObject paper;
    [SerializeField] GameObject paper_print;


    void ResetObjectList()
    {
        foreach (var obj in enableObjectList)
        { 
            obj.SetParent(parentsAnimObjects[currentPositionIndex].transform);
            obj.gameObject.SetActive(false);
        }
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

    public void SetAnimation(int positionIndex, RuntimeAnimatorController explainAnimController)
    {
        ResetObjectList();
        AddEnableObjectList(positionIndex);

        if (manAnimator.runtimeAnimatorController == null || manAnimator.runtimeAnimatorController != explainAnimController)
        {
            manAnimator.runtimeAnimatorController = explainAnimController;
            PlaySetting();
            StopPlaying();
        }

    }

    [ContextMenu("Play")]
    public void PlaySetting()
    {
        //manAnimator.SetTrigger("Start");
        manAnimator.Play("Scene");

    }
    [ContextMenu("Stop")]
    public void StopPlaying()
    {
        manAnimator.speed = 0;
    }
    [ContextMenu("Resume")]
    public void ResumePlaying()
    {
        manAnimator.speed = 1;
    }

    
    public void StartFadeIn() { }
    public void StartFadeOut() { }

    public void FocusPaper()
    {
        paper.SetActive(true);
        paper_print.SetActive(false);
    }
    public void FocusPrint()
    {
        paper.SetActive(false);
        paper_print.SetActive(true);
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
