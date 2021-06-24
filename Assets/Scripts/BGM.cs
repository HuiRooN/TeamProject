using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 0.4f;

        audioSource.clip = audioClips[0];
        audioSource.Play();
        audioSource.loop = true;
    }



    public void Victory()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
        audioSource.loop = false;

    }

    public void Defeat()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
        audioSource.loop = false;

    }


}
