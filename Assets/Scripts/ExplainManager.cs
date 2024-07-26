using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class ExplainManager : MonoBehaviour
{
    [Serializable]
    public struct ExplainSource
    {
        public bool isAnimationPlay;

        public Transform positionTransform;
        public RuntimeAnimatorController animatorController;
        public ExplainTrigger trigger;

        [HideInInspector] public ExplainingMan explainHuman;
    }

    int tempInt;
    int forTempInt;

    [SerializeField] ExplainingMan[] workingMans;

    [SerializeField] ExplainSource[] explainSources;

    void SetHumanTransform(ExplainingMan human, Transform moveTransform) => human.transform.SetPositionAndRotation(moveTransform.position, moveTransform.rotation);

    void MoveHuman(int centerPositionIndex)
    {
        if (centerPositionIndex - 1 < 0)
            return;
        if (centerPositionIndex + 1 >= explainSources.Length)
            return;

        if(explainSources[centerPositionIndex - 1].explainHuman == null)
        {
            explainSources[centerPositionIndex - 1].explainHuman = explainSources[centerPositionIndex + 2].explainHuman;
            explainSources[centerPositionIndex + 2].explainHuman = null;
            explainSources[centerPositionIndex - 1].explainHuman.SetAnimation(centerPositionIndex - 1, explainSources[centerPositionIndex - 1].animatorController);
            SetHumanTransform(explainSources[centerPositionIndex - 1].explainHuman, explainSources[centerPositionIndex - 1].positionTransform);
        }

        if (explainSources[centerPositionIndex + 1].explainHuman == null)
        {
            explainSources[centerPositionIndex + 1].explainHuman = explainSources[centerPositionIndex - 2].explainHuman;
            explainSources[centerPositionIndex - 2].explainHuman = null;
            explainSources[centerPositionIndex + 1].explainHuman.SetAnimation(centerPositionIndex + 1, explainSources[centerPositionIndex + 1].animatorController);
            SetHumanTransform(explainSources[centerPositionIndex + 1].explainHuman, explainSources[centerPositionIndex + 1].positionTransform);
        }


    }

    private void Start()
    {
        for(int i = 0; i < workingMans.Length; i++)
        {
            explainSources[i].explainHuman = workingMans[i];
            SetHumanTransform(workingMans[i], explainSources[i].positionTransform);
            explainSources[i].explainHuman.SetAnimation(i, explainSources[i].animatorController);
        }
    }


    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < explainSources.Length; i++)
        {
            if (explainSources[i].trigger.IsPlayerStay)
            {
                MoveHuman(i);
                explainSources[i].explainHuman.ResumePlaying();
            }
            else
            {
                if (explainSources[i].explainHuman != null)
                    explainSources[i].explainHuman.StopPlaying();
            }
        }

        //if (ExplainTrigger.IsAllTrigger)
        //{
            
        //}
        
    }
}
