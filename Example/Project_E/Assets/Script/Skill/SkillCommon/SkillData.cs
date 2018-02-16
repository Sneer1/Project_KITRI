using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class SkillData
{
    string _StrKey = string.Empty;
    float _Range = 0;
    List<string> _SkillTemplateList = new List<string>();

    public string StrKey { get { return _StrKey; } }
    public float Range { get { return _Range; } }
    public List<string> SkillTemplateList { get { return _SkillTemplateList; } }

    public SkillData(string strKey, JSONNode nodeData)
    {
        TextAsset stringAssetData = Resources.Load(ConstValue.TextDataPath) as TextAsset;

        if(stringAssetData == null)
        {
            Debug.LogError("스킬 데이터 로드 실패");
            return;
        }
        JSONNode rootNode = JSON.Parse(stringAssetData.text);
        if (rootNode == null)
            return;

        JSONObject stringDataNode = rootNode[ConstValue.]


        _StrKey = strKey;
        _Range = nodeData["RANGE"].AsFloat;

        JSONArray arrSkill = nodeData["SKILL"].AsArray;
        if(arrSkill != null)
        {
            for (int i =0; i<arrSkill.Count; ++i)
            {
                _SkillTemplateList.Add(arrSkill[i]);
            }
        }
    }
}
