using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAnimation : StateMachineBehaviour
{

    Actor TargetActor = null;
    bool bIsStun = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        TargetActor = animator.GetComponentInParent<Actor>();

        if (TargetActor != null)
        {
            if (TargetActor.AI.CurrentState == E_STATETYPE.STATE_STUN)
            {
                TargetActor.AI.IsStun = true;
                bIsStun = false;
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (TargetActor == null)
            return;

        if (animatorStateInfo.normalizedTime >= 1.0f && TargetActor.AI.IsStun)
        {
            if (TargetActor.AI.CurrentState == E_STATETYPE.STATE_STUN)
                TargetActor.AI.IsStun = false;
        }
    }
}
