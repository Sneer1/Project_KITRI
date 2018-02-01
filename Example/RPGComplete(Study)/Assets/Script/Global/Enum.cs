public enum EBaseObjectState
{
    ObjectState_Normal,
    ObjectState_Die,
}

public enum EStateType
{
    State_None,
    State_Idle,
    State_Attack,
    State_Walk,
    State_Dead,
}

public enum ETeamType
{
    Team_1,
    Team_2,
}

public enum EAutoMode
{
    Auto_On,
    Auto_Off,
}

public enum EAIType
{
    NormalAI,
}

public enum EStatusData
{
    MAX_HP,
    ATTACK,
    DEFFENCE,
    MAX
}

public enum EMonsterType
{
    Enemy_1,
    Enemy_2,
    Enemy_3,
    MAX
}

public enum ERegenType
{
    NONE,
    REGENTTIME_EVENT,
    TRIGGER_EVENT,
}

public enum ESkillTemplateType
{
    TARGET_ATTACK,
    RANGE_ATTACK
}

public enum ESkillRangeType
{
    RANGE_BOX,
    RANGE_SPHERE
}

public enum ESkillModelType
{
    CIRCLE,
    BOX,
    MAX
}

public enum EBoardType
{
    Board_None,
    Board_HP,
    Board_Damage,
}

public enum EUIType
{
    PF_UI_Lobby,
    PF_UI_Inventory,
    PF_UI_Item,

}

