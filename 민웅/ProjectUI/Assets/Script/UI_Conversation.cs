﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public enum EStageLevel { STAGE_1_START, STAGE_1_END, STAGE_2_START, STAGE_2_END, STAGE_3_START, STAGE_3_END, STAGE_4_START, STAGE_4_END }
public enum ECharacter { HANRAN, IRIS , HERO , TIBOUCHINA , VERBENA , ROSE, MAX }

public class UI_Conversation : MonoSingleton<UI_Conversation>
{
    Dictionary<EStageLevel, List<string>> ConversationDic = new Dictionary<EStageLevel, List<string>>();

    Color _fontColor = UnityEngine.Color.black;
    Font _textFont = null;
    Text _dialog = null;
    Image _playerSprite = null;


    [SerializeField]
    bool _bSetCanvas = false;

    bool fastreading = false;
    bool reading = true;
    GameObject _gameObject = null;
    float printTextTime = 0f;
    int length = 1;

    List<Sprite> listsprite = new List<Sprite>();

    int ConversationIndex = 0;

    int _fontSize = 0;
    int viewindex = 0;
    int listIndex = 0;

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
    public Image PlayerSprite
    {
        get
        {
            return _playerSprite;
        }
        set
        {
            _playerSprite = value;
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

    public void Init()
    {
        MyGameObject = this.gameObject;

        Dialog = GameObject.Find("ConversationDialog").GetComponent<Text>();
    }

    private void Awake()
    {
        LoadJSONStrAdd("JSON/ConversationText");
        Init();
    }

    private void SetSpriteResource()
    {
        for (int i = 0; i < (int)ECharacter.MAX; ++i)
        {
            listsprite.Add(Resources.Load<Sprite>("Prefabs/UI/Images/" + ((ECharacter)i).ToString()));
        }
    }

    private void Update()
    {
        ViewText(EStageLevel.STAGE_1_START);
    }

    //제이슨으로 로드한 데이터를 dic에 저장한다
    public void LoadJSONStrAdd(string ConversationTextPath)
    {

        TextAsset TextData = Resources.Load(ConversationTextPath) as TextAsset;
        if (TextData == null)
        {
            Debug.LogError("스킬 데이터 로드 실패");
            return;
        }

        JSONNode rootNode = JSON.Parse(TextData.text);

        if (rootNode == null)
            return;

        JSONObject TextDataNode = rootNode["ConversationText"] as JSONObject;

        List<string> liststring = new List<string>();

        foreach (KeyValuePair<string, JSONNode> pair in TextDataNode)
        {

            for (int i = 1; i < 7; ++i)
            {
                if (pair.Value["TEXT_" + i] != 0)
                {
                    liststring.Add(pair.Value["TEXT_" + i]);
                }
            }

            EStageLevel parsed_enum = (EStageLevel)System.Enum.Parse(typeof(EStageLevel), pair.Key);

            List<string> temp = new List<string>();
            for (int i = listIndex; i < liststring.Count; i++)
            {
                temp.Add(liststring[i]);
            }
            listIndex = liststring.Count;
            ConversationDic.Add(parsed_enum, temp);
        }
    }

    //스테이지 안에 있는 모든 다이얼로그를 얻는다
    public List<string> GetStageConversation(EStageLevel eStageLevel)
    {
        foreach (KeyValuePair<EStageLevel, List<string>> p in ConversationDic)
        {
            if (p.Key == eStageLevel)
            {
                return p.Value;
            }
        }
        return null;
    }

    public void SetSpriteImage(EStageLevel eStageLevel)
    {
        PlayerSprite = GameObject.Find("PlayerCharacterSprite").GetComponent<Image>();

        if (PlayerSprite == null)
        {
            Debug.LogError("스프라이트를 찾지 못했습니다");
            return;
        }

        string strplayer = null;

        for (int i = 0; i < ConversationDic[eStageLevel][viewindex].Length; ++i)
        {
            if (ConversationDic[eStageLevel][viewindex].Substring(i, 1) == "/")
            {
                strplayer = ConversationDic[eStageLevel][viewindex].Substring(0, i);
                ConversationIndex = i + 1;
                break;
            }

        }
        ECharacter parsed_enum = (ECharacter)System.Enum.Parse(typeof(ECharacter), strplayer);
        PlayerSprite.sprite = listsprite[(int)parsed_enum];

    }

    public void ViewText(EStageLevel eStageLevel)
    {
        if (Dialog == null)
        {
            Debug.LogError("다이얼로그가 비었습니다");
            return;
        }

        if (ConversationDic[eStageLevel].Count == 0)
        {
            Debug.LogError("리스트 다이얼로그가 비었습니다");
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (reading == true)
                fastreading = true;

            reading = true;
        }

        //설정된 다이얼로그 list의 최대 인덱스보다 크면 리턴
        if (ConversationDic[eStageLevel].Count <= viewindex)
            return;

        printTextTime += Time.deltaTime;

        //기본으로 출력되는 다이얼로그 
        if (fastreading == false && reading == true)
        {
            if (printTextTime > ConstValue.TextTimeCheck)
            {
                Dialog.text = ConversationDic[eStageLevel][viewindex].Substring(ConversationIndex, length);
                if (ConversationDic[eStageLevel][viewindex].Length - ConversationIndex >= length)
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
                Dialog.text = ConversationDic[eStageLevel][viewindex].Substring(ConversationIndex, length);
                if (ConversationDic[eStageLevel][viewindex].Length - ConversationIndex >= length)
                {
                    length++;
                }
                printTextTime = 0;
            }
        }

        SetSpriteImage(eStageLevel);
        //다이얼로그 하나가 끝나면 다음 다이얼로그로 넘어간다.
        if (length >= ConversationDic[eStageLevel][viewindex].Length - ConversationIndex + 1)
        {
            length = 1;
            fastreading = false;
            viewindex++;
            reading = false;
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
}