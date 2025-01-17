using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : EntityState
{
    PlayerMnagaer _parrent;

    public override void EnterState(PlayerMnagaer _parrent)
    {
        _parrent.animator.SetInteger("State", 0);
        this._parrent = _parrent;
    }

    public override void ExitState(EntityStateEnum _nextState)
    {

    }

    public override EntityStateEnum GetState()
    {
        return EntityStateEnum.Idle;
    }

    public override void StateLogic()
    {
        _parrent.transform.rotation = _parrent.lastRotation;
        _parrent._rb.velocity = new Vector3(0f, -5.1f, 0f);
        _parrent.SetState(new PlayerRunState());

        if (_parrent.MovementJoystick.Vertical != 0 || _parrent.MovementJoystick.Horizontal != 0)
        {
            _parrent.SetState(new PlayerRunState());
        }
    }
}
