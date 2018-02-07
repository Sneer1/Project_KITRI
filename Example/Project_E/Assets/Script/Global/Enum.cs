﻿
public enum E_BASEOBJECTSTATE
{
    STATE_NORMAL,
    STATE_DIE
}

public enum E_AITYPE
{
    NormalAI,
}

public enum E_STATETYPE
{
    STATE_NONE = 0,
    STATE_IDLE,
    STATE_ATTACK,
    STATE_WALK,
    STATE_DEAD
}

public enum E_STATUSDATA
{
    MAX_HP,
    ATTACK,
    DEFFENCE,
    MAX,
}

public enum E_TEAMTYPE
{
    TEAM_1,
    TEAM_2,
}

public enum E_AUTOMODE
{
    Auto_On,
    Auto_Off,
}


//Monster 관련
//public enum eRegeneratorType
//{
//    NONE,
//    REGENTIME_EVENT,
//    TRIGGER_EVENT,
//}

public enum E_ENEMYTYPE
{
    A_Monster,
    B_Monster,
    C_Monster,
    knight_Blue,
    knight_Red,
    MAX
}

//Skill 관련
public enum E_SKILLTEMPLATETYPE
{
    TARGET_ATTACK,
    RANGE_ATTACK,
}

public enum E_SKILLRANGETYPE
{
    RANGE_BOX,
    RANGE_SPHERE,
}

public enum E_SKILLMODETYPE
{
    CIRCLE,
    BOX,
    MAX
}

//public enum eBoardType
//{
//    BOARD_NONE,
//    BOARD_HP,
//    BOARD_DAMAGE,
//}

//public enum eClearType
//{
//    CLEAR_KILLCOUNT = 0,
//    CLEAR_TIME,
//}

public enum E_SCENETYPE
{
    SCENE_NONE,
    SCENE_TILE,
    SCENE_INTRO,
    SCENE_GAME,
    SCENE_END
}

public enum E_UITYPE
{
    PF_UI_TITLE,
    PF_UI_LOADING,
    PF_UI_LOBBY,
    PF_UI_STAGE,
}

//public enum eSlotType
//{
//    SLOT_NONE = -1,
//    SLOT_WEAPON = 0,
//    SLOT_ARMOR,
//    SLOT_HELMET,
//    SLOT_GUNTLET,
//    SLOT_MAX
//}

