using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    Actor PlayerActor = null;

    void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        PlayerActor = ActorManager.Instance.PlayerLoad();

        CameraManager.Instance.CameraInit(PlayerActor);
    }
}
