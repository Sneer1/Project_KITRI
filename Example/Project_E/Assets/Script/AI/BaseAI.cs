using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextAI
{
    public E_STATETYPE StateType;
    public BaseObject TargetObject;
    public Vector3 Position;
}

public class BaseAI : BaseObject
{
    protected List<NextAI> ListNextAI = new List<NextAI>();

    [SerializeField]
    protected E_STATETYPE _CurrentState = E_STATETYPE.STATE_IDLE;

    public E_STATETYPE CurrentState
    {
        get { return _CurrentState; }
    }

    public int Attack_Type
    {
        set;
        get;
    }

    public E_AUTOMODE AutoMode = E_AUTOMODE.Auto_On;

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

    public virtual void AddNextAI(E_STATETYPE nextStateType, BaseObject targetObject = null, Vector3 position = new Vector3())
    {  
     
        NextAI nextAI = new NextAI();
        nextAI.StateType = nextStateType;
        nextAI.TargetObject = targetObject;
        nextAI.Position = position;

        ListNextAI.Add(nextAI);
    }

    protected virtual void ProcessIdle()
    {
        _CurrentState = E_STATETYPE.STATE_IDLE;
        ChangeAnimation();
    }

    protected virtual void ProcessMove()
    {
        _CurrentState = E_STATETYPE.STATE_WALK;
        ChangeAnimation();
    }

    protected virtual void ProcessAttack()
    {
        Actor actor = Target as Actor;
        int nCount = actor.SelfChararcter.GetListCount() + 1;

        Attack_Type = Random.Range(0, nCount);

        Target.ThrowEvent(ConstValue.EventKey_SelectSkill, Attack_Type);

        Anim.SetInteger("Attack_Type", Attack_Type);
        _CurrentState = E_STATETYPE.STATE_ATTACK;
        ChangeAnimation();
    }

    protected virtual void ProcessDie()
    {
        _CurrentState = E_STATETYPE.STATE_DEAD;
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
            Target.ThrowEvent(ConstValue.ActorData_SetTarget, nextAI.TargetObject);
        }

        if (nextAI.Position != Vector3.zero)
        {
            MovePosition = nextAI.Position;
        }

        switch (nextAI.StateType)
        {
            case E_STATETYPE.STATE_IDLE:
                {
                    ProcessIdle();
                }
                break;
            case E_STATETYPE.STATE_ATTACK:
                {
                    if(nextAI.TargetObject != null)
                    {
                        SelfTransform.forward = (nextAI.TargetObject.SelfTransform.position - SelfTransform.position).normalized;
                    }
                    ProcessAttack();
                }
                break;
            case E_STATETYPE.STATE_WALK:
                {
                    ProcessMove();
                }
                break;
            case E_STATETYPE.STATE_DEAD:
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

        if (ObjectState == E_BASEOBJECTSTATE.STATE_DIE)
        {
            ListNextAI.Clear();
            ProcessDie();
        }

        bUpdateAI = true;

        switch (CurrentState)
        {
            case E_STATETYPE.STATE_IDLE:
                StartCoroutine("Idle");
                break;
            case E_STATETYPE.STATE_ATTACK:
                StartCoroutine("Attack");
                break;
            case E_STATETYPE.STATE_WALK:
                StartCoroutine("Move");
                break;
            case E_STATETYPE.STATE_DEAD:
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

    public void ClearAI(E_STATETYPE stateType)
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