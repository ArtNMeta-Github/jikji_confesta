using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour
{   
    private PlayerDistanceChecker distanceChecker;

    public Light[] spotLights;
    public Light faceLight;
    private float maxIntensity;

    [Tooltip("���� ������ ���� �ʿ��� �ּ� �Ÿ�, ū ��")]
    public float lightStartDistance;

    [Tooltip("���� �ִ� ��⿡ �����ϱ����� �ʿ��� �Ÿ�, ���� ��")]
    public float lightMaxDistance;

    private float sqrLSD;
    private float sqrLMD;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        distanceChecker = GetComponent<PlayerDistanceChecker>();

        spotLights = GetComponentsInChildren<Light>();
        maxIntensity = spotLights[0].intensity;

        sqrLSD = lightStartDistance * lightStartDistance;
        sqrLMD = lightMaxDistance * lightMaxDistance;

        SetSpotLightIntensity(0f);
        faceLight.intensity = 0f;

        canvasGroup = GetComponentInChildren<CanvasGroup>();

        if (canvasGroup != null)
            canvasGroup.alpha = 0f;
    }
    private void SetSpotLightIntensity(float intensity)
    {
        for(int i = 0; i < spotLights.Length; i++)
            spotLights[i].intensity = intensity;
    }
    private void Update()
    {
        float sqrDist = distanceChecker.SqrDist;

        if (sqrDist > sqrLSD)
        {
            SetSpotLightIntensity(0f);
            faceLight.intensity = 0f;
            return;
        }

        float t = Mathf.InverseLerp(sqrLSD, sqrLMD, sqrDist);
        float currIntensity = Mathf.Lerp(0f, maxIntensity, t);
        SetSpotLightIntensity(currIntensity);
        faceLight.intensity = t;

        if (canvasGroup != null)
            canvasGroup.alpha = t;
    }
}
