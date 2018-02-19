using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSound : MonoSingleton<BackSound>
{
    GameObject MainPlaySound;
    AudioClip PlayAudio;
    public EMUSIC backsound;

    private void Awake()
    {
        Init();
    }

    new void Init()
    {
        MainPlaySound = Camera.main.gameObject;
        MainPlaySound.AddComponent<AudioSource>();
    }

    public void BackSoundPlay(EMUSIC eMUSIC)
    {
        backsound = eMUSIC;
        PlayAudio = Resources.Load<AudioClip>("Sound/"+ backsound.ToString());   
        MainPlaySound.GetComponent<AudioSource>().clip = PlayAudio;
        MainPlaySound.GetComponent<AudioSource>().Play();
    }
}
