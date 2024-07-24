using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float delay = 1f;
    public float time = 3f;

    public void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(CanvasCo());
    }

    private IEnumerator CanvasCo()
    {
        yield return new WaitForSeconds(delay);

        float revTime = 1 / time;

        float timer = 0f;

        while (timer < time) 
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer * revTime);

            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(10f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
