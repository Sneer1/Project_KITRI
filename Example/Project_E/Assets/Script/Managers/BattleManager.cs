using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
	void Start ()
    {
        LoadBattle();
	}

    public void LoadBattle()
    {
        
        ActorManager.Instance.PlayerLoad();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
