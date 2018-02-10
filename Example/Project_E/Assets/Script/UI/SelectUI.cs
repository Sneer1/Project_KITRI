using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    Transform myTransform;
    List<GameObject> mylistGameObject = new List<GameObject>();

    SelectCharacterData SelectCharacterData = new SelectCharacterData();

    List<string> listSelectCharacter;
    List<Sprite> listsprite = new List<Sprite>();
    Dictionary<ESELECTCHARACTERSTAGE, List<string>> SelectCharacterDic;

    ESELECTCHARACTERSTAGE eSELECTCHARACTERSTAGE = ESELECTCHARACTERSTAGE.STAGE_3;

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
            mylistGameObject.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/SelectButton"), myTransform));
        }

        for (int j = 0; j < mylistGameObject.Count; ++j)
        {
            mylistGameObject[j].GetComponent<Image>().sprite = listsprite[j];
        }

        switch (mylistGameObject.Count)
        {
            case 2:
                {
                    mylistGameObject[0].transform.localPosition = new Vector3( -300, -160f, 0f);
                    mylistGameObject[1].transform.localPosition = new Vector3( 300, -160f, 0f);
                }
                break;

            case 3:
                {
                    mylistGameObject[0].transform.localPosition = new Vector3( -300, -160f, 0f);
                    mylistGameObject[1].transform.localPosition = new Vector3( 0, -160f, 0f);
                    mylistGameObject[2].transform.localPosition = new Vector3( 300, -160f, 0f);
                }
                break;

            case 4:
                {
                    mylistGameObject[0].transform.localPosition = new Vector3( -450, -160f, 0f);
                    mylistGameObject[1].transform.localPosition = new Vector3( -150, -160f, 0f);
                    mylistGameObject[2].transform.localPosition = new Vector3( 150, -160f, 0f);
                    mylistGameObject[3].transform.localPosition = new Vector3( 450, -160f, 0f);
                }
                break;
        }

    }
    
    void CharacterClickedButton(int selectindex)
    {
        List<GameObject> list = new List<GameObject>();
        int i = 0;
        if (list.Count < 3)
        {
            list.Add(Instantiate(mylistGameObject[selectindex]));
            i++;
            list[i].transform.localPosition = new Vector3(-130f, 150f);
            list[i].transform.localPosition = new Vector3(130f, 150f);
        }
    }


    void Init()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Select_Canvas"), this.transform);
        myTransform = GameObject.Find("Select_Panel").transform;
        SelectCharacterDic = SelectCharacterData.LoadJSONSelectCharacterDic("JSON/STAGE_CHARACTER_DATA");
        listSelectCharacter = SelectCharacterDic[eSELECTCHARACTERSTAGE];

        for(int i = 0; i < (int)ECHARACTER.MAX; ++i)
        {
            listsprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECHARACTER)i).ToString()));
        }

        SetButton();
    }

    private void Awake()
    {
        Init();
    }


}
