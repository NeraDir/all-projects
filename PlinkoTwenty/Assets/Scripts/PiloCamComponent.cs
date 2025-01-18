using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PiloCamComponent : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private Vector3 m_EnableDirections;
    [SerializeField] private float m_Speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3((m_Target.position.x + m_Offset.x) * m_EnableDirections.x, transform.position.y, (m_Target.position.z + m_Offset.z) * m_EnableDirections.z), m_Speed * Time.deltaTime);
    }
}
