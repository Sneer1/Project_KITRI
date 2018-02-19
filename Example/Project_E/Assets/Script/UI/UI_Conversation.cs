using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class UI_Conversation : MonoSingleton<UI_Conversation>
{
    //대사집딕셔너리
    Text_Character TextData;

    List<string> CharacterList = new List<string>();
    List<string> TextList = new List<string>();

    public string stageData;
    public char checkStartEnd;
    public GameObject stagePrefab = null;

    Color _fontColor = UnityEngine.Color.black;
    Font _textFont = null;
    Text _dialog = null;

    bool _bSetCanvas = false;

    bool fastreading = false;
    bool reading = true;
    GameObject _gameObject = null;
    float printTextTime = 0f;
    int length = 1;

    List<Sprite> listsprite = new List<Sprite>();

    int ConversationIndex = 0;

    int _fontSize = 0;
    int currentindex = 0;

    bool startconversation = true;

    string CenterSprite = null;

    public int FontSize
    {
        get
        {
            return _fontSize;
        }
        set
        {
            _fontSize = value;
        }
    }

    public bool BsetCanvas
    {
        get
        {
            return _bSetCanvas;
        }
        set
        {
            _bSetCanvas = value;
        }
    }
    public Color FontColor
    {
        get
        {
            return _fontColor;
        }
        set
        {
            _fontColor = value;
        }
    }
    public Font TextFont
    {
        get
        {
            return _textFont;
        }
        set
        {
            _textFont = value;
        }
    }
    //dialog property
    public Text Dialog
    {
        get
        {
            return _dialog;
        }
        set
        {
            _dialog = value;
        }

    }
    public GameObject MyGameObject
    {
        get
        {
            return _gameObject;
        }
        set
        {
            _gameObject = value;
        }
    }

    public void Init(E_TEXTTYPE _textType)
    {
        MyGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Conversation/UIConversationCanvas"), this.transform);
        Dialog = GameObject.Find("ConversationDialog").GetComponent<Text>();
        TextData = TextLoad.Instance.GetText_Stage(_textType.ToString());
        CharacterList = TextData.CharacterName;
        TextList = TextData.Text;
        SetSpriteResource();
        stageData = TextData.GetStageData.Substring(0, 6);
        checkStartEnd = TextData.GetStageData[7];
    }

    private void Awake()
    {

    }

    private void SetSpriteResource()
    {
        for (int i = 0; i < (int)ECHARACTER.MAX; ++i)
        {
            listsprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECHARACTER)i).ToString()));
        }
    }

    private void Update()
    {
        ViewText();
    }

    public void SetSpriteImage()
    {
        Image SpriteCenter = GameObject.Find("CharacterSpriteCenter").GetComponent<Image>();

        if (SpriteCenter == null)
        {
            Debug.LogError("스프라이트를 찾지 못했습니다");
            return;
        }


        CenterSprite = CharacterList[currentindex];
        SpriteCenter.color = new Color(1f, 1f, 1f);

        if (CenterSprite.Equals("HERO"))
        {
            if (currentindex + 1 < CharacterList.Count)
            {
                CenterSprite = CharacterList[currentindex - 1];
                SpriteCenter.color = new Color(0.3f, 0.3f, 0.3f);
            }
            else
            {
                CenterSprite = CharacterList[currentindex + 1];
                SpriteCenter.color = new Color(0.3f, 0.3f, 0.3f);
            }
        }

        ECHARACTER Character_enum = (ECHARACTER)System.Enum.Parse(typeof(ECHARACTER), CenterSprite);

        SpriteCenter.sprite = listsprite[(int)Character_enum];
    }

    public void ViewText()
    {
        if (Dialog == null)
        {
            Debug.Log("다이얼로그가 비었습니다");
            return;
        }

        if (CharacterList.Count == 0)
        {
            Debug.Log("리스트 다이얼로그가 비었습니다");
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (reading == true)
                fastreading = true;

            if (reading == false)
                startconversation = true;

            reading = true;

            if (CharacterList.Count <= currentindex)
                ConversationQuitStartSelect();
        }

        //설정된 다이얼로그 list의 최대 인덱스보다 크면 리턴
        if (CharacterList.Count <= currentindex)
            return;

        printTextTime += Time.deltaTime;

        if (startconversation)
        {
            SetSpriteImage();
            startconversation = false;
        }

        //기본으로 출력되는 다이얼로그 
        if (fastreading == false && reading == true)
        {
            if (printTextTime > ConstValue.TextTimeCheck)
            {
                Dialog.text = TextList[currentindex].Substring(ConversationIndex, length);
                if (TextList[currentindex].Length - ConversationIndex >= length)
                {
                    length++;
                }
                printTextTime = 0;
                reading = true;
            }
        }

        //마우스를 클릭하면 다이얼로그가 빠르게 출력된다.
        else if (reading == true)
        {
            printTextTime += ConstValue.TextTimeCheck;
            if (printTextTime > ConstValue.TextTimeCheck)
            {
                Dialog.text = TextList[currentindex].Substring(ConversationIndex, length);
                if (TextList[currentindex].Length - ConversationIndex >= length)
                {
                    length++;
                }
                printTextTime = 0;
            }
        }

        //다이얼로그 하나가 끝나면 다음 다이얼로그로 넘어간다.
        if (length >= TextList[currentindex].Length - ConversationIndex + 1)
        {
            length = 1;
            fastreading = false;
            currentindex++;
            reading = false;
            ConversationIndex = 0;

        }
    }

    public void SetCanvas(bool bsetcanvas = false)
    {
        GameObject _gobj = GameObject.Find("UIConversationCanvas").gameObject;

        BsetCanvas = bsetcanvas;

        if (_gobj == null)
        {
            Debug.LogError("게임오브젝트가 널값입니다");
        }
        if (BsetCanvas == false)
        {
            _gobj.SetActive(false);
            return;
        }
        _gobj.SetActive(true);
    }

    public void TextColorChange(Color color)
    {
        FontColor = color;
        Dialog.color = FontColor;
    }
    public void TextFontSizeChange(int fontsize)
    {
        FontSize = fontsize;
        Dialog.fontSize = FontSize;
    }
    public void TextFontChange(Font font)
    {
        TextFont = font;
        Dialog.font = TextFont;
    }

    void ConversationQuitStartSelect()
    {
        if (checkStartEnd == 'S')
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SELECT/Select_Canvas")) as GameObject;

            ESELECTCHARACTERSTAGE selectStage = ESELECTCHARACTERSTAGE.STAGE_1;
            switch (stageData)
            {
                case "STAGE1":
                    selectStage = ESELECTCHARACTERSTAGE.STAGE_1;
                    break;
                case "STAGE2":
                    selectStage = ESELECTCHARACTERSTAGE.STAGE_2;
                    break;
                case "STAGE3":
                    selectStage = ESELECTCHARACTERSTAGE.STAGE_3;
                    break;
                case "STAGE4":
                    selectStage = ESELECTCHARACTERSTAGE.STAGE_4;
                    break;

                default:
                    break;
            }
            go.GetComponent<SelectUI>().Init(selectStage);
        }
        else
        {
            GameManager.Instance.UIConversation_Change(BattleManager.Instance.E_NextStage);
            Destroy(stagePrefab);
        }


        Destroy(this.gameObject);
    }

}