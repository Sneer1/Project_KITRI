using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quick_CowdControl : BaseSkill {

    GameObject ModelPrefab = null;

    public override void InitSkill()
    {
        if (ModelPrefab == null)
            return;

        GameObject go = Instantiate(ModelPrefab, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(this.transform, false);

        SelfTransform.position = Target.SelfTransform.position + Vector3.up;

        if (End == true)
            return;

        Target.ThrowEvent(ConstValue.ActorData_CrowdControl, Template);
    }

    public override void UpdateSkill()
    {
        if (Target == null)
        {
            End = true;
            return;
        }

        if (Owner.SelfObject.GetComponent<Actor>().AI.IsAttack == false)
        {
            End = true;
        }

        //Vector3 TargetPosition = SelfTransform.position + (Target.SelfTransform.position - SelfTransform.position).normalized * 10 * Time.deltaTime;
        //SelfTransform.position = TargetPosition;
    }

    public override void ThrowEvent(string keyData, params object[] datas)
    {
        if (keyData == ConstValue.EventKey_SelectModel)
        {
            ModelPrefab = datas[0] as GameObject;
        }
        else
            base.ThrowEvent(keyData, datas);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (End == true)
        //    return;

        //GameObject colObject = other.gameObject;
        //BaseObject actorObject = colObject.GetComponent<BaseObject>();

        //if (actorObject != Target)
        //    return;

        //Target.ThrowEvent(ConstValue.ActorData_Hit, Owner.GetData(ConstValue.ActorData_Character), Template);
        //End = true;
    }
}
