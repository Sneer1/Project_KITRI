using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StringData : MonoBehaviour
{
<<<<<<< HEAD
    string _StrKey = string.Empty;
    List<string> _StringData = new List<string>();

    public string StrKey { get { return _StrKey; } }
    public List<string> StringTextData { get { return _StringData; } }

    public StringData(string strKey, JSONNode nodeData)
    {
        _StrKey = strKey;
        _Range = nodeData["RANGE"].AsFloat;

        JSONArray arrSkill = nodeData["SKILL"].AsArray;
        if (arrSkill != null)
        {
            for (int i = 0; i < arrSkill.Count; ++i)
            {
                _SkillTemplateList.Add(arrSkill[i]);
            }
        }
    }
=======
    //string _StrKey = string.Empty;
    //List<string> _StringData = new List<string>();

    //public string StrKey { get { return _StrKey; } }
    //public List<string> StringTextData { get { return _StringData; } }

    //public StringData(string strKey, JSONNode nodeData)
    //{
    //    TextAsset stringAssetData = Resources.Load(ConstValue.TextDataPath) as TextAsset;

    //    if (stringAssetData == null)
    //    {
    //        Debug.LogError("스킬 데이터 로드 실패");
    //        return;
    //    }
    //    JSONNode rootNode = JSON.Parse(stringAssetData.text);
    //    if (rootNode == null)
    //        return;

    //    JSONObject stringDataNode = rootNode[ConstValue.SkillDataKey]


    //    _StrKey = strKey;
    //    _Range = nodeData["RANGE"].AsFloat;

    //    JSONArray arrSkill = nodeData["SKILL"].AsArray;
    //    if (arrSkill != null)
    //    {
    //        for (int i = 0; i < arrSkill.Count; ++i)
    //        {
    //            _SkillTemplateList.Add(arrSkill[i]);
    //        }
    //    }
    //}
>>>>>>> parent of c8ce933... 머지/인트로씬/대화씬
}
