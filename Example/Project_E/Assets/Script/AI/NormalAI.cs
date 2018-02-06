using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : BaseAI
{
    protected override IEnumerator Idle()
    {
        BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);

        if (targetObject != null)
        {
            SkillData sData = Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;

            float attackRange = 1.1f;

            if (sData != null)
                attackRange = sData.Range;

            float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

            if (distance < attackRange)
            {
                Stop();
                AddNextAI(E_STATETYPE.STATE_ATTACK, targetObject);
            }

            else
            {
                AddNextAI(E_STATETYPE.STATE_WALK);
            }
        }

        yield return StartCoroutine(base.Idle());
    }

    protected override IEnumerator Move()
    {
        if (AutoMode == E_AUTOMODE.Auto_On)
        {
            BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);

            if (targetObject != null)
            {
                SkillData sData = Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;

                float attackRange = 1.1f;

                if (sData != null)
                    attackRange = sData.Range;

                float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

                if (distance < attackRange)
                {
                    Stop();
                    AddNextAI(E_STATETYPE.STATE_ATTACK, targetObject);
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
                AddNextAI(E_STATETYPE.STATE_IDLE);
            }
        }
        yield return StartCoroutine(base.Move());
    }

    protected override IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();

        while (IsAttack)
        {
            if (ObjectState == E_BASEOBJECTSTATE.STATE_DIE)
                break;

            yield return new WaitForEndOfFrame();
        }

        AddNextAI(E_STATETYPE.STATE_IDLE);

        yield return StartCoroutine(base.Attack());
    }

    protected override IEnumerator Die()
    {
        END = true;
        yield return StartCoroutine(base.Die());
    }
}