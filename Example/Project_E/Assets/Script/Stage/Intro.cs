using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    Text_Character Intro_Text;
    Text TextUI;
    int page_Index = 0;
    bool isOne = false;

    private void Awake()
    {
        TextUI = GetComponent<Text>();
        Intro_Text = TextLoad.Instance.GetText_Stage(E_TEXTTYPE.INTRO.ToString());

        TextUI.text = Intro_Text.Text[page_Index];
        page_Index++;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (isOne == false && Intro_Text.Text.Count <= page_Index)
            {
                SoundManager.Instance.PlayEFF(E_SOUND.SOUND_EFF_NEXTSCENE);
                Scene_Manager.Instance.LoadScene(E_SCENETYPE.SCENE_CONVERSATION);                
                isOne = true;
            }

            if (isOne == false)
            {
                SoundManager.Instance.PlayEFF(E_SOUND.SOUND_EFF_NEXTTEXT);               
                TextUI.text = Intro_Text.Text[page_Index];
                page_Index++;
            }
        }
    }
}
