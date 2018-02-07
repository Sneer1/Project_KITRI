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

    ECharacter leftcharacter_enum = ECharacter.NONE;
    ECharacter rightcharacter_enum = ECharacter.NONE;

    Image SpriteLeft = null;
    Image SpriteRight = null;

    void Init()
    {
        LoadToListSprite();
    }

    void LoadToListSprite()
    {
        SpriteLeft = GameObject.Find("CharacterSpriteLeft").GetComponent<Image>();
        SpriteRight = GameObject.Find("CharacterSpriteRight").GetComponent<Image>();

        for (int i = 0; i < (int)ECharacter.MAX; ++i)
        {
            ListSprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECharacter)i).ToString()));
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
        string current = null;
        string next = null;

        SetSpriteImage(current, next);
    }

    void BeginnigConversation(int currentindex, bool fastread = false)
    {
        List<string> CurrentStageTextList = ConversationDic[StageLevel];

        int DialogTextLength = CurrentStageTextList[currentindex].Length;
        int RealTextIndex = DialogTextLength - GetDialogName(currentindex).Length + 1;

        printTextTime += Time.deltaTime;

        if (fastread == true)
        {
            printTextTime += ConstValue.TextTimeCheck;
        }

        if (printTextTime > ConstValue.TextTimeCheck)
        {
            ConversationText = CurrentStageTextList[currentindex].Substring(RealTextIndex, CurrentLength);
            if (RealTextIndex > CurrentLength)
            {
                CurrentLength++;
            }
            printTextTime = 0;
        }
    }

    void AfterConversation()
    {

    }

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

    void SetSpriteImage(string left, string right)
    {
        if (SpriteLeft == null || SpriteRight == null)
        {
            Debug.LogError("스프라이트를 찾지 못했습니다");
            return;
        }

        leftcharacter_enum = (ECharacter)System.Enum.Parse(typeof(ECharacter), left);
        rightcharacter_enum = (ECharacter)System.Enum.Parse(typeof(ECharacter), right);

        SpriteLeft.sprite = ListSprite[(int)leftcharacter_enum];
        SpriteRight.sprite = ListSprite[(int)rightcharacter_enum];

        //SpriteRight.color = new Color(0.3f, 0.3f, 0.3f, 1f);
    }
}
