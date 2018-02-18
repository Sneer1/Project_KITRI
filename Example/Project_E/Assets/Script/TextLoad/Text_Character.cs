using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Text_Character
{
    string StageData = null;
    List<string> _CharacterName = new List<string>();
    List<string> _Text = new List<string>();

    public string GetStageData { get { return StageData; } }
    public List<string> CharacterName {  get { return _CharacterName; } }
    public List<string> Text { get { return _Text; } }

    public Text_Character(string _stageData, JSONNode nodeData)
    {
        StageData = _stageData;

        JSONArray arr_CharacterName = nodeData["CHARACTER"].AsArray;
        JSONArray arr_Text = nodeData["TEXT"].AsArray;

        if(arr_CharacterName != null && arr_Text != null)
        {
            for(int i = 0; i < arr_CharacterName.Count; ++i)
            {
                _CharacterName.Add(arr_CharacterName[i]);
            }

            for (int i = 0; i < arr_Text.Count; ++i)
            {
                _Text.Add(arr_Text[i]);
            }
        }
    }
}
