using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAlphaController : MonoBehaviour
{
    private List<Material> materials = new List<Material>();

    public float alphaChangeTime;
    private Coroutine fadeCoroutine;

    private bool isFaded = false;

    private void Awake()
    {
        var renderes = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderes.Length; i++)
        {
            materials.AddRange(renderes[i].materials);
        }
    }
    public void StartFadeIn()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        SetMatrialsAlpha(0f);

        if (isFaded)
            fadeCoroutine = StartCoroutine(MaterialFadeCoroutine(false));
    }
    public void StartFadeOut()
    {
        if(fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(MaterialFadeCoroutine());
    }

    private void SetMatrialsAlpha(float newAlpha)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("Dissolve", newAlpha);
        }
    }

    IEnumerator MaterialFadeCoroutine(bool fadeOut = true)
    {
        isFaded = fadeOut;

        float timer = 0f;
        float reverseT = 1 / alphaChangeTime;

        float sour = fadeOut ? 0f : 1f;
        float dest = fadeOut ? 1f : 0f;

        SetMatrialsAlpha(sour);

        while (timer < alphaChangeTime)
        {
            timer += Time.deltaTime;

            float newAlpha = Mathf.Lerp(sour, dest, timer * reverseT);
            SetMatrialsAlpha(newAlpha);

            yield return null;
        }

        SetMatrialsAlpha(dest);

        yield break;
    }
}
