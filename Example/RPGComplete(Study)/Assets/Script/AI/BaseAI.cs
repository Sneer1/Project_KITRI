using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextAI
{
    public EStateType StateType;
    public BaseObject TargetObject;
    public Vector3 Position;
}

public class BaseAI : BaseObject
{
    protected List<NextAI> ListNextAI = new List<NextAI>();

    [SerializeField]
    protected EStateType _CurrentState = EStateType.State_Idle;

    public EStateType CurrentState
    {
        get { return _CurrentState; }
    }

    public EAutoMode AutoMode = EAutoMode.Auto_On;

    bool bUpdateAI = false;
    bool bAttack = false;
    public bool IsAttack
    {
        get { return bAttack; }
        set { bAttack = value; }
    }

    bool bEnd = false;
    public bool END
    {
        get { return bEnd; }
        set { bEnd = value; }
    }

    protected Vector3 MovePosition = Vector3.zero;
    Vector3 PreMovePosition = Vector3.zero;

    Animator _Anim = null;
    NavMeshAgent _NavAgent = null;

    public Animator Anim
    {
        get
        {
            if (_Anim == null)
                _Anim = SelfObject.GetComponentInChildren<Animator>();

            return _Anim;
        }
    }

    public NavMeshAgent NavAgent
    {
        get
        {
            if (_NavAgent == null)
                _NavAgent = SelfObject.GetComponent<NavMeshAgent>();

            return _NavAgent;
        }
    }

    void ChangeAnimation()
    {
        if (Anim == null)
        {
            Debug.LogError(SelfObject.name + "애니메이터가 없습니다");
            return;
        }
        Anim.SetInteger("State", (int)CurrentState);
    }

    public virtual void AddNextAI(EStateType nextStateType, BaseObject targetObject = null, Vector3 position = new Vector3())
    {
        NextAI nextAI = new NextAI();
        nextAI.StateType = nextStateType;
        nextAI.TargetObject = targetObject;
        nextAI.Position = position;

        ListNextAI.Add(nextAI);
    }

    protected virtual void ProcessIdle()
    {
        _CurrentState = EStateType.State_Idle;
        ChangeAnimation();
    }

    protected virtual void ProcessMove()
    {
        _CurrentState = EStateType.State_Walk;
        ChangeAnimation();
    }

    protected virtual void ProcessAttack()
    {
        TargetComponent.ThrowEvent(ConstValue.EventKey_SelectSkill, 0);

        _CurrentState = EStateType.State_Attack;
        ChangeAnimation();
    }

    protected virtual void ProcessDie()
    {
        _CurrentState = EStateType.State_Dead;
        ChangeAnimation();
    }

    protected virtual IEnumerator Idle()
    {
        bUpdateAI = false;
        yield break;
    }

    protected virtual IEnumerator Move()
    {
        bUpdateAI = false;
        yield break;
    }

    protected virtual IEnumerator Attack()
    {
        bUpdateAI = false;
        yield break;
    }

    protected virtual IEnumerator Die()
    {
        bUpdateAI = false;
        yield break;
    }

    void SetNextAI(NextAI nextAI)
    {
        if (nextAI.TargetObject != null)
        {
            TargetComponent.ThrowEvent(ConstValue.ActorData_SetTarget, nextAI.TargetObject);
        }

        if (nextAI.Position != Vector3.zero)
        {
            MovePosition = nextAI.Position;
        }

        switch (nextAI.StateType)
        {
            case EStateType.State_Idle:
                {
                    ProcessIdle();
                }
                break;
            case EStateType.State_Attack:
                {
                    if(nextAI.TargetObject != null)
                    {
                        SelfTransform.forward = (nextAI.TargetObject.SelfTransform.position - SelfTransform.position).normalized;
                    }
                    ProcessAttack();
                }
                break;
            case EStateType.State_Walk:
                {
                    ProcessMove();
                }
                break;
            case EStateType.State_Dead:
                {
                    ProcessDie();
                }
                break;
        }
    }

    public void UpdateAI()
    {
        if (bUpdateAI == true)
            return;

        if (ListNextAI.Count > 0)
        {
            SetNextAI(ListNextAI[0]);
            ListNextAI.RemoveAt(0);
        }

        if (ObjectState == EBaseObjectState.ObjectState_Die)
        {
            ListNextAI.Clear();
            ProcessDie();
        }

        bUpdateAI = true;

        switch (CurrentState)
        {
            case EStateType.State_Idle:
                StartCoroutine("Idle");
                break;
            case EStateType.State_Attack:
                StartCoroutine("Attack");
                break;
            case EStateType.State_Walk:
                StartCoroutine("Move");
                break;
            case EStateType.State_Dead:
                StartCoroutine("Die");
                break;
        }
    }

    public void ClearAI()
    {
        ListNextAI.Clear();
        MovePosition = Vector3.zero;
        NavAgent.isStopped = true;
    }

    public void ClearAI(EStateType stateType)
    {
        /*
        //#1 for
        List<NextAI> removeIndex = new List<NextAI>();
        for (int i = 0; i < ListNextAI.Count; ++i)
        {
            //1-1
            NextAI nextAI = ListNextAI[i];
            if (nextAI.StateType == stateType)
            {
                ListNextAI.Remove(nextAI);
            }
            //1-2
            NextAI nextAI = ListNextAI[i];
            if (nextAI.StateType == stateType)
            {
                removeIndex.Add(nextAI);
            }
        }

        //1-2
        for (int i = 0; i < removeIndex.Count; ++i)
        {
            ListNextAI.Remove(removeIndex[i]);
        }*/

        //#2 predicate
        //tempState = stateType;
        //ListNextAI.RemoveAll(RemovePredicate);

        //#3 Lamda
        //ListNextAI.RemoveAll(
        //    (nextAI) =>
        //    {
        //        return nextAI.StateType == stateType;
        //    }
        //    ); 
        //리턴만 있을 때 람다
        ListNextAI.RemoveAll(
                    (nextAI) => nextAI.StateType == stateType
            );
    }

    //#2 predicate
    //EStateType tempState;
    //public bool RemovePredicate(NextAI nextAI)
    //{
    //    return nextAI.StateType == tempState;
    //}

    protected void SetMove(Vector3 position)
    {
        if (PreMovePosition == position)
            return;

        PreMovePosition = position;
        NavAgent.isStopped = false;
        NavAgent.SetDestination(position);
    }

    protected void Stop()
    {
        MovePosition = Vector3.zero;
        NavAgent.isStopped = true;
    }

    protected bool MoveCheck()
    {
        if(NavAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            if(NavAgent.hasPath == false || NavAgent.pathPending == false)
            {
                return true;
            }
        }

        return false;
    }
}