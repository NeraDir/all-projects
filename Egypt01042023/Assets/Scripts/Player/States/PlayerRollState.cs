using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : EntityState
{
    public override void EnterState(PlayerMnagaer _parrent)
    {
        _parrent.animator.SetInteger("State", 1);
    }

    public override void ExitState(EntityStateEnum _nextState)
    {

    }

    public override EntityStateEnum GetState()
    {
        return EntityStateEnum.Roll;
    }

    public override void StateLogic()
    {

    }
}
