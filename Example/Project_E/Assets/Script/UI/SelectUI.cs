using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    //battleManager return List
    List<E_PLAYTYPE> PlayerList = new List<E_PLAYTYPE>();
    List<E_ENEMYTYPE> EnemyList = new List<E_ENEMYTYPE>();

    E_SCENETYPE currStage;

    Transform myTransform;
    SelectCharacterData SelectCharacterData = new SelectCharacterData();

    List<GameObject> myCharacterlistGameObject = new List<GameObject>();

    List<string> StrlistCharacter;
    List<Sprite> listsprite = new List<Sprite>();
    List<GameObject> SelectCharacterlist = new List<GameObject>();

    GameObject ConfirmButton;

    Dictionary<ESELECTCHARACTERSTAGE, List<string>> SelectCharacterDic;

    public EMUSIC SelectBuff;
    List<GameObject> BuffButton = new List<GameObject>();
    GameObject SelectBuffBackImage;
    GameObject SelectCharacterImage;

    public void SelectMusic(int index)
    {
        string enumname = BuffButton[index].GetComponent<Image>().sprite.name;
        ECHARACTER ECharacterindex;

        ECharacterindex = (ECHARACTER)System.Enum.Parse(typeof(ECHARACTER), enumname);

        switch (ECharacterindex)
        {
            case ECHARACTER.HANRAN:
                SelectBuff = EMUSIC.HANRAN_MUSIC_1;
                break;
            case ECHARACTER.IRIS:
                SelectBuff = EMUSIC.IRIS_MUSIC_2;
                break;
            case ECHARACTER.TIBOUCHINA:
                SelectBuff = EMUSIC.TIBOUCHINA_MUSIC_3;
                break;
            case ECHARACTER.VERBENA:
                SelectBuff = EMUSIC.VERBENA_MUSIC_4;
                break;
            default:
                Debug.LogError("버프음악이 설정되지 않습니다");
                break;
        }

        Scene_Manager.Instance.LoadScene(currStage, false);
        Scene_Manager.Instance.UpdateScene();
        BattleManager.Instance.PlayerList = PlayerList;
        BattleManager.Instance.EnemyList = EnemyList;
//        BackSound.Instance.BackSoundPlay(SelectBuff);
        
        Debug.Log(SelectBuff.ToString());
    }

    void SetButton()
    {
        ConfirmButton = GameObject.Find("Confirm").gameObject;

        SelectCharacterImage = GameObject.Find("SelectCharacterBorder").gameObject;

        for (int i = 1; i <= 3; ++i)
        {
            SelectCharacterlist.Add(GameObject.Find("SelectCharacter" + i).gameObject);
        }

        for (int i = 0; i < StrlistCharacter.Count; ++i)
        {
            myCharacterlistGameObject.Add(GameObject.Find("Button" + ((ECHARACTER)i).ToString()).gameObject);
            myCharacterlistGameObject[i].GetComponent<Image>().enabled = true;
            myCharacterlistGameObject[i].GetComponent<Button>().interactable = true;
        }

        for (int i = 1; i <= 3; ++i)
        {
            BuffButton.Add(GameObject.Find("SelectMusic" + i).gameObject);
        }

        for (int i = 0; i < BuffButton.Count; ++i)
        {
            BuffButton[i].SetActive(false);
        }

        SelectBuffBackImage = GameObject.Find("ButtonBack").gameObject;
        SelectBuffBackImage.SetActive(false);

    }

    public void ConfirmClicked()
    {
        string ename = null;
        ECHARACTER Character_enum;
        for (int i = 0; i < SelectCharacterlist.Count; ++i)
        {
            if (SelectCharacterlist[i].GetComponent<Image>().sprite == null)
                break;

            ename = SelectCharacterlist[i].GetComponent<Image>().sprite.name;
            Character_enum = (ECHARACTER)System.Enum.Parse(typeof(ECHARACTER), ename);
            PlayerList.Add((E_PLAYTYPE)Character_enum);
        }

        SelectCharacterImage.SetActive(false);

        for (int i = 0; i < myCharacterlistGameObject.Count; ++i)
        {
            myCharacterlistGameObject[i].SetActive(false);
        }

        for (int i = 0; i < SelectCharacterlist.Count; ++i)
        {
            SelectCharacterlist[i].SetActive(false);
        }

        ConfirmButton.SetActive(false);

        SelectBuffBackImage.SetActive(true);

        for (int i = 0; i < BuffButton.Count; ++i)
        {
            BuffButton[i].SetActive(true);
            BuffButton[i].GetComponent<Image>().sprite = SelectCharacterlist[i].GetComponent<Image>().sprite;
        }
    }

    public void CharacterClickedButton(int index)
    {
        for (int i = 0; i < SelectCharacterlist.Count; ++i)
        {
            if (SelectCharacterlist[i].GetComponent<Image>().sprite != null)
            {
                if (SelectCharacterlist[i].GetComponent<Image>().sprite.name.Equals(listsprite[index].name))
                    return;
            }
        }

        if (SelectCharacterlist[0].GetComponent<Image>().sprite == null)
        {
            SelectCharacterlist[0].GetComponent<Image>().sprite = listsprite[index];
            SelectCharacterlist[0].GetComponent<Image>().enabled = true;
            SelectCharacterlist[0].GetComponent<Button>().interactable = true;
        }
        else if (SelectCharacterlist[1].GetComponent<Image>().sprite == null)
        {
            SelectCharacterlist[1].GetComponent<Image>().sprite = listsprite[index];
            SelectCharacterlist[1].GetComponent<Image>().enabled = true;
            SelectCharacterlist[1].GetComponent<Button>().interactable = true;
        }
        else if (SelectCharacterlist[2].GetComponent<Image>().sprite == null)
        {
            SelectCharacterlist[2].GetComponent<Image>().sprite = listsprite[index];
            SelectCharacterlist[2].GetComponent<Image>().enabled = true;
            SelectCharacterlist[2].GetComponent<Button>().interactable = true;
        }

        //선택할 캐릭터가 2개일 때 (Stage1)
        if(StrlistCharacter.Count == 2)
        {
            if (SelectCharacterlist[0].GetComponent<Image>().sprite != null
                 && SelectCharacterlist[1].GetComponent<Image>().sprite != null)
            {
                ConfirmButton.GetComponent<Image>().enabled = true;
                ConfirmButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (SelectCharacterlist[0].GetComponent<Image>().sprite != null
                && SelectCharacterlist[1].GetComponent<Image>().sprite != null
                && SelectCharacterlist[2].GetComponent<Image>().sprite != null)
            {
                ConfirmButton.GetComponent<Image>().enabled = true;
                ConfirmButton.GetComponent<Button>().interactable = true;
            }
        }

        for (int j = 0; j < SelectCharacterlist.Count; ++j)
        {
            if (SelectCharacterlist[j].GetComponent<Image>().sprite != null)
            {
                if (SelectCharacterlist[j].GetComponent<Image>().sprite.name.Equals(myCharacterlistGameObject[index].GetComponent<Image>().sprite.name))
                    myCharacterlistGameObject[index].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }
    public void SelectCharacterClicked(int index)
    {
        for (int i = 0; i < myCharacterlistGameObject.Count; ++i)
        {
            if (SelectCharacterlist[index].GetComponent<Image>().sprite.name.Equals(myCharacterlistGameObject[i].GetComponent<Image>().sprite.name))
                myCharacterlistGameObject[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }

        SelectCharacterlist[index].GetComponent<Image>().sprite = null;
        SelectCharacterlist[index].GetComponent<Image>().enabled = false;
        SelectCharacterlist[index].GetComponent<Button>().interactable = false;

        if (SelectCharacterlist[0].GetComponent<Image>().sprite == null || SelectCharacterlist[1].GetComponent<Image>().sprite == null)
        {
            ConfirmButton.GetComponent<Image>().enabled = false;
            ConfirmButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Init(ESELECTCHARACTERSTAGE _SelectStage)
    {
        myTransform = GameObject.Find("Select_Panel").transform;
        SelectCharacterDic = SelectCharacterData.LoadJSONSelectCharacterDic("JSON/STAGE_CHARACTER_DATA");
        StrlistCharacter = SelectCharacterDic[_SelectStage];

        switch (_SelectStage)
        {
            case ESELECTCHARACTERSTAGE.STAGE_1:
                EnemyList.Add(E_ENEMYTYPE.PF_ENEMY_BLUE);
                EnemyList.Add(E_ENEMYTYPE.PF_ENEMY_RED);
                currStage = E_SCENETYPE.SCENE_STAGE_1;
                BattleManager.Instance.E_NextStage = E_TEXTTYPE.STAGE2_S;
                BattleManager.Instance.E_CurrStageEnd = E_TEXTTYPE.STAGE1_E;
                break;
            case ESELECTCHARACTERSTAGE.STAGE_2:
                EnemyList.Add(E_ENEMYTYPE.PF_ENEMY_TIBOUCHINA);
                currStage = E_SCENETYPE.SCENE_STAGE_2;
                BattleManager.Instance.E_NextStage = E_TEXTTYPE.STAGE3_S;
                BattleManager.Instance.E_CurrStageEnd = E_TEXTTYPE.STAGE2_E;
                break;
            case ESELECTCHARACTERSTAGE.STAGE_3:
                EnemyList.Add(E_ENEMYTYPE.PF_ENEMY_VERBENA);
                currStage = E_SCENETYPE.SCENE_STAGE_3;
                BattleManager.Instance.E_NextStage = E_TEXTTYPE.STAGE4_S;
                BattleManager.Instance.E_CurrStageEnd = E_TEXTTYPE.STAGE3_E;
                break;
            case ESELECTCHARACTERSTAGE.STAGE_4:
                EnemyList.Add(E_ENEMYTYPE.PF_ENEMY_ROSE);
                currStage = E_SCENETYPE.SCENE_STAGE_4;
                BattleManager.Instance.E_NextStage = E_TEXTTYPE.END;
                BattleManager.Instance.E_CurrStageEnd = E_TEXTTYPE.STAGE4_E;
                break;

            default:
                break;
        }

        for (int i = 0; i < (int)ECHARACTER.MAX; ++i)
        {
            listsprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECHARACTER)i).ToString()));
        }

        SetButton();
    }
}
