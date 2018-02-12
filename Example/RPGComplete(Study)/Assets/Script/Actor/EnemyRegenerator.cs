using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegenerator : BaseObject
{
    public GameObject EnemyPrefab;
    List<Actor> ListAttachEnemy = new List<Actor>();

    public ERegenType RegenType = ERegenType.NONE;
    public EMonsterType EnemyType = EMonsterType.Enemy_1;

    public int MaxObjectNum = 0;

    public float RegenTime = 300f;
    private float CurrTime = 0f;

    public float Radius = 15f;
    new SphereCollider collider = null;

    private void OnEnable()
    {
        EnemyPrefab = Resources.Load("Prefabs/" + EnemyType.ToString()) as GameObject;

        if(EnemyPrefab == null)
        {
            Debug.Log("에너미 프리팹 로드 실패");
            return;
        }

        switch (RegenType)
        {
            case ERegenType.REGENTTIME_EVENT:
                {
                    CurrTime = 0f;
                }
                break;
            case ERegenType.TRIGGER_EVENT:
                {
                    if (collider == null)
                        collider = this.gameObject.AddComponent<SphereCollider>();

                    collider.isTrigger = true;
                    collider.radius = Radius;
                }
                break;
        }
    }

    private void Update()
    {
        switch (RegenType)
        {
            case ERegenType.REGENTTIME_EVENT:
                {
                    if (RegenTime > CurrTime)
                        CurrTime += Time.deltaTime;
                    else
                    {
                        CurrTime = 0;
                        RegenEnemy();
                    }
                }
                break;
            case ERegenType.TRIGGER_EVENT:
                break;
        }
    }

    private void RegenEnemy()
    {
        for(int i = ListAttachEnemy.Count; i < MaxObjectNum; ++i)
        {
            Actor actor = ActorManager.Instance.InstantiateOnce(EnemyPrefab, SelfTransform.position + GetRandomPos());

            actor.ThrowEvent(ConstValue.EventKey_EnemyInit, this);

            ListAttachEnemy.Add(actor);
        }
    }

    Vector3 GetRandomPos()
    {
        Vector3 dir = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        return dir.normalized * UnityEngine.Random.Range(1, Radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (RegenType)
        {
            case ERegenType.REGENTTIME_EVENT:
                break;
            case ERegenType.TRIGGER_EVENT:
                {
                    Actor actor = other.gameObject.GetComponent<Actor>();
                    if(actor != null && actor.IsPlayer == true)
                    {
                        RegenEnemy();
                    }
                }
                break;
        }
    }

    public void RemoveActor(Actor actor)
    {
        if (ListAttachEnemy.Contains(actor) == true)
            ListAttachEnemy.Remove(actor);
    }
    
}
