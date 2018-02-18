using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Conversation : MonoBehaviour 
{
	void Start () 
	{
        SetStage();
    }

    public void SetStage()
    {
        UI_Conversation.Instance.stagePrefab = Instantiate(Resources.Load("Prefabs/Stage/" + UI_Conversation.Instance.stageData.ToString())) as GameObject;
    }
}
