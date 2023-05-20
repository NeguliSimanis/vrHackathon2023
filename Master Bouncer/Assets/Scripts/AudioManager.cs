using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource thisAudioSource;
    public AudioClip yaySFX;
    public AudioClip grabSFX;
    public AudioClip throwSFX;

    private void Start()
    {
        thisAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayYaySFX()
    {
        thisAudioSource.PlayOneShot(yaySFX, 0.6f);
    }

}
