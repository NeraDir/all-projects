using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _meleeAttackDistance;


    private NavMeshAgent _navMeshAgent;
    private GameObject _mainHero;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _mainHero = GameObject.Find("MainHero"); 
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_mainHero.transform.position);

        if (Vector3.Distance(transform.position, _mainHero.transform.position) <= _navMeshAgent.stoppingDistance)
        {
            _animator.SetBool("IsAttack", true);
        }
        else
        {
            _animator.SetBool("IsAttack", false);
        }
    }
}
