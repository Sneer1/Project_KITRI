﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue 
{
    //텍스트 시간체크
    public const float TextTimeCheck = 0.05f;


	// JSON 관련
	public const string CharacterTemplatePath =
		"JSON/CHARACTER_TEMPLATE";
	public const string CharacterTemplateKey =
		"CHARACTER_TEMPLATE";
    public const string TextDataPath =
        "JSON/INTRO";
    public const string TextDataKey =
        "JSON/INTRO";

    public const string SkillTemplatePath = "JSON/SKILL_TEMPLATE";
	public const string SkillTemplateKey = "SKILL_TEMPLATE";

    public const string SkillDataPath = "JSON/SKILL_DATA";
    public const string SkillDataKey = "SKILL_DATA";

    // StatusData Key 관련
    public const string CharacterStatusDataKey =
		"CHARACTER_TEMPLATE";

	// GetData Key관련
	public const string ActorData_Team = "TEAM_TYPE";
	public const string ActorData_SetTarget = "SET_TARGET";
	public const string ActorData_GetTarget = "GET_TARGET";
	public const string ActorData_AttackRange = "ATTACK_RANGE";
	public const string ActorData_Character = "CHARACTER";
	public const string ActorData_Hit = "HIT";
    public const string ActorData_CrowdControl = "CC";
    public const string ActorData_Buff = "BUFF";
    public const string ActorData_SkillData = "SKILL_DATA";

	// ThrowEvent Key 관련
	public const string EventKey_EnemyInit = "E_INIT";
	public const string EventKey_Hit = "E_HIT";
	public const string EventKey_SelectSkill = "SELECT_SKILL";
	public const string EventKey_SelectModel = "SELECT_SKILL_MODEL";

	// SetData Key 관련
	public const string SetData_HP = "BOARD_HP";
	public const string SetData_Damage = "BOARD_DAMAGE";
    public const string SetData_DamageText = "BOARD_DAMAGE_TEXT";

    // UI Path  관련
    public const string UI_PATH_HP = "Prefabs/UI/HP_Board";
	public const string UI_PATH_DAMAGE = "Prefabs/UI/Damage_Board";

	// LocalSave
	public const string LocalSave_ItemInstance = "ITEM_INSTANCE";
}
