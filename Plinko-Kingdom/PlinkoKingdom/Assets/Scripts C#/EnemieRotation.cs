using UnityEngine;

public class EnemieRotation : MonoBehaviour
{
    private PlayerHealth m_PlayerHealth;

    private Transform m_Transform;

    private void Awake() { m_PlayerHealth = FindObjectOfType<PlayerHealth>(); m_Transform = GetComponent<Transform>(); }

    private void LateUpdate()
    {
        Vector3 relativePos = m_PlayerHealth.m_target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime
            * 11);

       /* m_Transform?.LookAt(new Vector3(m_PlayerHealth.m_target.position.x, 0, m_PlayerHealth.m_target.position.z));*/
    }
}
