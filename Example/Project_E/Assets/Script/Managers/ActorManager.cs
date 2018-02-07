using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoSingleton<ActorManager>
{
    Dictionary<E_TEAMTYPE, List<Actor>> DicActor = new Dictionary<E_TEAMTYPE, List<Actor>>();

    Transform ActorRoot = null;

    Dictionary<E_ENEMYTYPE, GameObject> DicEnemyPrefab = new Dictionary<E_ENEMYTYPE, GameObject>();

    private void Awake()
    {
        EnemyPrefabInit();
    }

    void EnemyPrefabInit()
    {
        for(int i =0; i < (int)E_ENEMYTYPE.MAX; ++i)
        {
            GameObject go = Resources.Load("Prefabs/Monsters/" + ((E_ENEMYTYPE)i).ToString()) as GameObject;

            if(go == null)
            {
                Debug.LogError(((E_ENEMYTYPE)i).ToString() + "로드 실패");
            }
            else
            {
                DicEnemyPrefab.Add((E_ENEMYTYPE)i, go);
            }

        }
    }

    public GameObject GetEnemyPrefab(E_ENEMYTYPE type)
    {
        if(DicEnemyPrefab.ContainsKey(type) == true)
        {
            return DicEnemyPrefab[type];
        }
        else
        {
            Debug.LogError(type.ToString() + "해당 타입 프리팹이 없습니다");
            return null;
        }
    }

    public Actor PlayerLoad()
    {
        GameObject playerPrefab = Resources.Load("Prefabs/Actor/" + "Iris") as GameObject;

        GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        return go.GetComponent<Actor>();
    }

    public Actor InstantiateOnce(GameObject prefab, Vector3 pos)
    {
        if (prefab == null)
        {
            Debug.LogError("프리팹이 널값입니다 액터매니저의 인스턴시에이트");
        }

        GameObject go = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

        if(ActorRoot == null)
        {
            GameObject temp = new GameObject("ActorRoot");
            ActorRoot = temp.transform;
        }

        go.transform.SetParent(ActorRoot);
        return go.GetComponent<Actor>();
    }

    public void AddActor(Actor actor)
    {
        List<Actor> listActor = null;
        E_TEAMTYPE teamType = actor.TeamType;

        if(DicActor.ContainsKey(teamType) == false)
        {
            listActor = new List<Actor>();
            DicActor.Add(teamType, listActor);
        }
        else
        {
            //listActor = DicActor[teamType];
            DicActor.TryGetValue(teamType, out listActor);
        }

        listActor.Add(actor);
    }

    public void RemoveActor(Actor actor, bool bDelete = false)
    {
        E_TEAMTYPE teamType = actor.TeamType;

        if(DicActor.ContainsKey(teamType) == true)
        {
            List<Actor> listActor = null;
            DicActor.TryGetValue(teamType, out listActor);
            listActor.Remove(actor);
        }
        else
        {
            Debug.LogError("존재하지 않는 엑터를 삭제하려고 합니다");
        }

        if(bDelete)
        {
            Destroy(actor.gameObject);
        }
    }

    public BaseObject GetSearchEnemy(BaseObject actor, float radius = 100.0f)
    {
        E_TEAMTYPE teamtype = (E_TEAMTYPE)actor.GetData(ConstValue.ActorData_Team);

        Vector3 myPosition = actor.SelfTransform.position;

        float nearDistance = radius;
        Actor nearActor = null;

        foreach(KeyValuePair<E_TEAMTYPE,List<Actor>> pair in DicActor)
        {
            if (pair.Key == teamtype)
                continue;

            List<Actor> listActor = pair.Value;

            for (int i = 0; i < listActor.Count; ++i)
            {
                if (listActor[i].SelfObject.activeSelf == false)
                    continue;

                if (listActor[i].ObjectState == E_BASEOBJECTSTATE.STATE_DIE)
                    continue;

                float distance = Vector3.Distance(myPosition, listActor[i].SelfTransform.position);

                if(distance < nearDistance)
                {
                    nearDistance = distance;
                    nearActor = listActor[i];
                }
            }
        }
        
        return nearActor;
    }

}