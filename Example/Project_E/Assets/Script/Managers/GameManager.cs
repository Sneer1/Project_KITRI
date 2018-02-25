using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        UI_Tools.Instance.ShowUI(E_UITYPE.PF_UI_TITLE);
    }

    //public void UIConversation_Change(E_TEXTTYPE _eSTextStage)
    //{
    //    StartCoroutine("waitUIConversationInit", _eSTextStage);
    //}

    //IEnumerator waitUIConversationInit(E_TEXTTYPE _eSTextStage)
    //{
    //    yield return new WaitForEndOfFrame();
    //    UI_Conversation.Instance.Init(_eSTextStage);
    //    GameObject.Find("Stage").GetComponent<Stage_Conversation>().SetStage();
    //}
}
