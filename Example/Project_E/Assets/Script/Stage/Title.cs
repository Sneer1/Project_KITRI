using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(E_SOUND.SOUND_BGM_TITLE);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Scene_Manager.Instance.LoadScene(E_SCENETYPE.SCENE_INTRO);            
            SoundManager.Instance.PlayEFF(E_SOUND.SOUND_EFF_NEXTSCENE);
        }
    }
}
