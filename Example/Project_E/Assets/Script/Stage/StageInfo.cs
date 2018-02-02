﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StageInfo
{

    string StrKey = string.Empty;
    string Name = string.Empty;
    string MapModel = string.Empty;
    double ClearFinish = 0.0f;

    public string KEY { get { return StrKey; } }
    public string NAME { get { return Name; } }
    public string MODEL { get { return MapModel; } }
    public double CLEAR_FINISH { get { return ClearFinish; } }

    public StageInfo(string _strKey, JSONNode nodeData)
    {
        StrKey = _strKey;
        Name = nodeData["NAME"];
        MapModel = nodeData["MAP_MODEL"];
        ClearFinish = nodeData["CLEAR_FINISH"].AsDouble;
    }
}
