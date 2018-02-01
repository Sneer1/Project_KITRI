using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue
{
    // GetData key 관련
    public const string ActorData_Team = "TEAM_TYPE";
    public const string ActorData_GetTarget = "GET_TARGET";
    public const string ActorData_Character = "CHARACTER";
    public const string ActorData_SKILLDATA = "SKILL_DATA";
    public const string ActorData_Hit = "HIT";

    // Throw Events 관련 
    public const string ActorData_SetTarget = "SET_TARGET";

    public const string EventKey_EnemyInit = "ENEMY_INIT";
    public const string EventKey_SelectSkill = "SELECT_SKILL";
    public const string EventKey_SelectModel = "SELECT_SKILL_MODEL";

    // Set Data Key
    public const string SetData_Damage = "BOARD_DAMAGE";
    public const string SetData_HP = "BOARD_HP";

    //JSON Path 관련
    public const string CharacterTemplatePath = "JSON/CHARACTER_TEMPLATE";
    public const string CharacterTemplateKey = "CHARACTER_TEMPLATE";

    public const string SkillTemplatePath = "JSON/SKILL_TEMPLATE";
    public const string SkillTemplateKey = "SKILL_TEMPLATE";

    public const string SkillDataPath = "JSON/SKILL_DATA";
    public const string SkillDataKey = "SKILL_DATA";

    //StatusData Key 관련
    public const string CharacterStatusDataKey = "CHARACTER_TEMPLATE";

    //Resources path
    public const string UI_Path_HP = "Prefabs/UI/Board/HPBoard";
    public const string UI_Path_DAMAGE = "Prefabs/UI/Board/DamageBoard";
}
