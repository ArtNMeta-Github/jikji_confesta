using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrative : LightHandler
{
    public CanvasGroup[] canvasGroups;

    protected override void Update()
    {
        base.Update();

        float t = faceLight.intensity;

        for(int i = 0; i < canvasGroups.Length; i++)
        {
            canvasGroups[i].alpha = t;
        }
    }
}
