using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClipBgr;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClipBgr;
        audioSource.volume = 0.03f;
        audioSource.loop = true;
        PlayAudio();
    }

    void PlayAudio()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
        }
    }
}
