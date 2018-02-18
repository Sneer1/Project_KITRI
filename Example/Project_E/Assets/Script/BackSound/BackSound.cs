using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSound : MonoBehaviour
{
    GameObject MainPlaySound;
    AudioClip PlayAudio;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        MainPlaySound = Camera.main.gameObject;
        MainPlaySound.AddComponent<AudioSource>();
    }

    public void BackSoundPlay(EMUSIC backsound)
    {
        switch (backsound)
        {
            case EMUSIC.HANRAN_MUSIC_1:
                {
                    PlayAudio = Resources.Load<AudioClip>("Sound/MerryGoRoundLife");
                }
                break;
            case EMUSIC.IRIS_MUSIC_2:
                {
                    PlayAudio = Resources.Load<AudioClip>("Sound/MerryGoRoundLife");

                }
                break;
            case EMUSIC.TIBOUCHINA_MUSIC_3:
                {
                    PlayAudio = Resources.Load<AudioClip>("Sound/MerryGoRoundLife");
                }
                break;
            case EMUSIC.VERBENA_MUSIC_4:
                {
                    PlayAudio = Resources.Load<AudioClip>("Sound/MerryGoRoundLife");
                }
                break;
            default:
                Debug.LogError("오디오가 제대로 설정되지 않았습니다");
                break;
        }
        MainPlaySound.GetComponent<AudioSource>().clip = PlayAudio;
        MainPlaySound.GetComponent<AudioSource>().Play();
    }



}
