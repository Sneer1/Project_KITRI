using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    int _index;
    public int Index
    {
        get { return _index; }
    }
    List<string> list;
    SelectCharacterData SelectCharacterData = new SelectCharacterData();
    Dictionary<ESELECTCHARACTERSTAGE, List<string>> Dictionary;
    Sprite mySprite;

    public void Init(ESELECTCHARACTERSTAGE eSELECTCHARACTERSTAGE)
    {
        Dictionary = SelectCharacterData.LoadJSONSelectCharacterDic("JSON/STAGE_CHARACTER_DATA");
        list = Dictionary[eSELECTCHARACTERSTAGE];

        mySprite = gameObject.GetComponent<Image>().sprite;
        
        for (int i = 0; i < list.Count; ++i)
        {
            if (mySprite.name.Equals(list[i]))
                _index = i;
        }

    }

    public void CharacterPortrait()
    {
        
    }
    
}
