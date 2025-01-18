using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField]
    private Joystick _fixedJoystick;
    [SerializeField]
    private HeroRotator _heroRotator;
    [SerializeField]
    private Transform _enemyParentObject;
    [SerializeField]
    private float _meleeAttackDistance;

    public static GameObject _nearestEnemy;
    public EnemyHealthSystem _enemyHealthSystem;
    private float _swordDamage;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MaxDamage"))
        {
            _swordDamage = PlayerPrefs.GetFloat("MaxDamage");
        }
        else
        {
            _swordDamage = UpgradesManager._bulletDamage;
        }
    }
    private void Update()
    {
        if(_fixedJoystick.Direction == Vector2.zero && _enemyParentObject.childCount > 0)
        {
            if (_nearestEnemy == null)
            {
                _nearestEnemy = _enemyParentObject.GetChild(0).gameObject;
                _enemyHealthSystem = _nearestEnemy.GetComponent<EnemyHealthSystem>();
            }
   
            for (int i = 0; i < _enemyParentObject.childCount; i++)
            {
                if (Vector3.Distance(transform.position, _enemyParentObject.GetChild(i).transform.position) < Vector3.Distance(transform.position, _nearestEnemy.transform.position))
                {
                    _nearestEnemy = _enemyParentObject.GetChild(i).gameObject;
                    _enemyHealthSystem = _nearestEnemy.GetComponent<EnemyHealthSystem>();
                }
            }

            if (Vector3.Distance(transform.position, new Vector3(_nearestEnemy.transform.position.x, transform.position.y, _nearestEnemy.transform.position.z)) < _meleeAttackDistance)
            {
                _heroRotator.RotateToEnemy(_nearestEnemy.transform, "Sword");
            }
            else
            {
                _heroRotator.RotateToEnemy(_nearestEnemy.transform, "Bow");
            }
        }
        else if(_fixedJoystick.Direction == Vector2.zero && _enemyParentObject.childCount == 0)
        {
            _heroRotator.ResetAttack();
        }
    }

    public void AttackNearestEnemy()
    {
        _enemyHealthSystem.ApplyDamage(_swordDamage);
    }
}
