using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip[] Concrete;
    public AudioClip[] Metal;
    public AudioClip[] Wood;
    public AudioClip[] Dirt;
    public AudioClip[] Sand;

    public AudioClip[][] allClips = { };

    private void Start()
    {
        allClips = new AudioClip[][]
        {
            Concrete,
            Metal,
            Wood,
            Dirt,
            Sand
        };
    }
    public void PlayRandom(AudioClip[] clip)
    {
        if (Source.isPlaying)
        {

        }
        else
        {
            Source.pitch = Random.Range(0.3f, 0.7f);
            Source.volume = Random.Range(0.1f, 1.0f);
            int i = Random.Range(0, clip.Length);
            Source.PlayOneShot(clip[i]);
        }
    }
}
