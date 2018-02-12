using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ModelLoad : MonoSingleton<AI_ModelLoad>
{
    Dictionary<E_AIMODETYPE, GameObject> DicModel = new Dictionary<E_AIMODETYPE, GameObject>();

    private void Awake()
    {
        LoadAIModel();
    }

    public void LoadAIModel()
    {
        for (int i = 0; i < (int)E_AIMODETYPE.MAX; ++i)
        {
            GameObject go = Resources.Load("Prefabs/AI_Models/" + ((E_AIMODETYPE)i).ToString()) as GameObject;
            if (go == null)
            {
                Debug.LogError("프리팹 AI 모델 로드 실패");
                continue;

            }
            DicModel.Add((E_AIMODETYPE)i, go);
        }
    }

    public GameObject GetModel(E_AIMODETYPE type)
    {
        if (DicModel.ContainsKey(type))
        {
            return DicModel[type];
        }
        else
        {
            Debug.LogError("모델 로드 실패");
            return null;
        }
    }
}
