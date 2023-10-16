using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    private Light[] lights;
    private float maxIntensity;

    [Tooltip("빛이 켜지기 위해 필요한 최소 거리, 큰 값")]
    public float lightStartDistance;

    [Tooltip("빛이 최대 밝기에 도달하기위해 필요한 거리, 작은 값")]
    public float lightMaxDistance;

    private float sqrLSD;
    private float sqrLMD;  

    private void Awake()
    {
        lights = GetComponentsInChildren<Light>();
        maxIntensity = lights[0].intensity;

        sqrLSD = lightStartDistance * lightStartDistance;
        sqrLMD = lightMaxDistance * lightMaxDistance;

        SetLightIntensity(0f);
    }
    private void SetLightIntensity(float intensity)
    {
        for(int i = 0; i < lights.Length; i++)
            lights[i].intensity = intensity;
    }
    private void Update()
    {
        float sqrDist = Vector3.SqrMagnitude(transform.position - PlayerTracker.GetPlayerPos());

        if(sqrDist > sqrLSD)
        {
            SetLightIntensity(0f);
            return;
        }
        float t = Mathf.InverseLerp(sqrLSD, sqrLMD, sqrDist);
        float currIntensity = Mathf.Lerp(0f, maxIntensity, t);
        SetLightIntensity(currIntensity);
    }
}
