using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CharacterTemplateData
{
    string _StrKey = string.Empty;
    StatusData _StatusData = new StatusData();
    List<string> _ListSkill = new List<string>();

    public string StrKey { get { return _StrKey; } }
    public StatusData StatusData { get { return _StatusData; } }
    public List<string> ListSkill { get { return _ListSkill; } }

    public CharacterTemplateData(string strKey, JSONNode nodeData)
    {
        _StrKey = strKey;

        for(int i = 0; i < (int)EStatusData.MAX; ++i)
        {
            EStatusData eStatus = (EStatusData)i;
            double valueData = nodeData[eStatus.ToString("F")].AsDouble;
            _StatusData.IncreaseData(eStatus, valueData);
        }

        JSONArray arrSkill = nodeData["SKILL"].AsArray;
        if(arrSkill != null)
        {
            for(int i = 0; i < arrSkill.Count; ++i)
            {
                ListSkill.Add(arrSkill[i]);
            }
        }
    }
}