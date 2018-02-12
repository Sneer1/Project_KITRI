using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DeathAnimation : StateMachineBehaviour
{

    Actor TargetActor = null;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TargetActor = animator.GetComponentInParent<Actor>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (stateInfo.normalizedTime >= 1.0f && TargetActor.AI.CurrentState == E_STATETYPE.STATE_DEAD)
        {
            TargetActor.IsPlayer = false;
        }
    }
}