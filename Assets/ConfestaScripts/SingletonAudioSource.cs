using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAudioSource : MonoBehaviour
{
    public static SingletonAudioSource Instance { get; private set; }
    private AudioSource audioSource;
    private Coroutine clipChangeCoroutine;

    public float clipChangeTime = 1f;
    public float clipChangeDelay = 0.5f;

    public float loopInterval = 1.0f; 
    private float timeSinceLastLoop = 0;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlayClip(AudioClip clip)
    { 
        Instance.InstancedPlayClip(clip);
    }

    public void InstancedPlayClip(AudioClip clip)
    {
        if(clipChangeCoroutine != null) 
            StopCoroutine(clipChangeCoroutine);

        clipChangeCoroutine = StartCoroutine(ClipChangeCoroutine(clip));
    }

    private void Update()
    {
        // 현재 AudioSource가 재생 중이 아니면 루프 간격마다 다시 재생
        if (!audioSource.isPlaying)
        {
            timeSinceLastLoop += Time.deltaTime;
            if (timeSinceLastLoop >= loopInterval)
            {
                audioSource.Play();
                timeSinceLastLoop = 0;
            }
        }
    }

    private IEnumerator ClipChangeCoroutine(AudioClip clip)
    {
        float sourVolume = audioSource.volume;

        float timer = 0f;
        float reverseCT = 1 / clipChangeTime;

        while(timer < clipChangeTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(sourVolume, 0f, timer * reverseCT);

            yield return null;
        }

        //yield return new WaitForSeconds(clipChangeDelay);

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();
    }
}
