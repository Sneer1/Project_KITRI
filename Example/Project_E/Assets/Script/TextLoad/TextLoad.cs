using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TextLoad : MonoSingleton<TextLoad>
{
    Dictionary<string, Text_Character> DicTextData = new Dictionary<string, Text_Character>();

    private void Awake()
    {
        TextAsset characterText = Resources.Load(ConstValue.TextLoadPath) as TextAsset;

        if(characterText != null)
        {
            JSONObject rootNodeText = JSON.Parse(characterText.text) as JSONObject;

            if(rootNodeText != null)
            {
                JSONObject TextLoadData = rootNodeText[ConstValue.TextLoadKey] as JSONObject;

                foreach(KeyValuePair<string, JSONNode> TextNode in TextLoadData)
                {
                    DicTextData.Add(TextNode.Key, new Text_Character(TextNode.Key, TextNode.Value));
                }
            }
        }
            else
        {
            Debug.LogError("텍스트 로드 실패");
        }
    }

    public Text_Character GetText_Stage(string _strStage)
    {
        Text_Character TextData = null;
        DicTextData.TryGetValue(_strStage, out TextData);
        if(TextData == null)
        {
            Debug.LogError("Key : " + _strStage + "해당 데이터 미등록!, 또는 키오류");
            return null;
        }
        return TextData;
    }


}
