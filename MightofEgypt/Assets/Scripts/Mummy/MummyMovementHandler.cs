using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class MummyMovementHandler : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;

    [SerializeField] private Transform _targetPlayer;

    private NavMeshAgent _agent;

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _movementSpeed;
    }

    private void FixedUpdate() {
        MoveToPlayer();
    }

    private void MoveToPlayer() {
        _agent.SetDestination(_targetPlayer.position);
    }

    public void LookAtPlayer() => transform.LookAt(_targetPlayer.position);
}
