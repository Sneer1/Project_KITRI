using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    Transform myTransform;
    SelectCharacterData SelectCharacterData = new SelectCharacterData();

    List<GameObject> mylistGameObject = new List<GameObject>();

    List<string> listSelectCharacter;
    List<Sprite> listsprite = new List<Sprite>();
    List<GameObject> SelectCharacterlist = new List<GameObject>();

    Dictionary<ESELECTCHARACTERSTAGE, List<string>> SelectCharacterDic;

    ESELECTCHARACTERSTAGE eSELECTCHARACTERSTAGE = ESELECTCHARACTERSTAGE.STAGE_3;

    void SelectMusic()
    {


    }

    void SetButton()
    {
        for (int i = 0; i < listSelectCharacter.Count; ++i)
        {
            mylistGameObject.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Button" + ((ECHARACTER)i).ToString()), myTransform));
        }

        for (int j = 0; j < mylistGameObject.Count; ++j)
        {
            mylistGameObject[j].GetComponent<Image>().sprite = listsprite[j];
        }

        switch (mylistGameObject.Count)
        {
            case 2:
                {
                    mylistGameObject[0].transform.localPosition = new Vector3(-300, -160f, 0f);
                    mylistGameObject[1].transform.localPosition = new Vector3(300, -160f, 0f);
                }
                break;

            case 3:
                {
                    mylistGameObject[0].transform.localPosition = new Vector3(-300, -160f, 0f);
                    mylistGameObject[1].transform.localPosition = new Vector3(0, -160f, 0f);
                    mylistGameObject[2].transform.localPosition = new Vector3(300, -160f, 0f);
                }
                break;

            case 4:
                {
                    mylistGameObject[0].transform.localPosition = new Vector3(-450, -160f, 0f);
                    mylistGameObject[1].transform.localPosition = new Vector3(-150, -160f, 0f);
                    mylistGameObject[2].transform.localPosition = new Vector3(150, -160f, 0f);
                    mylistGameObject[3].transform.localPosition = new Vector3(450, -160f, 0f);
                }
                break;
        }

        Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Confirm"), myTransform);
    }
    public List<ECHARACTER> CharacterConfirm()
    {
        List<ECHARACTER> list = new List<ECHARACTER>();
        list = null;
        string ename = null;
        for(int i = 0; i < SelectCharacterlist.Count; ++i)
        {
            ename = SelectCharacterlist[i].GetComponent<Image>().sprite.name;
            ECHARACTER Character_enum = (ECHARACTER)System.Enum.Parse(typeof(ECHARACTER), ename);
            list.Add(Character_enum);
        }
        return list;
    }

    public void CharacterClickedButton(int index)
    {
        for (int i = 0; i < SelectCharacterlist.Count; ++i)
        {
            if (SelectCharacterlist[i].GetComponent<Image>().sprite == null)
            {
                SelectCharacterlist[i].GetComponent<Image>().sprite = listsprite[index];
                return;
            }
        }
    }

    public void SelectCharacterClicked(int index)
    {
        SelectCharacterlist[index].GetComponent<Image>().sprite = null;
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
        for (int i = 1; i < 3; ++i)
        {
            SelectCharacterlist.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/SelectCharacter" + i), myTransform));
        }
        SetButton();
    }

    private void Awake()
    {
        Init();
    }
}
