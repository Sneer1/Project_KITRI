using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : BaseObject
{
    bool _IsPlayer = true;
    public bool IsPlayer
    {
        get { return _IsPlayer; }
        set { _IsPlayer = value; }
    }

    [SerializeField]
    E_TEAMTYPE _TeamType;
    
    public E_TEAMTYPE TeamType
    {
        get
        {
            return _TeamType;
        }
    }



    [SerializeField]
    string TemplateKey = string.Empty;

    GameCharacter _SelfCharacter = null;

    public GameCharacter SelfChararcter
    { get { return _SelfCharacter; } }

    [SerializeField]
    E_AITYPE AIType = E_AITYPE.NormalAI;

    BaseAI _AI = null;
    public BaseAI AI
    { get { return _AI; } }

    BaseObject TargetObject = null;

    [SerializeField]
    bool bEnableBoard = true;

    private void Awake()
    {
        GameObject aiObject = new GameObject();
        aiObject.name = AIType.ToString("F");

        switch (AIType)
        {
            case E_AITYPE.NormalAI:
                {
                    _AI = aiObject.AddComponent<NormalAI>();
                }
                break;
        }

        _AI.Target = this;
        aiObject.transform.SetParent(SelfTransform);

        GameCharacter gameCharacter = CharacterManager.Instance.AddCharacter(TemplateKey);
        gameCharacter.TargetComponent = this;
        _SelfCharacter = gameCharacter;

        for(int i = 0; i < gameCharacter.TemplateData.ListSkill.Count; ++i)
        {
            SkillData data = SkillManager.Instance.GetSkillData(gameCharacter.TemplateData.ListSkill[i]);

            if(data == null)
            {
                Debug.LogError(gameCharacter.TemplateData.ListSkill[i].ToString() + "해당 스킬 키를 찾을 수 없습니다");
            }
            else
            {
                gameCharacter.AddSkill(data);
            }
        }
        //체력바
        if (bEnableBoard)
        {
            BaseBoard board = BoardManager.Instance.AddBoard(this, E_BOARDTYPE.BOARD_HP);

            board.SetData(ConstValue.SetData_HP, GetStatusData(E_STATUSDATA.MAX_HP), SelfChararcter.CurrentHP);
        }

        ActorManager.Instance.AddActor(this);
    }

    public double GetStatusData(E_STATUSDATA statusData)
    {
        return SelfChararcter.CharacterStatus.GetStatusData(statusData);
    }

    public override object GetData(string keyData, params object[] datas)
    {
        if (keyData == ConstValue.ActorData_Team)
            return TeamType;
        else if (keyData == ConstValue.ActorData_Character)
            return SelfChararcter;
        else if (keyData == ConstValue.ActorData_GetTarget)
            return TargetObject;
        else if (keyData == ConstValue.ActorData_SkillData)
        {
            int index = (int)datas[0];
            return SelfChararcter.GetSkillByIndex(index);
        }

        return base.GetData(keyData, datas);
    }

    public override void ThrowEvent(string keyData, params object[] datas)
    {

        if(keyData.Equals(ConstValue.ActorData_SetTarget))
        {
            TargetObject = datas[0] as BaseObject;
        }
        
        else if (keyData == ConstValue.EventKey_SelectSkill)
        {
            int index = (int)datas[0];
            SkillData data = SelfChararcter.GetSkillByIndex(index);
            SelfChararcter.SelectSkill = data;
        }

        else if (keyData == ConstValue.ActorData_Hit)
        {
            if (ObjectState == E_BASEOBJECTSTATE.STATE_DIE)
                return;

            if(ObjectState == E_BASEOBJECTSTATE.STATE_DODGE)
            {
                BaseBoard board_temp = BoardManager.Instance.AddBoard(this, E_BOARDTYPE.BOARD_DAMAGE);

                if (board_temp != null)
                {
                    board_temp.SetData(ConstValue.SetData_DamageText, "회피");
                }
                return;
            }


            GameCharacter casterCharacter = datas[0] as GameCharacter;
            SkillTemplate skillTemplate = datas[1] as SkillTemplate;

            casterCharacter.CharacterStatus.AddStatusData("Skill", skillTemplate.SkillStatus);

            double attackDamage = casterCharacter.CharacterStatus.GetStatusData(E_STATUSDATA.ATTACK);

            casterCharacter.CharacterStatus.RemoveStatusData("Skill");

            SelfChararcter.IncreaseCurrentHP(-attackDamage);

            BaseBoard board = BoardManager.Instance.GetBoardData(this, E_BOARDTYPE.BOARD_HP);

            if (board != null)
            {
                board.SetData(ConstValue.SetData_HP, GetStatusData(E_STATUSDATA.MAX_HP), SelfChararcter.CurrentHP);
            }

            board = null;

            board = BoardManager.Instance.AddBoard(this, E_BOARDTYPE.BOARD_DAMAGE);

            if (board != null)
            {
                board.SetData(ConstValue.SetData_Damage, attackDamage);
            }

            AI.Anim.SetInteger("Hit", 1);
        }

        else if(keyData == ConstValue.ActorData_CrowdControl)
        {
            if (ObjectState == E_BASEOBJECTSTATE.STATE_DIE)
                return;

            SkillTemplate skillTemplate = datas[0] as SkillTemplate;

            E_SKILLTEMPLATETYPE skillType = skillTemplate.SkillType;

            switch(skillType)
            {
                case E_SKILLTEMPLATETYPE.STUN_CROWDCONTROL:
                    {
                        AI.AddNextAI(E_STATETYPE.STATE_STUN);
                    }
                    break;

                case E_SKILLTEMPLATETYPE.GRAVITY_CROWDCONTROL:
                    {
                        AI.AddNextAI(E_STATETYPE.STATE_GRAVITY);
                    }
                    break;
            }           
        }

        else if (keyData == ConstValue.ActorData_Buff)
        {
            if (ObjectState == E_BASEOBJECTSTATE.STATE_DIE)
                return;

            SetDodgeState(E_BASEOBJECTSTATE.STATE_DODGE, 0.5f);
        }

        base.ThrowEvent(keyData, datas);
    }

    protected virtual void Update()
    {
        AI.UpdateAI();
        if(IsPlayer == false && AI.END)
        {
            Destroy(SelfObject);
        }
    }

    public void RunSkill()
    {
        SkillData selectSkill = SelfChararcter.SelectSkill;
        if (selectSkill == null)
            return;

        for (int i = 0; i < selectSkill.SkillTemplateList.Count; ++i)
        {
            SkillManager.Instance.RunSkill(this, selectSkill.SkillTemplateList[i]);
        }
    }

    private void OnEnable()
    {
        if (BoardManager.Instance != null)
            BoardManager.Instance.ShowBoard(this, true);
    }

    public void OnDisable()
    {
        if (BoardManager.Instance != null)
            BoardManager.Instance.ShowBoard(this, false);
    }

    public void OnDestroy()
    {
        if (BoardManager.Instance != null)
            BoardManager.Instance.ClearBoard(this);

        if (ActorManager.Instance != null)
            ActorManager.Instance.RemoveActor(this);
    }
}