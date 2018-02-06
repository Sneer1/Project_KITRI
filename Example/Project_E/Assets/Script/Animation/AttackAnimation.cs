using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
    Actor TargetActor = null;
    bool bIsAttack = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        TargetActor = animator.GetComponentInParent<Actor>();

        if (TargetActor != null)
        {
            if(TargetActor.AI.CurrentState == E_STATETYPE.STATE_ATTACK)
            {
                TargetActor.AI.IsAttack = true;
                bIsAttack = false;
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (TargetActor == null)
            return;

        if(bIsAttack == false && animatorStateInfo.normalizedTime > 0.5f)
        {
            bIsAttack = true;
            TargetActor.RunSkill();
        }

        if(animatorStateInfo.normalizedTime >= 1.0f && TargetActor.AI.IsAttack)
        {
            if (TargetActor.AI.CurrentState == E_STATETYPE.STATE_ATTACK)
                TargetActor.AI.IsAttack = false;
        }
    }
}
