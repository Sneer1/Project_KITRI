using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter
{
    public BaseObject TargetComponent;

    CharacterTemplateData _TemplateData;
    CharacterStatusData _CharacterStatus = new CharacterStatusData();

    public CharacterTemplateData TemplateData
    {
        get { return _TemplateData; }
    }

    public CharacterStatusData CharacterStatus
    {
        get { return _CharacterStatus; }
    }

    double _CurrentHp = 0;
    public double CurrentHP
    { get { return _CurrentHp; } }

    // 
    public SkillData SelectSkill
    {
        get;
        set;
    }
    List<SkillData> ListSkill = new List<SkillData>();


    public void IncreaseCurrentHP(double valueData)
    {
        _CurrentHp += valueData;
        if (_CurrentHp < 0)
            _CurrentHp = 0;

        double maxHP = CharacterStatus.GetStatusData(E_STATUSDATA.MAX_HP);
        if (_CurrentHp > maxHP)
            _CurrentHp = maxHP;

        if (_CurrentHp == 0)
            TargetComponent.ObjectState = E_BASEOBJECTSTATE.STATE_DIE;
    }

    public void SetTemplate(CharacterTemplateData templateData)
    {
        _TemplateData = templateData;

        _CharacterStatus.AddStatusData(ConstValue.CharacterStatusDataKey, templateData.StatusData);
        _CurrentHp = CharacterStatus.GetStatusData(E_STATUSDATA.MAX_HP);

    }

    public void AddSkill(SkillData skillData)
    {
        ListSkill.Add(skillData);

    }

    public SkillData GetSkillByIndex(int index)
    {
        if(ListSkill.Count > index)
        {
            return ListSkill[index];
        }
        return null;
    }
}