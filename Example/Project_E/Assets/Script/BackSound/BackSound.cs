using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSound : MonoSingleton<BackSound>
{
    [SerializeField]
    AudioClip[] PlayAudio;

    public AudioSource musicSource;
    public EMUSIC backsound;

    private void Awake()
    {
        Init();
    }

    new void Init()
    {
        musicSource= this.gameObject.AddComponent<AudioSource>();
        musicSource.clip = PlayAudio[0];
        musicSource.Play();
    }
}
