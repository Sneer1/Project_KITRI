using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Battle : MonoBehaviour 
{
    [SerializeField]
    private Transform[] PlayGen;
    [SerializeField]
    private Transform[] EnemyGen;

    int nPlayerGenIndex = 0;
    int nEnemyGenIndex = 0;

    List<Actor> temp_ActorList;

    private void Update()
    {
        ActorManager.Instance.GetDicActor.TryGetValue(E_TEAMTYPE.TEAM_1, out temp_ActorList);
        if (temp_ActorList.Count <= 0)
            Debug.Log("gameOver");

        ActorManager.Instance.GetDicActor.TryGetValue(E_TEAMTYPE.TEAM_2, out temp_ActorList);
        if(temp_ActorList.Count <= 0)
        {
            Scene_Manager.Instance.LoadScene(E_SCENETYPE.SCENE_CONVERSATION, false);
            Scene_Manager.Instance.UpdateScene();
            UI_Conversation.Instance.Init(E_TEXTTYPE.STAGE1_E);
        }
    }

    private void Awake()
    {
        LoadBattle(BattleManager.Instance.PlayerList, BattleManager.Instance.EnemyList);
    }

    public void LoadBattle(List<E_PLAYTYPE> _ePlayer, List<E_ENEMYTYPE> _eEnemy)
    {
        foreach (E_PLAYTYPE list in _ePlayer)
        {
            SpawnPlayer(list);
        }

        foreach (E_ENEMYTYPE list in _eEnemy)
        {
            SpawnEnemy(list);
        }
    }

    private void SpawnPlayer(E_PLAYTYPE _ePlayer)
    {
        GameObject playerPrefab = ActorManager.Instance.GetPlayerPrefab(_ePlayer);
        GameObject go = Instantiate(playerPrefab, PlayGen[nPlayerGenIndex].position, Quaternion.identity) as GameObject;
        nPlayerGenIndex++;
    }

    private void SpawnEnemy(E_ENEMYTYPE _eEnemy)
    {
        GameObject EnemyPrefab = ActorManager.Instance.GetEnemyPrefab(_eEnemy);
        GameObject go = Instantiate(EnemyPrefab, EnemyGen[nEnemyGenIndex].position, Quaternion.identity) as GameObject;
        nEnemyGenIndex++;
    }
}
