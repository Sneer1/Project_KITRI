using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class ConversationData
{
    //대사집의 JSON로드
    public Dictionary<ESTAGELEVEL, List<string>> LoadJSONdialogtext(string ConversationTextPath)
    {
        Dictionary<ESTAGELEVEL, List<string>> conversationdic = new Dictionary<ESTAGELEVEL, List<string>>();
        int listindex = 0;

        TextAsset TextData = Resources.Load(ConversationTextPath) as TextAsset;
        if (TextData == null)
        {
            Debug.LogError("대사집 데이터 로드 실패");
            return null;
        }

        JSONNode rootNode = JSON.Parse(TextData.text);

        if (rootNode == null)
            return null;

        JSONObject TextDataNode = rootNode["ConversationText"] as JSONObject;

        List<string> liststring = new List<string>();

        foreach (KeyValuePair<string, JSONNode> pair in TextDataNode)
        {

            for (int i = 1; i < 7; ++i)
            {
                if (pair.Value["TEXT_" + i] != 0)
                {
                    liststring.Add(pair.Value["TEXT_" + i]);
                }
            }

            ESTAGELEVEL parsed_enum = (ESTAGELEVEL)System.Enum.Parse(typeof(ESTAGELEVEL), pair.Key);

            List<string> temp = new List<string>();
            for (int i = listindex; i < liststring.Count; i++)
            {
                temp.Add(liststring[i]);
            }
            listindex = liststring.Count;
            conversationdic.Add(parsed_enum, temp);
        }
        return conversationdic;
    }
}
