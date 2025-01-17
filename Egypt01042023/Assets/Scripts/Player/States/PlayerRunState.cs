using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : EntityState
{
    PlayerMnagaer _parrent;

    public override void EnterState(PlayerMnagaer _parrent)
    {
        _parrent.animator.SetInteger("State", 3);
        this._parrent = _parrent;
    }

    public override void ExitState(EntityStateEnum _nextState)
    {

    }

    public override EntityStateEnum GetState()
    {
        return EntityStateEnum.Run;
    }

    public override void StateLogic()
    {
        if (_parrent.MovementJoystick.Vertical != 0 || _parrent.MovementJoystick.Horizontal != 0)
        {
            float _targetRotation = Mathf.Atan2(_parrent.MovementJoystick.Horizontal, _parrent.MovementJoystick.Vertical) * Mathf.Rad2Deg;
            _parrent.transform.rotation = GetRotation(_parrent.transform, new Quaternion(0, _targetRotation, 0, 0));

            //_parrent._rb.velocity = new Vector3(_parrent.MovementJoystick.Horizontal * _parrent.speed, _parrent._rb.velocity.y, _parrent.MovementJoystick.Vertical * _parrent.speed);
            _parrent._rb.velocity = _parrent.transform.forward * _parrent.speed;
            if (_parrent._rb.velocity.x != 0 || _parrent._rb.velocity.z != 0)
            {
                if (!_parrent.AttackNow)
                    _parrent.animator.SetInteger("State", 3);
            }
            else
            {
                if (!_parrent.AttackNow)
                    _parrent.animator.SetInteger("State", 0);
            }
            _parrent.lastRotation = _parrent.transform.rotation;
        }
        else
        {
            _parrent.transform.rotation = _parrent.lastRotation;
            _parrent._rb.velocity = new Vector3(0f, -5.1f, 0f);
            if (!_parrent.AttackNow)
                _parrent.animator.SetInteger("State", 0);
        }
    }

    private Quaternion GetRotation(Transform transform, Quaternion rotation)
    {
        float RotationSmoothTime = 0.04f;
        float _rotationVelocity = 0;
        return Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation.y, ref _rotationVelocity,
                RotationSmoothTime), 0);

    }
}
