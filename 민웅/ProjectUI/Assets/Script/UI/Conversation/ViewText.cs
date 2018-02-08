using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewText : MonoBehaviour
{
    //출력시간
    float printTextTime = 0f;

    //출력해줄 대사의 길이 초기화
    int CurrentLength = 1;

    //대사 스트링
    string ConversationText = null;

    //스테이지레벨
    EStageLevel StageLevel;

    //대사집 딕셔너리
    Dictionary<EStageLevel, List<string>> ConversationDic;

    //스프라이트 리스트
    List<Sprite> ListSprite = new List<Sprite>();

    ECharacter Centercharacter_enum = ECharacter.NONE;


    Image SpriteCenter = null;


    List<string> CurrentCharacterNameList = new List<string>();

    int ConversationListSize = 0;


    bool Reading = true;

    void Init()
    {
        ConversationListSize = ConversationDic[StageLevel].Count;
        LoadToListSprite();
    }

    private void Update()
    {

    }

    //스프라이트 이미지 리스트에 추가, 현재 단락의 모든 이름들 리스트에 추가
    void LoadToListSprite()
    {
        SpriteCenter = GameObject.Find("CharacterSpriteCenter").GetComponent<Image>();
        for (int i = 0; i < (int)ECharacter.MAX; ++i)
        {
            ListSprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECharacter)i).ToString()));
        }

        for (int i = 0; i < ConversationListSize; ++i)
        {
            CurrentCharacterNameList.Add(GetDialogName(i));
        }

    }

    //대사집 딕셔너리, 스테이지레벨
    public ViewText(Dictionary<EStageLevel, List<string>> dic, EStageLevel eStageLevel)
    {
        ConversationDic = dic;
        StageLevel = eStageLevel;
    }

    //대사 스트링을 반환한다
    public string DialogViewtext()
    {
        return ConversationText;
    }

    void BeforeConversation()
    {
    }

    //대사출력
    void ReadingConversation(int currentindex, bool fastread = false)
    {
        List<string> CurrentStageTextList = ConversationDic[StageLevel];

        int DialogTextLength = CurrentStageTextList[currentindex].Length;
        int RealTextIndex = DialogTextLength - GetDialogName(currentindex).Length + 1;

        //예외처리
        if (currentindex > CurrentStageTextList.Count)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (Reading == false)
                SetSpriteImage(currentindex);

            if (Reading == true)
                fastread = true;

            Reading = true;
        }

        if (Reading == true)
        {
            printTextTime += Time.deltaTime;

            //빠르게 읽기
            if (fastread == true)
            {
                printTextTime += ConstValue.TextTimeCheck;
            }

            //디폴트 읽기
            if (printTextTime > ConstValue.TextTimeCheck)
            {
                ConversationText = CurrentStageTextList[currentindex].Substring(RealTextIndex, CurrentLength);
                if (RealTextIndex > CurrentLength)
                {
                    CurrentLength++;
                }
                printTextTime = 0;
            }

            //하나의 텍스트가 끝난다
            if (CurrentLength > RealTextIndex)
            {
                Reading = false;
                currentindex++;
                fastread = false;
            }
        }
    }

    void AfterConversation()
    {
    }

    //원하는 텍스트의 이름을 얻는다
    string GetDialogName(int wantindex)
    {
        string charactername = null;
        List<string> currentstagedialog = ConversationDic[StageLevel];

        for (int i = 0; i < currentstagedialog[wantindex].Length; ++i)
        {
            if (currentstagedialog[wantindex].Substring(i, 1) == "/")
            {
                charactername = currentstagedialog[wantindex].Substring(0, i);
            }
        }
        return charactername;
    }

    //스프라이트 이미지 셋
    void SetSpriteImage(int currentindex)
    {
        if (SpriteCenter == null)
        {
            Debug.LogError("스프라이트를 찾지 못했습니다");
            return;
        }

        Centercharacter_enum = (ECharacter)System.Enum.Parse(typeof(ECharacter), CurrentCharacterNameList[currentindex]);
        SpriteCenter.sprite = ListSprite[(int)Centercharacter_enum];
    }
}