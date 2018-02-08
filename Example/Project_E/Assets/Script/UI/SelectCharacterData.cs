using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class SelectCharacterData
{
    public Dictionary<ESELECTCHARACTERSTAGE, List<string>> LoadJSONSelectCharacterDic (string STAGE_CHARACTER_DATA_Path)
    {
        Dictionary<ESELECTCHARACTERSTAGE, List<string>> SelectCharacterDic = new Dictionary<ESELECTCHARACTERSTAGE, List<string>>();
        int listindex = 0;

        TextAsset TextData = Resources.Load(STAGE_CHARACTER_DATA_Path) as TextAsset;
        if (TextData == null)
        {
            Debug.LogError("대사집 데이터 로드 실패");
            return null;
        }

        JSONNode rootNode = JSON.Parse(TextData.text);

        if (rootNode == null)
            return null;

        JSONObject SelectCharacterDataNode = rootNode["STAGE_CHARACTER_DATA"] as JSONObject;

        List<string> liststring = new List<string>();

        foreach (KeyValuePair<string, JSONNode> pair in SelectCharacterDataNode)
        {

            for (int i = 1; i < (int)ESELECTCHARACTERSTAGE.MAX; ++i)
            {
                if (pair.Value["CHARACTER_" + i] != 0)
                {
                    liststring.Add(pair.Value["CHARACTER_" + i]);
                }
            }
            ESELECTCHARACTERSTAGE parsed_enum = (ESELECTCHARACTERSTAGE)System.Enum.Parse(typeof(ESELECTCHARACTERSTAGE), pair.Key);

            List<string> temp = new List<string>();
            for (int i = listindex; i < liststring.Count; i++)
            {
                temp.Add(liststring[i]);
            }
            listindex = liststring.Count;
            SelectCharacterDic.Add(parsed_enum, temp);
        }
        return SelectCharacterDic;
    }

}
