﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Conversation : MonoBehaviour
{
    void Start()
    {
        SetStage();
    }

    public void SetStage()
    {
        UI_Conversation.Instance.stagePrefab = Instantiate(Resources.Load("Prefabs/Stage/" + UI_Conversation.Instance.stageData.ToString())) as GameObject;

        if (UI_Conversation.Instance.stageData.ToString() == "STAGE2")
        {
            Skybox skyBox = Camera.main.gameObject.AddComponent<Skybox>();
            skyBox.material = Resources.Load("Materials/SkyBox_Night") as Material;
        }
        else if (UI_Conversation.Instance.stageData.ToString() == "STAGE3")
        {
            Skybox skyBox = Camera.main.gameObject.AddComponent<Skybox>();
            skyBox.material = Resources.Load("Materials/SkyBox_Sunny") as Material;
        }
    }
}
