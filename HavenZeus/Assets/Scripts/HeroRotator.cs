using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRotator : MonoBehaviour
{
    [SerializeField]
    private Joystick _fixedJoystick;
    [SerializeField]
    private Animator _animator;

    void FixedUpdate()
    {
        if(_fixedJoystick.Direction != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_fixedJoystick.Direction.x, 0f, _fixedJoystick.Direction.y));
        }
    }

    public void RotateToEnemy(Transform _enemyPosition, string attackType)
    {
        if(attackType == "Sword")
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(_enemyPosition.position.x, transform.position.y, _enemyPosition.position.z) - transform.position);
            _animator.SetBool("IsBowAttack", false);
            _animator.SetBool("IsSwordAttack", true);
        }
        else if(attackType == "Bow")
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.right, transform.position - new Vector3(_enemyPosition.position.x, transform.position.y, _enemyPosition.position.z));
            _animator.SetBool("IsSwordAttack", false);
            _animator.SetBool("IsBowAttack", true);
        }
    }

    public void ResetAttack()
    {
        _animator.SetBool("IsBowAttack", false);
        _animator.SetBool("IsSwordAttack", false);
    }
}
