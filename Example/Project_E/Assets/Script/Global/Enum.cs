
public enum E_BASEOBJECTSTATE
{
    STATE_NORMAL,
    STATE_DIE,
    STATE_DODGE
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
    STATE_DEAD,
    STATE_STUN,
    STATE_GRAVITY
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
public enum E_PLAYTYPE
{
    PF_CHARACTER_HANRAN,
    PF_CHARACTER_IRIS,
    PF_CHARACTER_TIBOUCHINA,
    PF_CHARACTER_VERBENA,
    PF_CHARACTER_ROSE,
    MAX
}

public enum E_ENEMYTYPE
{
    PF_ENEMY_BLUE,
    PF_ENEMY_RED,
    PF_ENEMY_TIBOUCHINA,
    PF_ENEMY_VERBENA,
    PF_ENEMY_ROSE,
    MAX
}

//Skill 관련
public enum E_SKILLTEMPLATETYPE
{
    TARGET_ATTACK,
    RANGE_ATTACK,
    RANGE_FIRE,
    RANGE_LIGHTNING,
    STUN_CROWDCONTROL,
    GRAVITY_CROWDCONTROL,
    DODGE_BUFF
}

public enum E_SKILLRANGETYPE // Collision Type
{
    RANGE_BOX, //RANGE_A
    RANGE_SPHERE, // STUN
}

public enum E_SKILLMODETYPE
{
    BOX,
    CIRCLE,
    FIRE,
    LIGHT,
    GRAVITY,
    MAX
}

public enum E_AIMODETYPE
{
    STUN,
    MAX
}

public enum E_BOARDTYPE
{
    BOARD_NONE,
    BOARD_HP,
    BOARD_DAMAGE,
}

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

//스테이지 레벨
public enum ESTAGELEVEL
{
    STAGE_1_START,
    STAGE_1_END,
    STAGE_2_START,
    STAGE_2_END,
    STAGE_3_START,
    STAGE_3_END,
    STAGE_4_START,
    STAGE_4_END
}
//캐릭터
public enum ECHARACTER
{
    HANRAN,
    IRIS,
    TIBOUCHINA,
    VERBENA,
    ROSE,
    HERO,
    MAX
}

public enum ESCORETYPE
{
    Score_Miss,
    Score_Good,
    Score_Great,
    Score_Perpect,
}

public enum ESELECTCHARACTERSTAGE
{
    STAGE_1,
    STAGE_2,
    STAGE_3,
    STAGE_4,
    MAX
}

public enum EMUSIC
{
    HANRAN_MUSIC_1,
    IRIS_MUSIC_2,
    TIBOUCHINA_MUSIC_3,
    VERBENA_MUSIC_4,
}