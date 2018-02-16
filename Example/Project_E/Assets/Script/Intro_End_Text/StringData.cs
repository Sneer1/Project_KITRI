using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StringData : MonoBehaviour
{
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
}
