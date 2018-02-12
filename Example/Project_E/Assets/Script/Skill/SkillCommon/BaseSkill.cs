using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : BaseObject
{
    public BaseObject Owner
    {
        get;
        set;
    }

    public BaseObject Target
    {
        get;
        set;
    }

    public SkillTemplate Template
    {
        get;
        set;
    }

    public bool End
    {
        get;
        protected set;
    }

    abstract public void InitSkill();
    abstract public void UpdateSkill();
}