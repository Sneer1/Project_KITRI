using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class SkillTemplate
{
    string _StrKey = string.Empty;
    E_SKILLTEMPLATETYPE _SkillType = E_SKILLTEMPLATETYPE.TARGET_ATTACK;
    E_SKILLRANGETYPE _RangeType = E_SKILLRANGETYPE.RANGE_BOX;

    float _RangeData_1 = 0;
    float _RangeData_2 = 0;

    StatusData _SkillStatus = new StatusData();

    public string StrKey { get { return _StrKey; } }
    public E_SKILLTEMPLATETYPE SkillType { get { return _SkillType; } }
    public E_SKILLRANGETYPE RangeType { get { return _RangeType; } }

    public float RangeData_1 { get { return _RangeData_1; } }
    public float RangeData_2 { get { return _RangeData_2; } }
    public StatusData SkillStatus { get { return _SkillStatus; } }

    public SkillTemplate(string strKey, JSONNode nodeData)
    {
        _StrKey = strKey;
        _SkillType = (E_SKILLTEMPLATETYPE)nodeData["SKILL_TYPE"].AsInt;
        _RangeType = (E_SKILLRANGETYPE)nodeData["RANGE_TYPE"].AsInt;

        _RangeData_1 = nodeData["RANGE_DATA_1"].AsFloat;
        _RangeData_2 = nodeData["RANGE_DATA_2"].AsFloat;

        for (int i = 0; i < (int)E_STATUSDATA.MAX; ++i)
        {
            E_STATUSDATA eStatus = (E_STATUSDATA)i;
            double valueData = nodeData[eStatus.ToString()].AsDouble;
            if (valueData > 0)
                _SkillStatus.IncreaseData(eStatus, valueData);
        }
    }
}