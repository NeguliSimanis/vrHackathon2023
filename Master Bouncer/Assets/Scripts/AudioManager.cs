using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource thisAudioSource;
    public AudioClip yaySFX;
    public AudioClip grabSFX;
    public AudioClip throwSFX;
    public AudioClip wrongSFX;
    public AudioClip gameOverSFX;

    private void Start()
    {
        thisAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayYaySFX()
    {
        thisAudioSource.PlayOneShot(yaySFX, 0.4f);
    }

    public void PlayWrongSFX()
    {
        thisAudioSource.PlayOneShot(wrongSFX, 0.3f);
    }

    public void PlayGameOverSFX()
    {
        thisAudioSource.PlayOneShot(gameOverSFX, 0.6f);
    }
}
