using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAlphaController : MonoBehaviour
{
    Material[] materials;

    public float alphaChangeTime;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        for(int i = 0; i < materials.Length; i++)
        {
            var mat = materials[i];            
            mat.SetFloat("_Surface", 1.0f);            
            mat.SetFloat("_Blend", 0.0f);
        }
    }
    public void SetMaterialsMaxAlphaValue()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        SetMatrialsAlpha(1f);
    }
    public void StartFadeOut()
    {
        if(fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(MaterialFadeCoroutine());
    }

    private void SetMatrialsAlpha(float newAlpha)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            var mat = materials[i];
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
        }
    }

    IEnumerator MaterialFadeCoroutine()
    {
        float timer = 0f;
        float reverseT = 1 / alphaChangeTime;

        while(timer < alphaChangeTime)
        {
            timer += Time.deltaTime;

            float newAlpha = Mathf.Lerp(1f, 0f, timer * reverseT);
            SetMatrialsAlpha(newAlpha);

            yield return null;
        }

        SetMatrialsAlpha(0f);

        yield break;
    }
}
