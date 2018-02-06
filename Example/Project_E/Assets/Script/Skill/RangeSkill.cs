using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSkill : BaseSkill
{
    GameObject ModelPrefab = null;

    public override void InitSkill()
    {
        if (ModelPrefab == null)
            return;

        GameObject go = Instantiate(ModelPrefab, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(this.transform, false);
    }

    public override void UpdateSkill()
    {
        if (Target == null)
        {
            End = true;
            return;
        }

        Vector3 TargetPosition = SelfTransform.position + (Target.SelfTransform.position - SelfTransform.position).normalized * 10 * Time.deltaTime;
        SelfTransform.position = TargetPosition;
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
        if(End == true)
            return;

        GameObject colObject = other.gameObject;
        BaseObject actorObject = colObject.GetComponent<BaseObject>();

        if (actorObject != Target)
            return;

        Target.ThrowEvent(ConstValue.ActorData_Hit, Owner.GetData(ConstValue.ActorData_Character), Template);
        End = true;
    }

}
