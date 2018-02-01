using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : BaseAI
{
    protected override IEnumerator Idle()
    {
        BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(TargetComponent);

        if (targetObject != null)
        {
            SkillData sData = TargetComponent.GetData(ConstValue.ActorData_SKILLDATA, 0) as SkillData;

            float attackRange = 1.1f;

            if (sData != null)
                attackRange = sData.Range;

            float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

            if (distance < attackRange)
            {
                Stop();
                AddNextAI(EStateType.State_Attack, targetObject);
            }

            else
            {
                AddNextAI(EStateType.State_Walk);
            }
        }

        yield return StartCoroutine(base.Idle());
    }

    protected override IEnumerator Move()
    {
        if (AutoMode == EAutoMode.Auto_On)
        {
            BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(TargetComponent);

            if (targetObject != null)
            {
                SkillData sData = TargetComponent.GetData(ConstValue.ActorData_SKILLDATA, 0) as SkillData;

                float attackRange = 1.1f;

                if (sData != null)
                    attackRange = sData.Range;

                float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

                if (distance < attackRange)
                {
                    Stop();
                    AddNextAI(EStateType.State_Attack, targetObject);
                }

                else
                {
                    SetMove(targetObject.SelfTransform.position);
                }
            }
        }
        else
        {
            if (MovePosition != Vector3.zero)
            {
                SetMove(MovePosition);
                MovePosition = Vector3.zero;
                yield return null;
            }

            if (MoveCheck() == false)
            {
                Stop();
                AddNextAI(EStateType.State_Idle);
            }
        }
        yield return StartCoroutine(base.Move());
    }

    protected override IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();

        while (IsAttack)
        {
            if (ObjectState == EBaseObjectState.ObjectState_Die)
                break;

            yield return new WaitForEndOfFrame();
        }

        AddNextAI(EStateType.State_Idle);

        yield return StartCoroutine(base.Attack());
    }

    protected override IEnumerator Die()
    {
        END = true;
        yield return StartCoroutine(base.Die());
    }
}