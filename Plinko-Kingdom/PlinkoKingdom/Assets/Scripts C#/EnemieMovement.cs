using UnityEngine;
using UnityEngine.AI;

public class EnemieMovement : MonoBehaviour
{
    private PlayerHealth m_PlayerHealth;

    private NavMeshAgent m_NavMeshAgent;

    private void Awake() 
    {
        m_PlayerHealth = FindObjectOfType<PlayerHealth>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        if (m_PlayerHealth != null)
            m_NavMeshAgent?.SetDestination(m_PlayerHealth.m_target.position);
    }
}
