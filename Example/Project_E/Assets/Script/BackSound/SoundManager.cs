using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    List<AudioClip> PlayAudioList = new List<AudioClip>();

    public AudioSource effSource;
    public AudioSource BGSource;

    private void Awake()
    {
        BGSource = this.gameObject.AddComponent<AudioSource>();
        effSource = this.gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < (int)E_SOUND.MAX; ++i)
        {
            PlayAudioList.Add(Resources.Load("Sounds/" + ((E_SOUND)i).ToString()) as AudioClip);
        }
    }

    public void PlayBGM(E_SOUND _BGM)
    {
        BGSource.clip = PlayAudioList[(int)_BGM];
        BGSource.loop = true;
        BGSource.Play();
    }

    public void PlayEFF(E_SOUND _EFF)
    {
        effSource.clip = PlayAudioList[(int)_EFF];
        effSource.Play();
    }
}
