using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAnimation : StateMachineBehaviour
{

    Actor TargetActor = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        TargetActor = animator.GetComponentInParent<Actor>();

        if (TargetActor != null)
        {
            if (TargetActor.AI.CurrentState == E_STATETYPE.STATE_GRAVITY)
            {
                TargetActor.AI.IsGravity = true;
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (TargetActor == null)
            return;

        if (animatorStateInfo.normalizedTime >= 1.0f && TargetActor.AI.IsGravity)
        {
            if (TargetActor.AI.CurrentState == E_STATETYPE.STATE_GRAVITY)
                TargetActor.AI.IsGravity = false;
        }
    }
}
