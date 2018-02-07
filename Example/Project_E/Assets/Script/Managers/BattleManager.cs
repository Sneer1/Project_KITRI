using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    [SerializeField]
    private Transform[] PlayGen;
    [SerializeField]
    private Transform[] EnemyGen;

    int nPlayerGenIndex = 0;
    int nEnemyGenIndex = 0;

    void Start ()
    {
        List<E_PLAYTYPE> listPlayer = new List<E_PLAYTYPE>();
        listPlayer.Add(E_PLAYTYPE.PF_CHARACTER_HANRAN);
        listPlayer.Add(E_PLAYTYPE.PF_CHARACTER_IRIS);

        List<E_ENEMYTYPE> listEnemy = new List<E_ENEMYTYPE>();
        listEnemy.Add(E_ENEMYTYPE.PF_ENEMY_BLUE);
        listEnemy.Add(E_ENEMYTYPE.PF_ENEMY_RED);

        LoadBattle(listPlayer, listEnemy);
	}

    public void LoadBattle(List<E_PLAYTYPE> _ePlayer, List<E_ENEMYTYPE> _eEnemy)
    {
        foreach(E_PLAYTYPE list in _ePlayer)
        {
            SpawnPlayer(list);
        }

        foreach (E_ENEMYTYPE list in _eEnemy)
        {
            SpawnEnemy(list);
        }
    }

    public void SpawnPlayer(E_PLAYTYPE _ePlayer)
    {
        GameObject playerPrefab = ActorManager.Instance.GetPlayerPrefab(_ePlayer);
        GameObject go = Instantiate(playerPrefab, PlayGen[nPlayerGenIndex].position, Quaternion.identity) as GameObject;
        nPlayerGenIndex++;
    }

    public void SpawnEnemy(E_ENEMYTYPE _eEnemy)
    {
        GameObject EnemyPrefab = ActorManager.Instance.GetEnemyPrefab(_eEnemy);
        GameObject go = Instantiate(EnemyPrefab, EnemyGen[nEnemyGenIndex].position, Quaternion.identity) as GameObject;
        nEnemyGenIndex++;
    }
}
