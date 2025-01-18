using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotating : MonoBehaviour
{
    private Camera m_Cam;
    private Transform m_Transform;
    private PlayerShooting m_Shooting;

    private void Awake()
    {
        m_Cam = FindObjectOfType<Camera>();
        m_Transform = GetComponent<Transform>();
        m_Shooting = GetComponent<PlayerShooting>() ? GetComponent<PlayerShooting>() : GetComponentInChildren<PlayerShooting>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                m_Transform.LookAt(new Vector3(hit.point.x,0,hit.point.z));
                m_Shooting.Shoot();
            }
        }
    }
}
