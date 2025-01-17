using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : EntityState
{
    public override void EnterState(PlayerMnagaer _parrent)
    {
        _parrent.animator.SetInteger("State", 2);
    }

    public override void ExitState(EntityStateEnum _nextState)
    {

    }

    public override EntityStateEnum GetState()
    {
        return EntityStateEnum.Attack;
    }

    public override void StateLogic()
    {

    }
}
