using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {

    }

    public void UIConversation_Change(E_TEXTTYPE _eSTextStage)
    {
        StartCoroutine("waitUIConversationInit", _eSTextStage);
    }

    IEnumerator waitUIConversationInit(E_TEXTTYPE _eSTextStage)
    {
        yield return new WaitForEndOfFrame();
        UI_Conversation.Instance.Init(E_TEXTTYPE.STAGE2_S);
        GameObject.Find("Stage").GetComponent<Stage_Conversation>().SetStage();
    }
}
