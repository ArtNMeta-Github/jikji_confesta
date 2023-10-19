using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Tutorial : MonoBehaviour
{
    public Light[] lights;
    public Light[] pointLights;
    public float eventTime = 3f;

    public CanvasGroup[] canvasGroups;

    float startIntensity;

    public Transform playerMovePos;

    public PlayerTranslater translater1;
    public PlayerTranslater translater2;
    private void Awake()
    {
        startIntensity = lights[0].intensity;
    }

    private void Start()
    {
        translater1.MovePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tutorial"))
        {
            StartCoroutine(LightOffCoroutine());                
        }
    }

    private void SetLightIntensity(float intensity)
    {
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = intensity;
        }
    }

    private void SetCanvasGroupsAlpha(float alpha)
    {
        for(int i  =0; i < canvasGroups.Length; i++)
        {
            canvasGroups[i].alpha = alpha;
        }
    }

    IEnumerator LightOffCoroutine()
    {
        float timer = 0f;
        float ivEvtTime = 1 / eventTime;

        while(timer < eventTime)
        {
            float t = timer * ivEvtTime;

            var intensity = Mathf.Lerp(startIntensity, 0f, t);
            SetLightIntensity(intensity);

            for(int i = 0; i < 2; i++)
            {
                pointLights[i].intensity = 1 - t;
            }

            SetCanvasGroupsAlpha(1 - t);

            timer += Time.deltaTime;
            yield return null;
        }

        translater2.MovePlayer();
    }
}
