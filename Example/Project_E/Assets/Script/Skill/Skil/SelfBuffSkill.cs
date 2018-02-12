using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBuffSkill : BaseSkill
{

    float StackTime = 0;

    public override void InitSkill()
    {

    }

    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if (StackTime >= 0.1f)
            End = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (End == true)
            return;

        GameObject colObject = other.gameObject;
        BaseObject actorObject = colObject.GetComponent<BaseObject>();

        if (actorObject != Target)
            return;

        Target.ThrowEvent(ConstValue.ActorData_Hit, Owner.GetData(ConstValue.ActorData_Character), Template);

    }
}
