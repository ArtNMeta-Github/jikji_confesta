using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAudioSource : MonoBehaviour
{
    public static SingletonAudioSource Instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlayClip(AudioClip clip)
    {
        var audioSource = Instance.audioSource;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
