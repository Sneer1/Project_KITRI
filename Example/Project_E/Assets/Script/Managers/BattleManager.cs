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

    public E_TEXTTYPE E_NextStage
    {
        get;
        set;
    }

    public E_TEXTTYPE E_CurrStageEnd
    {
        get;
        set;
    }

    List<E_PLAYTYPE> _PlayerList = new List<E_PLAYTYPE>();
    List<E_ENEMYTYPE> _EnemyList = new List<E_ENEMYTYPE>();

    public List<E_PLAYTYPE> PlayerList
    {
        get { return _PlayerList; }
        set { _PlayerList = value;}
    }

    public List<E_ENEMYTYPE> EnemyList
    {
        get { return _EnemyList; }
        set { _EnemyList = value; }
    }
}
