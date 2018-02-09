using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    Transform myTransform;
    List<GameObject> CharacterSelectlist = new List<GameObject>();

    SelectCharacterData SelectCharacterData = new SelectCharacterData();

    List<string> listSelectCharacter;
    List<Sprite> listsprite = new List<Sprite>();
    Dictionary<ESELECTCHARACTERSTAGE, List<string>> SelectCharacterDic;

    GameObject ConfirmButton;

    ESELECTCHARACTERSTAGE eSELECTCHARACTERSTAGE = ESELECTCHARACTERSTAGE.STAGE_3;
    List<string> selectcharacter = new List<string>();

    List<SelectButton> CharacterButtonList = new List<SelectButton>();
    List<GameObject> mySelectCharacterlist = new List<GameObject>();
    public List<ESELECTCHARACTERSTAGE> mySelectCharacter()
    {
        List<ESELECTCHARACTERSTAGE> list = new List<ESELECTCHARACTERSTAGE>();
        return list;
    }

    void SelectMusic()
    {

    }

    void SetButton()
    {
        for (int i = 0; i < listSelectCharacter.Count; ++i)
        {
            CharacterSelectlist.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Button" + ((ECHARACTER)i).ToString()), myTransform));

            CharacterButtonList.Add(CharacterSelectlist[i].GetComponent<SelectButton>());
            CharacterButtonList[i].Init(eSELECTCHARACTERSTAGE);
        }

        for (int j = 0; j < CharacterSelectlist.Count; ++j)
        {
            CharacterSelectlist[j].GetComponent<Image>().sprite = listsprite[j];
        }

        switch (CharacterSelectlist.Count)
        {
            case 2:
                {
                    CharacterSelectlist[0].transform.localPosition = new Vector3(-300, -160f, 0f);
                    CharacterSelectlist[1].transform.localPosition = new Vector3(300, -160f, 0f);
                }
                break;

            case 3:
                {
                    CharacterSelectlist[0].transform.localPosition = new Vector3(-300, -160f, 0f);
                    CharacterSelectlist[1].transform.localPosition = new Vector3(0, -160f, 0f);
                    CharacterSelectlist[2].transform.localPosition = new Vector3(300, -160f, 0f);
                }
                break;

            case 4:
                {
                    CharacterSelectlist[0].transform.localPosition = new Vector3(-450, -160f, 0f);
                    CharacterSelectlist[1].transform.localPosition = new Vector3(-150, -160f, 0f);
                    CharacterSelectlist[2].transform.localPosition = new Vector3(150, -160f, 0f);
                    CharacterSelectlist[3].transform.localPosition = new Vector3(450, -160f, 0f);
                }
                break;
        }

        myTransform = GameObject.Find("Select_Panel").transform;

        mySelectCharacterlist.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/SelectCharacter"), myTransform));
        mySelectCharacterlist.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/SelectCharacter2"), myTransform));

        ConfirmButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Confirm"), myTransform);

        //SelectButton.Init(eSELECTCHARACTERSTAGE);
    }

    public void CharacterBattle()
    {
        //mySelectCharacterlist[SelectButton.GetIndex()].GetComponent<Image>().sprite = listsprite[SelectButton.GetIndex()];
        
    }

    List<string> CharacterConfirm(int selectindex)
    {
        //selectcharacter.Add(listSelectCharacter[selectindex]);
        //return selectcharacter;
        return null;
    }


    void Init()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Select_Canvas"), this.transform);

        myTransform = GameObject.Find("Select_Panel").transform;

        SelectCharacterDic = SelectCharacterData.LoadJSONSelectCharacterDic("JSON/STAGE_CHARACTER_DATA");

        listSelectCharacter = SelectCharacterDic[eSELECTCHARACTERSTAGE];

        for (int i = 0; i < (int)ECHARACTER.MAX; ++i)
        {
            listsprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECHARACTER)i).ToString()));
        }

        SetButton();
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {

    }


}
