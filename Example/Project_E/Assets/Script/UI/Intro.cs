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
        if (isOne == false && Intro_Text.Text.Count <= page_Index)
        {           
            Scene_Manager.Instance.LoadScene(E_SCENETYPE.SCENE_CONVERSATION, false);
            UI_Conversation.Instance.Init(E_TEXTTYPE.STAGE1_S);
            isOne = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            TextUI.text = Intro_Text.Text[page_Index];
            page_Index++;
        }
    }
}
