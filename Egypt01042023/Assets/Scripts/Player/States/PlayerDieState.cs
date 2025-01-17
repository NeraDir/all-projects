using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : EntityState
{
    public override void EnterState(PlayerMnagaer _parrent)
    {
        _parrent.animator.SetInteger("State", 4);
    }

    public override void ExitState(EntityStateEnum _nextState)
    {

    }

    public override EntityStateEnum GetState()
    {
        return EntityStateEnum.Die;
    }

    public override void StateLogic()
    {

    }
}
